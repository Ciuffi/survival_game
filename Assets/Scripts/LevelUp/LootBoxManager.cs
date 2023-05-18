using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LootBoxManager : MonoBehaviour
{
    GameObject player;
    public UpgradeLootHandler upgradeWindow;
    public AttackBuilder[] weaponBuilders;
    public Upgrade[] weapons;
    public List<GameObject> playerStatUpgrades;
    public GameObject[] stats;
    public List<GameObject> upgrades;
    public bool isWeapon = true;
    private List<GameObject> previousUpgrades = new List<GameObject>();

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
        playerStats = FindObjectOfType<StatsHandler>();
        rarityColors = weaponRarityPrefab.GetComponent<InventoryItem>().rarityColors;
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

        //eventually want to move this to on-confirm-selection, and add a new button to close menu
        TimelineManager.GetComponent<TimelineUI>().addAttack();
        TimelineManager.GetComponent<TimelineUI>().spawnTimeline();

        isWeapon = true; //always set to weapon
        setUpgrades();
        panel.SetActive(true);
    }

    public void setUpgrades()
    {
        if (isWeapon)
        {

            GameObject GO = null;
            while (GO == null)
            {
                AttackBuilder builder = weaponBuilders[Random.Range(0, weaponBuilders.Length)];
                GO = builder.Build((Rarity)Random.Range(0, 3)).gameObject;

                if (previousUpgrades.Contains(GO))
                {
                    GO = null;
                }
                else if (GO != null)
                {

                    previousUpgrades.Add(GO);
                    upgradeWindow.GetComponent<UpgradeLootHandler>().upgrade = GO.GetComponent<Upgrade>();

                    upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                    upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite =
                        GO.GetComponent<Upgrade>().GetUpgradeIcon();

                    TMP_Text[] textComponents = upgradeWindow.GetComponentsInChildren<TMP_Text>();

                    string rarityText = GO.GetComponent<Upgrade>().GetRarity().ToString();
                    textComponents[1].text = rarityText;

                    int index = rarityNames.IndexOf(rarityText);
                    textComponents[1].color = rarityColors[index];
                    upgradeWindow.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                    textComponents[2].text = GO.GetComponent<Upgrade>().GetUpgradeDescription();

                    textComponents[3].text = GO.GetComponent<Attack>().attackType.ToString();

                }
            }
        }
        else
        {
            upgrades = new List<GameObject>(playerStatUpgrades);

            upgrades.AddRange(GetAttackStats().Select(s => s.GetTransform().gameObject).ToList());
            upgrades.AddRange(
                getAttackSetStats().Select(s => s.GetTransform().gameObject).ToList()
            );

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
            TMP_Text[] textComponents = upgradeWindow.GetComponentsInChildren<TMP_Text>();

            if (statComponent != null)
            {
                // It's a PlayerStat upgrade
                upgradeWindow.upgrade = statComponent.stat;
                upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite = statComponent.stat.GetUpgradeIcon();

                string rarityText = statComponent.stat.GetRarity().ToString();
                textComponents[1].text = rarityText;

                int index = rarityNames.IndexOf(rarityText);

                textComponents[1].color = rarityColors[index];
                upgradeWindow.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                textComponents[2].text = statComponent.stat.description;

                string upgradeTag = "ALL";
                textComponents[3].text = upgradeTag;

            }
            else if (attackStatComponent != null)
            {
                // It's either a WeaponStat upgrade or a WeaponSet upgrade
                upgradeWindow.upgrade = attackStatComponent.stat;
                upgradeWindow.GetComponentInChildren<TMP_Text>().text = GO.name;
                upgradeWindow.transform.Find("Image").GetComponent<Image>().enabled = true;
                upgradeWindow.transform.Find("Image").GetComponent<Image>().sprite = attackStatComponent.stat.GetUpgradeIcon();

                string rarityText = attackStatComponent.stat.GetRarity().ToString();
                textComponents[1].text = rarityText;

                int index = rarityNames.IndexOf(rarityText);

                textComponents[1].color = rarityColors[index];
                upgradeWindow.transform.Find("Image_Outline").GetComponent<Image>().color = rarityColors[index];

                textComponents[2].text = attackStatComponent.stat.description;

                switch (upgradeWindow.upgrade.GetUpgradeType())
                {
                    case UpgradeType.WeaponSetStat:
                        {
                            string upgradeTag = WeaponSetUpgradeMap.GetWeaponSetTypeForStat(attackStatComponent.stat).ToString();
                            textComponents[3].text = upgradeTag;
                        }
                        break;

                    case UpgradeType.WeaponStat:
                        if (upgradeWindow.upgrade is AttackStats weaponUpgrade)
                        {
                            string upgradeTag = weaponUpgrade.AttackName;
                            textComponents[3].text = upgradeTag;
                        }
                        break;
                }

            }
        } 
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

    public void reroll()
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
        GameObject.FindObjectOfType<PlayerMovement>().StopMoving();
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
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
