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
    public Color flashColor;
    private Color xpColor;
    private CoroutineQueue xpBarQueue;
    private List<UpgradeHandler> upgradeWindows;
    private GameObject panel;
    public AttackBuilder[] weaponBuilders;
    public Upgrade[] weapons;
    public List<GameObject> playerStatUpgrades;
    public List<GameObject> weaponStatUpgrades;
    public List<GameObject> wpnSetUpgrades;
    public List<GameObject> upgrades;
    public bool isWeapon = false;
    private List<GameObject> previousUpgrades = new List<GameObject>();

    public GameObject RerollBtn,
        SwapBtn;
    private bool hasRolled;
    public GameObject SkipBtn;

    public GameObject TimelineManager;
    public GameObject VFX;
    public StatsHandler playerStats;

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
        xpBarQueue.AddToQueue(BarHelper.AddToBar(xpBar, currXp, newXp, maxXp, 0.1f));
    }

    public void LevelUp(float level)
    {
        xpBarQueue.AddToQueue(BarHelper.RemoveFromBarTimed(xpBar, 0.2f));
        ShowLevelUpUI();
    }

    public void reroll() //check if weapon or stat, then swap so it swaps back for setUpgrades()
    {
        if (isWeapon)
        {
            isWeapon = true;

            setUpgrades();
        }
        else
        {
            isWeapon = false;

            setUpgrades();
        }
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

    public void setUpgrades()
    {
        previousUpgrades.Clear();

        if (isWeapon)
        {
            //isWeapon = false;

            //create weighting later
            upgradeWindows.ForEach(
                (u) =>
                {
                    AttackBuilder builder = weaponBuilders[Random.Range(0, weaponBuilders.Length)];
                    GameObject GO = builder.Build((Rarity)Random.Range(0, 6)).gameObject;
                    u.upgrade = GO.GetComponent<Upgrade>();
                    u.GetComponentInChildren<TMP_Text>().text = GO.name;
                    u.transform.Find("Image").GetComponent<Image>().enabled = true;
                    u.transform.Find("Image").GetComponent<Image>().sprite =
                        GO.GetComponent<Upgrade>().GetUpgradeIcon();
                    TMP_Text[] textComponents = u.GetComponentsInChildren<TMP_Text>();
                    textComponents[1].text = GO.GetComponent<Attack>().attackType.ToString();
                }
            );
        }
        else
        {
            upgrades = new List<GameObject>(playerStatUpgrades);

            upgrades.AddRange(GetAttackStats().Select(s => s.GetTransform().gameObject).ToList());
            upgrades.AddRange(
                getAttackSetStats().Select(s => s.GetTransform().gameObject).ToList()
            );
            //isWeapon = true;

            //create weighting later
            upgradeWindows.ForEach(
                (u) =>
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
                    u.transform.Find("Image").GetComponent<Image>().sprite =
                        GO.GetComponent<PlayerCharacterStats>().icon;
                    TMP_Text[] textComponents = u.GetComponentsInChildren<TMP_Text>();
                    textComponents[1].text = "";
                }
            );
        }
    }

    public void ShowLevelUpUI()
    {
        Instantiate(VFX, transform.position, Quaternion.identity, transform);
        xpBar.fillRect.GetComponent<Image>().color = flashColor;

        StartCoroutine(WaitForTime(0.4f));
    }

    private IEnumerator WaitForTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PauseGame();
        GameObject.FindObjectOfType<CanvasClickHandler>().DisableJoystick();
        RerollBtn.GetComponent<RollSwapHandler>().setActive();
        SwapBtn.GetComponent<RollSwapHandler>().setActive();
        SkipBtn.GetComponent<SkipHandler>().setActive();
        upgradeWindows.ForEach(
            (u) =>
            {
                u.GetComponent<UpgradeHandler>().setActive();
            }
        );

        //eventually want to move this to on-confirm-selection, and add a new button to close menu
        TimelineManager.GetComponent<TimelineUI>().addAttack();
        TimelineManager.GetComponent<TimelineUI>().spawnTimeline();

        isWeapon = false; //always show upgrade
        setUpgrades();
        panel.SetActive(true);
    }

    public void SignalItemChosen()
    {
        panel.SetActive(false);
        TimelineManager.GetComponent<TimelineUI>().despawnTimeline();
        GameObject.FindObjectOfType<CanvasClickHandler>().EnableJoystick();
        xpBar.fillRect.GetComponent<Image>().color = xpColor;
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
        weaponBuilders = AttackLibrary.getAttackBuilders();
        playerStatUpgrades = PlayerStatsLibrary.GetStatGameObjects();
        weaponStatUpgrades = AttackStatsLibrary.GetStatGameObjects();

        hasRolled = false;
        xpColor = xpBar.fillRect.GetComponent<Image>().color;
        panel.SetActive(false);
        playerStats = FindObjectOfType<StatsHandler>();
    }

    public AttackStats[] GetAttackStats()
    {
        return FindObjectOfType<AttackHandler>().attacks
            .SelectMany(a => a.weaponUpgrades)
            .ToArray();
    }

    public AttackStats[] getAttackSetStats()
    {
        return FindObjectOfType<AttackHandler>().attacks
            .Select(a => a.weaponSetType)
            .SelectMany(
                t =>
                    WeaponSetUpgradeMap.AttackStatsMap[t]
                        .Where(k => k.Key == (Rarity)Random.Range(0, 6))
                        .SelectMany(k => k.Value)
            )
            .ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (RerollBtn != null)
        {
            RerollBtn.GetComponentInChildren<TextMeshProUGUI>().text =
                "Reroll "
                + "("
                + RerollBtn.GetComponent<RollSwapHandler>().currentReroll.ToString()
                + ")";
        }
        if (SwapBtn != null)
        {
            SwapBtn.GetComponentInChildren<TextMeshProUGUI>().text =
                "Swap "
                + "("
                + SwapBtn.GetComponent<RollSwapHandler>().currentSwap.ToString()
                + ")";
        }

        if (playerStats.currentHealth <= 0)
        {
            ResumeGame();
        }
    }
}
