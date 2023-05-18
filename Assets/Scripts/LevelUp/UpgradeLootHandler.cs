using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeLootHandler : MonoBehaviour, IPointerDownHandler
{
    public Upgrade upgrade;
    private AttackHandler playerAttacks;
    private StatsHandler playerStats;
    private LootBoxManager lootManager;
    public float uiDelay;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerAttacks = player.GetComponent<AttackHandler>();
        playerStats = player.GetComponent<StatsHandler>();
        lootManager = GameObject.FindObjectOfType<LootBoxManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (delayFinished)
        {
            switch (upgrade.GetUpgradeType())
            {
                case UpgradeType.Weapon:
                    if (playerAttacks.attacks.Count < 6)
                    {
                        playerAttacks.AddWeapon((Attack)upgrade);
                        lootManager.SignalItemChosen();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case UpgradeType.PlayerStats:
                    playerStats.AddStat((PlayerCharacterStats)upgrade);
                    lootManager.SignalItemChosen();
                    break;
                case UpgradeType.WeaponSetStat:
                    var weaponSet = WeaponSetUpgradeMap.AttackStatsMap.FirstOrDefault(
                        w => w.Value.Any(r => r.Value.Contains((AttackStats)upgrade))
                    );
                    playerAttacks.attacks
                        .Where(a => a.weaponSetType == weaponSet.Key)
                        .ToList()
                        .ForEach(a => a.AddWeaponUpgrade((AttackStats)upgrade));
                    lootManager.SignalItemChosen();
                    break;
                case UpgradeType.WeaponStat:
                    playerAttacks.attacks
                        .Where(a => a.weaponUpgrades.Any(upgrade =>
                            upgrade is AttackStats weaponUpgrade &&
                            weaponUpgrade.AttackName == a.name.Replace("(Clone)", "").Trim()))
                        .ToList()
                        .ForEach(a => a.AddWeaponUpgrade((AttackStats)upgrade));
                    lootManager.SignalItemChosen();
                    break;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startDelay)
        {
            timer += Time.unscaledDeltaTime; // increment timer each frame
            if (timer >= uiDelay)
            {
                startDelay = false;
                delayFinished = true;
            }
        }
    }

    public void setActive()
    {
        timer = 0f; // reset timer when panel is set active
        startDelay = true;
        delayFinished = false;
    }
}
