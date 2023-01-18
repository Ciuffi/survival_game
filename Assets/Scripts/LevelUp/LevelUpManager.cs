using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
public class LevelUpManager : MonoBehaviour
{
    public float baseXP;
    public float growthMultiplier;
    private Slider xpBar;
    private CoroutineQueue xpBarQueue;
    private List<UpgradeHandler> upgradeWindows;
    private GameObject panel;
    public GameObject[] weapons;
    public GameObject[] stats;
    public List<GameObject> upgrades;
    public bool isWeapon = true;
    private List<GameObject> previousUpgrades = new List<GameObject>();

    public GameObject RerollBtn, SwapBtn;

    public int GetXpToNextLevel(float level)
    {
        return (int)(Mathf.Floor(baseXP * (Mathf.Pow(level, growthMultiplier))));
    }

    public void ResetXP()
    {
        xpBarQueue.EmptyQueue();
        xpBarQueue.AddToQueue(BarHelper.ForceUpdateBar(xpBar, 0, GetXpToNextLevel(1)));
    }

    public void AddXP(float currXp, float newXp, float maxXp)
    {
        xpBarQueue.AddToQueue(BarHelper.AddToBar(xpBar, currXp, newXp, maxXp, 0.3f));
    }
    public void LevelUp(float level)
    {
        xpBarQueue.AddToQueue(BarHelper.RemoveFromBarTimed(xpBar, 0.3f));
        ShowLevelUpUI();
    }

    public void reroll() //check if weapon or stat, then swap so it swaps back for setUpgrades()
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

    public void swap()
    {
        setUpgrades();
    }

    public void setUpgrades()
    {
        if (isWeapon)
        {
            upgrades = new List<GameObject>(weapons);
            isWeapon = false;
        }
        else
        { 
            upgrades = new List<GameObject>(stats);
            isWeapon = true;
        }

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
        });

        previousUpgrades.Clear();
    }

 
    public void ShowLevelUpUI()
    {
        PauseGame();
        RerollBtn.GetComponent<RollSwapHandler>().resetChances();
        SwapBtn.GetComponent<RollSwapHandler>().resetChances();
        setUpgrades();
        GameObject.FindObjectOfType<CanvasClickHandler>().DisableJoystick(); 
        panel.SetActive(true);
    }
    public void SignalItemChosen()
    {
        panel.SetActive(false);
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

    // Start is called before the first frame update
    void Start()
    {
        xpBarQueue = gameObject.AddComponent<CoroutineQueue>();
        xpBarQueue.StartQueue();
        xpBar = GameObject.Find("xpBar").GetComponent<Slider>();
        panel = GameObject.Find("UpgradeContainer");
        upgradeWindows = new List<UpgradeHandler>(GameObject.FindObjectsOfType<UpgradeHandler>());
        panel.SetActive(false);
        weapons = Resources.LoadAll("Attacks", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();
        stats = Resources.LoadAll("Stats", typeof(GameObject)).Cast<GameObject>().ToArray<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
