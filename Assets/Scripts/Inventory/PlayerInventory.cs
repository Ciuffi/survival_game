using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<string> startingWeaponNames = new List<string>();
    public List<int> startingWeaponRarities = new List<int>();
    public List<bool> startingWeaponIsPerm = new List<bool>();
    public List<int> startingWeaponDurabilities = new List<int>();

    private void Awake()
    {
        AttackLibrary.InitializeLibrary();
        DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        StartingInventory();
    }

    private void StartingInventory()
    {
        AddWeaponByName("Classic Rifle", 0, true, 0);
        AddWeaponByName("Double Barrel", 2, true, 0);
        AddWeaponByName("Gatling Gun", 2, true, 0);
        AddWeaponByName("Shotgun", 4, true, 0);
        AddWeaponByName("Impact Nova", 4, true, 0);
        AddWeaponByName("Petrify Nova", 4, true, 0);
        AddWeaponByName("Drain Scythe", 4, true, 0);
        AddWeaponByName("Revolver", 6, true, 0);
    }

    public void AddWeaponByName(string weaponName, int rarity, bool isPerm, int durability)
    {
        startingWeaponNames.Add(weaponName);
        startingWeaponRarities.Add(rarity);
        startingWeaponIsPerm.Add(isPerm); //not consumed
        startingWeaponDurabilities.Add(durability); //consumed on 0 - one time use is 1, permanent items should use 0
    }
}
