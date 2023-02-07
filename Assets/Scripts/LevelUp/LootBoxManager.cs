using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;


public class LootBoxManager : MonoBehaviour
{
    GameObject player;
    public GameObject upgradeWindow;
    public GameObject[] weapons;
    public GameObject[] stats;
    public List<GameObject> upgrades;
    public bool isWeapon = true;
    private List<GameObject> previousUpgrades = new List<GameObject>();


    private GameObject panel;
    private GameObject panelAnimated;
    public GameObject RerollBtn, SwapBtn, TimelineManager;

    public GameObject VFX;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        panel = GameObject.Find("LootContainer");
        panelAnimated = GameObject.Find("LootPopup");
        panel.SetActive(false);
        panelAnimated.SetActive(false);

        weapons = Resources.LoadAll("Attacks", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();
        stats = Resources.LoadAll("Stats", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();

    }

    public void ShowLootUI()
    {
        Instantiate(VFX, transform.position, Quaternion.identity, transform);
        StartCoroutine(WaitForTime(0.6f));  
    }

    public void ShowLootReward()
    {
        panel.SetActive(true);
        RerollBtn.GetComponent<RollSwapHandler>().setActive();
        SwapBtn.GetComponent<RollSwapHandler>().setActive();
        //eventually want to move this to on-confirm-selection, and add a new button to close menu
        TimelineManager.GetComponent<TimelineUI>().addAttack();
        TimelineManager.GetComponent<TimelineUI>().spawnTimeline();
        setUpgrades();
    }

    public void setUpgrades()
    {

        if (isWeapon)
        {
            upgrades = new List<GameObject>(weapons);
            //rarity stuff?

            //create weighting later

            GameObject GO = null;
            while (GO == null)
            {
                GO = upgrades[Random.Range(0, upgrades.Count)];
                if (previousUpgrades.Contains(GO))
                {
                    GO = null;
                }
                else if (GO != null)
                {
                    previousUpgrades.Add(GO);
                    Upgrade upgrade = GO.GetComponent<Upgrade>();
                    upgradeWindow.GetComponent<UpgradeLootHandler>().upgrade = upgrade;
                    upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite = GO.GetComponent<Attack>().thrownSprite;
                }
            }

        }
        else
        {
            upgrades = new List<GameObject>(stats);

            //create weighting later
            GameObject GO = null;
            while (GO == null)
            {
                GO = upgrades[Random.Range(0, upgrades.Count)];
                if (previousUpgrades.Contains(GO))
                {
                    GO = null;
                }
                else if (GO != null)
                {
                    previousUpgrades.Add(GO);
                    Upgrade upgrade = GO.GetComponent<Upgrade>();
                    upgradeWindow.GetComponent<UpgradeLootHandler>().upgrade = upgrade;
                    upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = false;
                    //upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite = GO.GetComponent<Attack>().thrownSprite;
                }
            }
        }

    }

    private IEnumerator WaitForTime(float waitTime)
    {
        //float startTime = Time.realtimeSinceStartup;
        //float endTime = startTime + waitTime;
        //while (Time.realtimeSinceStartup < endTime) {yield return null;}   //for running while game is paused

        yield return new WaitForSeconds(waitTime);
        GameObject.FindObjectOfType<CanvasClickHandler>().DisableJoystick();
        PauseGame();
        panelAnimated.SetActive(true);
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

    public void SignalItemChosen()
    {
        previousUpgrades.Clear();
        TimelineManager.GetComponent<TimelineUI>().despawnTimeline();
        GameObject.FindObjectOfType<CanvasClickHandler>().EnableJoystick();
        panel.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
