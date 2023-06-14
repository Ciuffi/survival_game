using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class UpgradeHandler : MonoBehaviour, IPointerDownHandler
{
    public Upgrade upgrade;
    private AttackHandler playerAttacks;
    private StatsHandler playerStats;
    private LevelUpManager levelUpManager;

    public float uiDelay;
    public bool startDelay;
    public bool delayFinished;
    private float timer; // make timer a class member variable

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
                        levelUpManager.SignalItemChosen();
                    }
                    else
                    {
                        return;
                    }
                    break;
                case UpgradeType.PlayerStats:
                    playerStats.AddStat((PlayerCharacterStats)upgrade);
                    levelUpManager.SignalItemChosen();
                    break;
                case UpgradeType.WeaponSetStat:
                    playerAttacks.attacks
                        .Where(a => a.weaponSetType == ((AttackStats)upgrade).weaponSetType)
                        .ToList()
                        .ForEach(a => a.AddWeaponUpgrade((AttackStats)upgrade));
                    playerAttacks.WeaponSetAttackStats.Add((AttackStats)upgrade);
                    levelUpManager.SignalItemChosen();
                    break;
                case UpgradeType.WeaponStat:
                    playerAttacks.attacks
                        .Where(a => a.name.Contains(((AttackStats)upgrade).AttackName))
                        .ToList().ForEach(a => a.AddWeaponUpgrade((AttackStats)upgrade));
                    levelUpManager.SignalItemChosen();
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerAttacks = player.GetComponent<AttackHandler>();
        playerStats = player.GetComponent<StatsHandler>();
        levelUpManager = GameObject.FindObjectOfType<LevelUpManager>();
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
