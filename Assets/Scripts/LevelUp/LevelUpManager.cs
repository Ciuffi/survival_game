using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

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
    public List<GameObject> upgrades;
    public List<GameObject> potentialUpgrades;
    public List<GameObject> weaponSetUpgrades;


    public bool isWeapon = false;
    List<string> previousUpgrades = new List<string>();

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
        setUpgrades();
        StartCoroutine(animateUpgrades()); 
    }

    public void swap()
    {
        if (isWeapon)
        {
            isWeapon = false;
        }
        else
        {
            isWeapon = true;
        }
        setUpgrades();
        StartCoroutine(animateUpgrades());
    }

    private IEnumerator animateUpgrades()
    {
        // Store the final scale of the upgrade windows
        List<Vector3> finalScales = new List<Vector3>();
        
        upgradeWindows.ForEach((u) =>
        {
            finalScales.Add(u.transform.localScale);
        });

        // Kill any ongoing animations and scale down the upgrade windows
        upgradeWindows.ForEach((u) =>
        {
            u.transform.DOKill();
            u.transform.localScale = new Vector3(0f, 0f, 1f);
        });

        // Wait for the end of frame to allow DOTween to reset the tweened properties
        yield return new WaitForEndOfFrame();

        // Animate the upgrade windows into place
        float delay = 0f;
        for (int i = 0; i < upgradeWindows.Count; i++)
        {
            // Create a sequence for each upgrade window
            Sequence sequence = DOTween.Sequence();

            // Append a scale tween to the sequence
            sequence.Join(upgradeWindows[i].transform.DOScale(finalScales[i], 0.5f).SetEase(Ease.OutExpo));
            sequence.SetUpdate(true);

            // Start the sequence after a delay
            sequence.Play().SetDelay(delay);

            delay += 0.1f; // Increase the delay for the next sequence
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
                    float rarityRoll = Random.Range(1, 101);  // adjust to 101 so that 100 can be included
                    float[] rarityChances = dropTable.guiltDropTables[guiltTracker.currentGuilt].dropRates;

                    float legendaryStart = rarityChances[3];
                    float epicStart = legendaryStart + rarityChances[2];
                    float rareStart = epicStart + rarityChances[1];
                    float commonStart = rareStart + rarityChances[0];

                    if (rarityRoll <= legendaryStart)
                    {
                        chosenRarity = Rarity.Legendary;
                    }
                    else if (rarityRoll <= epicStart)
                    {
                        chosenRarity = Rarity.Epic;
                    }
                    else if (rarityRoll <= rareStart)
                    {
                        chosenRarity = Rarity.Rare;
                    }
                    else  // this will catch anything that is not less than or equal to rareStart
                    {
                        chosenRarity = Rarity.Common;
                    }

                    GameObject GO = null;
                    while (GO == null)
                    {
                        AttackBuilder builder = weaponBuilders[Random.Range(0, weaponBuilders.Length)];
                        GO = builder.Build(chosenRarity).gameObject;
                        if (previousUpgrades.Contains(GO.name))
                        {
                            GO = null;
                        }
                    }
                    previousUpgrades.Add(GO.name);

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
                    u.GetComponentInChildren<TMP_Text>().color = rarityColors[index];

                    u.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = GO.GetComponent<Upgrade>().GetUpgradeDescription();

                    textComponents[3].text = GO.GetComponent<Attack>().weaponSetType.ToString();

                }
            );
        }
        else
        {
            foreach (UpgradeHandler u in upgradeWindows)
            {

                // Determine rarity based on guiltDropTables
                Rarity chosenRarity;
                float rarityRoll = Random.Range(1, 101);  // adjust to 101 so that 100 can be included
                float[] rarityChances = dropTable.guiltDropTables[guiltTracker.currentGuilt].dropRates;

                float legendaryStart = rarityChances[3];
                float epicStart = legendaryStart + rarityChances[2];
                float rareStart = epicStart + rarityChances[1];
                float commonStart = rareStart + rarityChances[0];

                if (rarityRoll <= legendaryStart)
                {
                    chosenRarity = Rarity.Legendary;
                }
                else if (rarityRoll <= epicStart)
                {
                    chosenRarity = Rarity.Epic;
                }
                else if (rarityRoll <= rareStart)
                {
                    chosenRarity = Rarity.Rare;
                }
                else  // this will catch anything that is not less than or equal to rareStart
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

                    // Weapon Set upgrade
                    upgrades = weaponSetUpgrades;
                    //foreach (WeaponSetType weaponSetType in Enum.GetValues(typeof(WeaponSetType)))
                    //{int count = weaponSetUpgrades.Count(u => u.GetComponent<AttackStatComponent>().stat.weaponSetType == weaponSetType);
                        //Debug.Log($"Count of {weaponSetType}: {count}");}

                    if (upgradeRoll < dropTable.existingWeaponOrSetChance)
                    {
                        //Debug.Log(upgradeRoll + "+ chance:" + dropTable.existingWeaponOrSetChance);

                        var attackHandler = FindObjectOfType<AttackHandler>();
                        // Get the unique weapon set types in the current attacks
                        var currentWeaponSetTypes = attackHandler.attacks.Select(a => a.weaponSetType).Distinct();
                        //Debug.Log($"Current WeaponSetTypes: {string.Join(", ", currentWeaponSetTypes)}");

                        // Filter upgrades that correspond to any of the current weapon set types
                        upgrades = upgrades
                            .Where(u => currentWeaponSetTypes.Contains(u.GetComponent<AttackStatComponent>().stat.weaponSetType))
                            .ToList();
                    }
                    upgrades = upgrades.Where(u => u.GetComponent<AttackStatComponent>().stat.GetRarity() == chosenRarity).ToList();
                    //Debug.Log(upgrades.Count);

                }
                else
                {

                    //specific weapon stat

                    //if (upgradeRoll < dropTable.existingWeaponOrSetChance) //owned weapon stat
                    //{
                        //Debug.Log("existing weapon stat");
                        //Debug.Log(upgradeRoll + "+ chance:" + dropTable.existingWeaponOrSetChance);

                        upgrades = GetAttackStatsGameObjects().ToList();

                    //}
                    //else //new weapon stat
                    //{
                        //Debug.Log("new weapon stat");
                        //Debug.Log(upgradeRoll + "+ chance:" + dropTable.existingWeaponOrSetChance);

                        //upgrades = GetPotentialUpgrades(weaponBuilders).ToList();
                        //dont filter
                    //}

                    upgrades = upgrades.Where(u => u.GetComponent<AttackStatComponent>().stat.GetRarity() == chosenRarity).ToList();
                }

                // ... continue from here as before, but using potentialUpgrades list
                // Remember to check if potentialUpgrades is not empty before proceeding
                GameObject GO = null;  
                    while (GO == null)
                    {
                        GO = upgrades[Random.Range(0, upgrades.Count)];
                        string baseName = Regex.Replace(GO.name, @"\s\d$", "");
                        if (previousUpgrades.Contains(baseName))
                        {
                            GO = null;
                        }
                    }
                    string baseName2 = Regex.Replace(GO.name, @"\s\d$", "");
                    previousUpgrades.Add(baseName2);

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
                    u.GetComponentInChildren<TMP_Text>().color = rarityColors[index];
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
                    u.GetComponentInChildren<TMP_Text>().color = rarityColors[index];
                    textComponents[1].color = rarityColors[index];
                    u.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = attackStatComponent.stat.description;

                    switch (u.upgrade.GetUpgradeType())
                    {
                        case UpgradeType.WeaponSetStat:
                            {
                                string upgradeTag = attackStatComponent.stat.weaponSetType.ToString();
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

    void DestroyPotentialUpgradeObjects()
    {
        foreach (var upgrade in potentialUpgrades)
        {
            Destroy(upgrade);
        }
        potentialUpgrades.Clear();
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

        foreach (var u in upgradeWindows)
            u.GetComponent<UpgradeHandler>().setActive();

        isWeapon = false; //always show upgrade
        setUpgrades();

        panel.SetActive(true);

        AnimateTitle();

        AnimateUpgradeWindows();

        AnimateButtons();

        //timeline stuff
        TimelineManager.GetComponent<TimelineUI>().addAttack();
        TimelineManager.GetComponent<TimelineUI>().spawnTimeline();
    }

    private void AnimateTitle()
    {
        GameObject title = panel.transform.GetChild(0).gameObject;
        Vector3 titlePosition = title.transform.localPosition;
        title.transform.localPosition += new Vector3(0, -2000, 0);

        Sequence titleSequence = DOTween.Sequence();
        titleSequence.SetUpdate(true);
        titleSequence.Append(title.transform.DOLocalMove(titlePosition, 0.5f).SetEase(Ease.OutExpo));
        titleSequence.Play();
    }

    private void AnimateUpgradeWindows()
    {
        List<Vector3> finalPositions = new List<Vector3>();
        List<Vector3> finalScales = new List<Vector3>();

        foreach (var u in upgradeWindows)
        {
            finalPositions.Add(u.transform.localPosition);
            finalScales.Add(u.transform.localScale);
            u.transform.localPosition += new Vector3(0, -2000, 0);
            u.transform.localScale = new Vector3(0f, 0f, 1f);
        }

        float delay = 0.1f;
        for (int i = 0; i < upgradeWindows.Count; i++)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(upgradeWindows[i].transform.DOLocalMove(finalPositions[i], 0.5f).SetEase(Ease.OutExpo));
            sequence.Join(upgradeWindows[i].transform.DOScale(finalScales[i], 1f).SetEase(Ease.OutExpo));
            sequence.SetUpdate(true);
            sequence.Play().SetDelay(delay);
            delay += 0.1f;
        }
    }

    private void AnimateButtons()
    {
        List<Vector3> finalBtnPositions = new List<Vector3>() {
        RerollBtn.transform.localPosition,
        SwapBtn.transform.localPosition,
        SkipBtn.transform.localPosition
    };

        RerollBtn.transform.localPosition += new Vector3(0, -500, 0);
        SwapBtn.transform.localPosition += new Vector3(0, -500, 0);
        SkipBtn.transform.localPosition += new Vector3(0, -500, 0);

        Sequence buttonSequence = DOTween.Sequence();
        buttonSequence.SetUpdate(true);
        buttonSequence.Append(RerollBtn.transform.DOLocalMove(finalBtnPositions[0], 0.5f).SetEase(Ease.OutExpo));
        buttonSequence.Join(SwapBtn.transform.DOLocalMove(finalBtnPositions[1], 0.5f).SetEase(Ease.OutExpo));
        buttonSequence.Join(SkipBtn.transform.DOLocalMove(finalBtnPositions[2], 0.5f).SetEase(Ease.OutExpo));
        buttonSequence.Play().SetDelay(0.1f * upgradeWindows.Count);
    }

    public void SignalItemChosen()
    {
        DestroyPotentialUpgradeObjects();
        panel.SetActive(false);
        TimelineManager.GetComponent<TimelineUI>().despawnTimeline();
        GameObject.FindObjectOfType<CanvasClickHandler>().EnableJoystick();
        xpBar.fillRect.GetComponent<Image>().color = xpColor;
        ResumeGame();
    }

    public void PauseGame()
    {
        GameObject.FindObjectOfType<PlayerMovement>().StopMoving();
        GameTime.instance.Pause();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        GameTime.instance.Unpause();
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

        rarityColors = weaponRarityPrefab.GetComponent<InventoryItem>().rarityColors;
        dropTable = GetComponent<DropTableUpgrades>();
        guiltTracker = FindObjectOfType<BasicSpawner>();

        hasRolled = false;
        xpColor = xpBar.fillRect.GetComponent<Image>().color;
        panel.SetActive(false);
        playerStats = FindObjectOfType<StatsHandler>();
        weaponSetUpgrades = getSetUpgrades().ToList();
    }

    public GameObject[] GetAttackStatsGameObjects()
    {
        var attackHandler = FindObjectOfType<AttackHandler>();
        GameObject statObject = new GameObject();

        foreach (Attack attack in attackHandler.attacks)
        {
            foreach (AttackStats stats in attack.weaponUpgrades)
            {
                GameObject upgradeGameObject = Instantiate(statObject);
                upgradeGameObject.AddComponent<AttackStatComponent>().stat = stats;
                upgradeGameObject.GetComponent<AttackStatComponent>().stat.statsContainer = upgradeGameObject;
                upgradeGameObject.name = upgradeGameObject.GetComponent<AttackStatComponent>().stat.name;
                potentialUpgrades.Add(upgradeGameObject);
            }
        }

        return potentialUpgrades.ToArray();
    }

    public GameObject[] GetPotentialUpgrades(AttackBuilder[] weaponBuilders)
    {
        GameObject statObject = new GameObject();

        // randomly select a weapon builder
        int randIndex = Random.Range(0, weaponBuilders.Length);
        AttackBuilder selectedBuilder = weaponBuilders[randIndex];

        // build the weapon at base rarity
        Attack weapon = selectedBuilder.Build(Rarity.Common);

        // convert the weapon upgrades into game objects
        foreach (AttackStats stats in weapon.weaponUpgrades)
        {
            GameObject upgradeGameObject = Instantiate(statObject);
            upgradeGameObject.AddComponent<AttackStatComponent>().stat = stats;
            upgradeGameObject.name = upgradeGameObject.GetComponent<AttackStatComponent>().stat.name;
            upgradeGameObject.GetComponent<AttackStatComponent>().stat.statsContainer = upgradeGameObject;

            potentialUpgrades.Add(upgradeGameObject);
        }

        return potentialUpgrades.ToArray();
    }

    public GameObject[] getSetUpgrades()
    {
        List<GameObject> weaponSetStats = new List<GameObject>();

        // Fetch all stats from the weapon set upgrade map
        foreach (var entry in WeaponSetUpgradeMap.AttackStatsMap)
        {
            WeaponSetType weaponSetType = entry.Key;
            Dictionary<Rarity, List<AttackStats>> rarityStats = entry.Value;

            foreach (var rarityEntry in rarityStats)
            {
                List<AttackStats> statsList = rarityEntry.Value;

                foreach (AttackStats upgrade in statsList)
                {
                    AttackStats clonedUpgrade = upgrade.Clone();

                    GameObject upgradeGameObject = new GameObject();
                    upgradeGameObject.AddComponent<AttackStatComponent>().stat = clonedUpgrade;
                    upgradeGameObject.name = upgradeGameObject.GetComponent<AttackStatComponent>().stat.name;
                    upgradeGameObject.GetComponent<AttackStatComponent>().stat.statsContainer = upgradeGameObject;
                    upgradeGameObject.GetComponent<AttackStatComponent>().stat.weaponSetType = weaponSetType;
                    weaponSetStats.Add(upgradeGameObject);
                }
            }
        }

        foreach (WeaponSetType weaponSetType in Enum.GetValues(typeof(WeaponSetType)))
        {
            int count = weaponSetStats.Count(u => u.GetComponent<AttackStatComponent>().stat.weaponSetType == weaponSetType);
            Debug.Log($"Count of {weaponSetType}: {count}");
        }

        return weaponSetStats.ToArray();
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
