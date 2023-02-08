using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeLootHandler : MonoBehaviour, IPointerDownHandler
{
    public Upgrade upgrade;
    private AttackHandler playerAttacks;
    private StatsHandler playerStats;
    private LootBoxManager lootManager;


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
        if (upgrade.GetUpgradeType() == UpgradeType.StatBoost)
        {

            GameObject newStat = Instantiate(upgrade.GetTransform().gameObject, playerStats.transform.position, Quaternion.identity);
            playerStats.AddStat(newStat);
            lootManager.SignalItemChosen();


        }
        else
        {
            if (playerAttacks.attacks.Count < 6)
            {
                GameObject newWeapon = Instantiate(upgrade.GetTransform().gameObject, playerAttacks.transform.position, Quaternion.identity);
                playerAttacks.AddWeapon(newWeapon);
                lootManager.SignalItemChosen();

            }
            else
            {
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
