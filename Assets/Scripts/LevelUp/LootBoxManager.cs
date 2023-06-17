using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System.Text.RegularExpressions;
using DG.Tweening;

public class LootBoxManager : MonoBehaviour
{
    GameObject player;
    public UpgradeLootHandler upgradeWindow;
    public AttackBuilder[] weaponBuilders;
    public Upgrade[] weapons;
    public List<GameObject> playerStatUpgrades;
    public GameObject[] stats;
    public List<GameObject> potentialUpgrades;
    public List<GameObject> weaponSetUpgrades;
    public List<GameObject> upgrades;
    public bool isWeapon = true;
    List<string> previousUpgrades = new List<string>();

    private GameObject panel;
    private GameObject panelAnimated;
    public GameObject RerollBtn,
        SwapBtn,
        TimelineManager;
    public GameObject SkipBtn;

    public GameObject VFX;

    private GameObject lootPopup;
    public int finalGold;
    public GameObject lootOnTap;
    StatsHandler playerStats;
    DropTableUpgrades dropTable;
    BasicSpawner guiltTracker;

    public List<string> rarityNames = new List<string>()
    {
        "Common",
        "Rare",
        "Epic",
        "Legendary"
    };

    public List<Color> rarityColors;
    public GameObject weaponRarityPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        upgradeWindow = FindObjectOfType<UpgradeLootHandler>();

        panel = GameObject.Find("LootContainer");
        panelAnimated = GameObject.Find("LootPopup");
        weaponBuilders = AttackLibrary.getAttackBuilders();
        playerStatUpgrades = PlayerStatsLibrary.GetStatGameObjects();
        stats = PlayerStatsLibrary.GetStatGameObjects().ToArray();


