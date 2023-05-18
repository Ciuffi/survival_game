using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;

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

    public List<string> rarityNames = new List<string>()
    {
        "Common",
        "Rare",
        "Epic",
        "Legendary"
    };

    public List<Color> rarityColors;
    public GameObject weaponRarityPrefab;
    public DropTableUpgrades dropTable;
    public BasicSpawner guiltTracker;

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
                    // Determine rarity based on guiltDropTables
                    Rarity chosenRarity;
                    float rarityRoll = Random.Range(1,100);
                    float[] rarityChances = dropTable.guiltDropTables[guiltTracker.currentGuilt].dropRates;

                    if (rarityRoll <= rarityChances[3])
                    {
                        chosenRarity = Rarity.Legendary;
                    }
                    else if (rarityRoll <= rarityChances[2])
                    {
                        chosenRarity = Rarity.Epic;
                    }
                    else if (rarityRoll <= rarityChances[1])
                    {
                        chosenRarity = Rarity.Rare;
                    }
                    else 
                    {
                        chosenRarity = Rarity.Common;
                    }

                    GameObject GO = null;
                    while (GO == null)
                    {
                        AttackBuilder builder = weaponBuilders[Random.Range(0, weaponBuilders.Length)];
                        GO = builder.Build(chosenRarity).gameObject;
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
                        GO.GetComponent<Upgrade>().GetUpgradeIcon();

                    TMP_Text[] textComponents = u.GetComponentsInChildren<TMP_Text>();

                    string rarityText = chosenRarity.ToString();
                    textComponents[1].text = rarityText;

                    int index = rarityNames.IndexOf(rarityText) * 2;
                    textComponents[1].color = rarityColors[index];
                    u.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = GO.GetComponent<Upgrade>().GetUpgradeDescription();

                    textComponents[3].text = GO.GetComponent<Attack>().attackType.ToString();

                }
            );
        }
        else
        {
            foreach (UpgradeHandler u in upgradeWindows)
            {

                // Determine rarity based on guiltDropTables
                Rarity chosenRarity;
                float rarityRoll = Random.Range(1, 100);
                float[] rarityChances = dropTable.guiltDropTables[guiltTracker.currentGuilt].dropRates;

                if (rarityRoll <= rarityChances[3])
                {
                    chosenRarity = Rarity.Legendary;
                }
                else if (rarityRoll <= rarityChances[2])
                {
                    chosenRarity = Rarity.Epic;
                }
                else if (rarityRoll <= rarityChances[1])
                {
                    chosenRarity = Rarity.Rare;
                }
                else
                {
                    chosenRarity = Rarity.Common;
                }

                float typeRoll = Random.value;
                float upgradeRoll = Random.value;

                if (typeRoll < dropTable.playerStatChance)
                {
                    // Player Stat upgrade
                    upgrades = playerStatUpgrades;

                    upgrades = upgrades.Where(u => u.GetComponent<StatComponent>().stat.GetRarity() == chosenRarity).ToList();

                }
                else if (typeRoll < dropTable.playerStatChance + dropTable.weaponSetStatChance)
                {
                    Debug.Log(typeRoll + "weapon set");

                    // Weapon Set upgrade
                    upgrades = getAttackSetStats().Select(s => s.GetTransform().gameObject).ToList();

                    if (upgradeRoll < dropTable.existingWeaponOrSetChance)
                    {
                        var attackHandler = FindObjectOfType<AttackHandler>();
                        upgrades = upgrades
                            .Where(u =>
                                attackHandler.attacks.Any(a =>
                                    a.weaponSetType == WeaponSetUpgradeMap.GetWeaponSetTypeForStat(u.GetComponent<AttackStatComponent>().stat))
                            )
                            .ToList();
                    }

                    upgrades = upgrades.Where(u => u.GetComponent<AttackStatComponent>().stat.GetRarity() == chosenRarity).ToList();

                }
                else
                {
                    Debug.Log(typeRoll + "weapon stat");

                    // Weapon Stat upgrade
                    upgrades = GetAttackStats().Select(s => s.GetTransform().gameObject).ToList();
                    if (upgradeRoll < dropTable.existingWeaponOrSetChance)
                    {
                        var attackHandler = FindObjectOfType<AttackHandler>();
                        upgrades = upgrades
                            .Where(u => attackHandler.attacks
                                .Any(a => a.weaponUpgrades
                                    .Any(wu => wu.AttackName == u.GetComponent<AttackStatComponent>().stat.AttackName)))
                            .ToList();
                    }
                    upgrades = upgrades.Where(u => u.GetComponent<AttackStatComponent>().stat.GetRarity() == chosenRarity).ToList();
                }

                // ... continue from here as before, but using potentialUpgrades list
                // Remember to check if potentialUpgrades is not empty before proceeding

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

                var statComponent = GO.GetComponent<StatComponent>();
                var attackStatComponent = GO.GetComponent<AttackStatComponent>();

                if (statComponent != null)
                {
                    // It's a PlayerStat upgrade
                    u.upgrade = statComponent.stat;

                    string pattern = @"\s\d$";
                    string editedName = Regex.Replace(GO.name, pattern, "");
                    u.GetComponentInChildren<TMP_Text>().text = editedName;

                    u.transform.Find("Image").GetComponent<Image>().enabled = true;
                    u.transform.Find("Image").GetComponent<Image>().sprite = statComponent.stat.GetUpgradeIcon();
                    TMP_Text[] textComponents = u.GetComponentsInChildren<TMP_Text>();

                    string rarityText = statComponent.stat.GetRarity().ToString();
                    textComponents[1].text = rarityText;

                    int index = rarityNames.IndexOf(rarityText) * 2;

                    textComponents[1].color = rarityColors[index];
                    u.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = statComponent.stat.description;

                    string upgradeTag = "ALL";
                    textComponents[3].text = upgradeTag;

                }
                else if (attackStatComponent != null)
                {
                    // It's either a WeaponStat upgrade or a WeaponSet upgrade
                    u.upgrade = attackStatComponent.stat;

                    string pattern = @"\s\d$";
                    string editedName = Regex.Replace(GO.name, pattern, "");
                    u.GetComponentInChildren<TMP_Text>().text = editedName;

                    u.transform.Find("Image").GetComponent<Image>().enabled = true;
                    u.transform.Find("Image").GetComponent<Image>().sprite = attackStatComponent.stat.GetUpgradeIcon();
                    TMP_Text[] textComponents = u.GetComponentsInChildren<TMP_Text>();

                    string rarityText = attackStatComponent.stat.GetRarity().ToString();
                    textComponents[1].text = rarityText;

                    int index = rarityNames.IndexOf(rarityText) * 2;

                    textComponents[1].color = rarityColors[index];
                    u.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = attackStatComponent.stat.description;

                    switch (u.upgrade.GetUpgradeType())
                    {
                        case UpgradeType.WeaponSetStat:
                            {
                                string upgradeTag = WeaponSetUpgradeMap.GetWeaponSetTypeForStat(attackStatComponent.stat).ToString();
                                textComponents[3].text = upgradeTag;
                            }
                            break;

                        case UpgradeType.WeaponStat:
                            if (u.upgrade is AttackStats weaponUpgrade)
                            {
                                string upgradeTag = weaponUpgrade.AttackName;
                                textComponents[3].text = upgradeTag;
                            }
                            break;
                    }

                }
            }
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

        rarityColors = weaponRarityPrefab.GetComponent<InventoryItem>().rarityColors;
        dropTable = GetComponent<DropTableUpgrades>();
        guiltTracker = FindObjectOfType<BasicSpawner>();

        hasRolled = false;
        xpColor = xpBar.fillRect.GetComponent<Image>().color;
        panel.SetActive(false);
        playerStats = FindObjectOfType<StatsHandler>();
    }

    public AttackStats[] GetAttackStats()
    {
        List<Attack> attacks = (FindObjectOfType<AttackHandler>().attacks);
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
                        //.Where(k => k.Key == (Rarity)(Random.Range(0,3)*2))
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
