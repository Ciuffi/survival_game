using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LootBox : MonoBehaviour
{

    Rigidbody2D rb;
    PlayerMovement player;

    SpriteRenderer Sprite;
    private SpriteRenderer spriteRend;
    Color OGcolor;

    private List<UpgradeHandler> upgradeWindows;
    public GameObject[] weapons;
    public GameObject[] stats;
    public List<GameObject> upgrades;
    public bool isWeapon = true;
    private List<GameObject> previousUpgrades = new List<GameObject>();

    public GameObject RerollBtn, SwapBtn;
    public GameObject TimelineManager;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        print("started");
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;

        panel = GameObject.Find("LootContainer");
        print(panel);
        panel.SetActive(false);
        print(panel);
        upgradeWindows = new List<UpgradeHandler>(GameObject.FindObjectsOfType<UpgradeHandler>());
        weapons = Resources.LoadAll("Attacks", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();
        stats = Resources.LoadAll("Stats", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();

    }

    private void FixedUpdate()
    {
        print("hello");

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Player")
        {
            ShowLevelUpUI();
            Destroy(gameObject);

        }
    }

    public void setUpgrades()
    {

        if (isWeapon)
        {
            upgrades = new List<GameObject>(weapons);
            //rarity stuff?

            //create weighting later
            upgradeWindows.ForEach((u) =>
            {
                GameObject GO = null;
                while (GO == null)
                {
                    GO = upgrades[Random.Range(0, upgrades.Count)];
                    if (previousUpgrades.Contains(GO))
                    {
                        GO = null;
                    }
                }
                previousUpgrades.Add(GO);
                u.upgrade = GO.GetComponent<Upgrade>();
                u.GetComponentInChildren<TMP_Text>().text = GO.name;
                u.transform.Find("Image").GetComponent<Image>().enabled = true;
                u.transform.Find("Image").GetComponent<Image>().sprite = GO.GetComponent<Attack>().thrownSprite;
            });

        }
        else
        {
            upgrades = new List<GameObject>(stats);

            //create weighting later
            upgradeWindows.ForEach((u) =>
            {
                GameObject GO = null;
                while (GO == null)
                {
                    GO = upgrades[Random.Range(0, upgrades.Count)];
                    if (previousUpgrades.Contains(GO))
                    {
                        GO = null;
                    }
                }
                previousUpgrades.Add(GO);
                u.upgrade = GO.GetComponent<Upgrade>();
                u.GetComponentInChildren<TMP_Text>().text = GO.name;
                u.transform.Find("Image").GetComponent<Image>().enabled = false;

            });
        }

    }

    public void reroll() 
    {
        setUpgrades();
    }

    public void swap()
    {
        if (isWeapon)
        {
            isWeapon = false;
            setUpgrades();
        }
        else
        {
            isWeapon = true;
            setUpgrades();
        }
    }

    public void ShowLevelUpUI()
    {
        PauseGame();
        GameObject.FindObjectOfType<CanvasClickHandler>().DisableJoystick();
        panel.SetActive(true);
        RerollBtn.GetComponent<RollSwapHandler>().resetChances();
        SwapBtn.GetComponent<RollSwapHandler>().resetChances();
        //eventually want to move this to on-confirm-selection, and add a new button to close menu
        TimelineManager.GetComponent<TimelineUI>().addAttack();
        TimelineManager.GetComponent<TimelineUI>().spawnTimeline();
        setUpgrades();
    }

    public void SignalItemChosen()
    {
        panel.SetActive(false);
        previousUpgrades.Clear();
        TimelineManager.GetComponent<TimelineUI>().despawnTimeline();
        GameObject.FindObjectOfType<CanvasClickHandler>().EnableJoystick();
        ResumeGame();
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        GameObject.FindObjectOfType<PlayerMovement>().StopMoving();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        GameObject.FindObjectOfType<PlayerMovement>().StartMoving();
    }


}