        panel.SetActive(false);
        panelAnimated.SetActive(false);
        playerStats = player.GetComponent<StatsHandler>();
        rarityColors = weaponRarityPrefab.GetComponent<InventoryItem>().rarityColors;
        dropTable = GetComponent<DropTableUpgrades>();
        guiltTracker = FindObjectOfType<BasicSpawner>();
        weaponSetUpgrades = getSetUpgrades().ToList();
    }

    public void ShowLootUI()
    {
        Instantiate(VFX, transform.position, Quaternion.identity, transform);
        StartCoroutine(WaitForTime(0.6f));
    }

    public void ShowLootReward()
    {
        RerollBtn.GetComponent<RollSwapHandler>().setActive();
        SwapBtn.GetComponent<RollSwapHandler>().setActive();
        SkipBtn.GetComponent<SkipHandler>().setActive();
        upgradeWindow.GetComponent<UpgradeLootHandler>().setActive();

        isWeapon = true; //always set to weapon
        setUpgrades();
        panel.SetActive(true);

        AnimateTitle();

        AnimateUpgradeWindows();

        AnimateButtons();

        //eventually want to move this to on-confirm-selection, and add a new button to close menu
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
        Vector3 finalPosition = upgradeWindow.transform.localPosition; 
        Vector3 finalScale = upgradeWindow.transform.localScale; 

        upgradeWindow.transform.localPosition += new Vector3(0, -2000, 0);
        upgradeWindow.transform.localScale = new Vector3(0f, 0f, 1f);
        

        float delay = 0.1f;
            Sequence sequence = DOTween.Sequence();
            sequence.Append(upgradeWindow.transform.DOLocalMove(finalPosition, 0.5f).SetEase(Ease.OutExpo));
            sequence.Join(upgradeWindow.transform.DOScale(finalScale, 1f).SetEase(Ease.OutExpo));
            sequence.SetUpdate(true);
            sequence.Play().SetDelay(delay);
            delay += 0.1f;
       
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
        buttonSequence.Play().SetDelay(0.1f);
    }

    public void setUpgrades()
    {
        if (isWeapon)
        {
            // Determine rarity based on guiltDropTables
            Rarity chosenRarity;
            float rarityRoll = Random.Range(1, 101);  // adjust to 101 so that 100 can be included
            float[] rarityChances = dropTable.lootDropTables[guiltTracker.currentGuilt].dropRates;

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
                else if (GO != null)
                {

                    previousUpgrades.Add(GO.name);
                    upgradeWindow.GetComponent<UpgradeLootHandler>().upgrade = GO.GetComponent<Upgrade>();

                    upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite =
                        GO.GetComponent<Upgrade>().GetUpgradeIcon();

                    TMP_Text[] textComponents = upgradeWindow.GetComponentsInChildren<TMP_Text>();

                    string rarityText = chosenRarity.ToString();
                    textComponents[1].text = rarityText;

                    int index = rarityNames.IndexOf(rarityText) * 2;
                    textComponents[1].color = rarityColors[index];
                    upgradeWindow.GetComponentInChildren<TMP_Text>().color = rarityColors[index];

                    upgradeWindow.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = GO.GetComponent<Upgrade>().GetUpgradeDescription();

                    textComponents[3].text = GO.GetComponent<Attack>().weaponSetType.ToString();

                }
            }
        }
        else
        {

            // Determine rarity based on guiltDropTables
            Rarity chosenRarity;
            float rarityRoll = Random.Range(1, 101);  // adjust to 101 so that 100 can be included
            float[] rarityChances = dropTable.lootDropTables[guiltTracker.currentGuilt].dropRates;

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

    
            // Player Stat upgrade
            upgrades = playerStatUpgrades;
            upgrades = upgrades.Where(u => u.GetComponent<StatComponent>().stat.GetRarity() == chosenRarity).ToList();

            GameObject GO = null;
            while (GO == null)
            {
                GO = upgrades[Random.Range(0, upgrades.Count)];
                if (previousUpgrades.Contains(GO.name))
                {
                    GO = null;
                }
            }

            previousUpgrades.Add(GO.name);

            var statComponent = GO.GetComponent<StatComponent>();
            TMP_Text[] textComponents = upgradeWindow.GetComponentsInChildren<TMP_Text>();

            if (statComponent != null)
            {
                // It's a PlayerStat upgrade
                upgradeWindow.upgrade = statComponent.stat;

                string pattern = @"\s\d$";
                string editedName = Regex.Replace(GO.name, pattern, "");
                upgradeWindow.GetComponentInChildren<TMP_Text>().text = editedName;
                
                upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite = statComponent.stat.GetUpgradeIcon();

                string rarityText = chosenRarity.ToString();
                textComponents[1].text = rarityText;

                int index = rarityNames.IndexOf(rarityText) * 2;
                upgradeWindow.GetComponentInChildren<TMP_Text>().color = rarityColors[index];
                textComponents[1].color = rarityColors[index];
                upgradeWindow.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                textComponents[2].text = statComponent.stat.description;

                string upgradeTag = "RELIC";
                textComponents[3].text = upgradeTag;

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

    public AttackStats[] getAttackSetStats()
    {
        // Fetch all stats from the library
        var allStats = AttackStatsLibrary.GetStats();

        // Filter the stats where weaponSet = true
        var weaponSetStats = allStats.Where(stat => stat.weaponSet).ToArray();

        return weaponSetStats;
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

        return weaponSetStats.ToArray();
    }

    private IEnumerator WaitForTime(float waitTime)
    {
        //float startTime = Time.realtimeSinceStartup;
        //float endTime = startTime + waitTime;
        //while (Time.realtimeSinceStartup < endTime) {yield return null;}   //for running while game is paused

        yield return new WaitForSeconds(waitTime);
        GameObject.FindObjectOfType<CanvasClickHandler>().DisableJoystick();
        panelAnimated.SetActive(true);
        panelAnimated.GetComponent<LootGoldCounter>().ResetStats();
        lootOnTap.GetComponent<LootPopupAnimator>().finalGold = finalGold;
        PauseGame();


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
        Vector3 finalScale = upgradeWindow.transform.localScale;

        // Kill any ongoing animations and scale down the upgrade windows
        upgradeWindow.transform.DOKill();
        upgradeWindow.transform.localScale = new Vector3(0f, 0f, 1f);

        // Wait for the end of frame to allow DOTween to reset the tweened properties
        yield return new WaitForEndOfFrame();

        // Animate the upgrade windows into place
        float delay = 0f;
            // Create a sequence for each upgrade window
            Sequence sequence = DOTween.Sequence();
            // Append a scale tween to the sequence
            sequence.Join(upgradeWindow.transform.DOScale(finalScale, 0.5f).SetEase(Ease.OutExpo));
            sequence.SetUpdate(true);
            // Start the sequence after a delay
            sequence.Play().SetDelay(delay);
    }

    public void SignalItemChosen()
    {
        DestroyPotentialUpgradeObjects();
        previousUpgrades.Clear();
        TimelineManager.GetComponent<TimelineUI>().despawnTimeline();
        GameObject.FindObjectOfType<CanvasClickHandler>().EnableJoystick();
        panel.SetActive(false);
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
