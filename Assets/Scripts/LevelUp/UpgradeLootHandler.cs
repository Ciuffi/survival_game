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
        if (upgrade.GetUpgradeType() == UpgradeType.Weapon)
        {
            GameObject newWeapon = Instantiate(upgrade.GetTransform().gameObject, playerAttacks.transform.position, Quaternion.identity);
            playerAttacks.AddWeapon(newWeapon);
        }
        else
        {
            GameObject newStat = Instantiate(upgrade.GetTransform().gameObject, playerStats.transform.position, Quaternion.identity);
            playerStats.AddStat(newStat);
        }
        lootManager.SignalItemChosen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
