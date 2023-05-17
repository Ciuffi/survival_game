using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weaponInventory = new List<Weapon>();
    public int selectedWeaponIndex; // index of the currently selected weapon

    private void Awake()
    {
        AttackLibrary.InitializeLibrary();
        DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        //LoadInventory();
        if (weaponInventory.Count == 0)
        {
            StartingInventory();
            SaveInventory();
        }
    }

    private void StartingInventory()
    {
        AddWeapon(new Weapon("Classic Rifle", 0, true, 0));
        //AddWeapon(new Weapon("SMG", 0, true, 0));
        //AddWeapon(new Weapon("Revolver", 0, true, 0));
        //AddWeapon(new Weapon("Sniper Rifle", 0, true, 0));
        //AddWeapon(new Weapon("Gatling Gun", 0, true, 0));
        //AddWeapon(new Weapon("Suction Cannon", 0, true, 0));
        //AddWeapon(new Weapon("Double Barrel", 0, true, 0));
        //AddWeapon(new Weapon("Shotgun", 0, true, 0));
        //AddWeapon(new Weapon("Impact Nova", 0, true, 0));
        //AddWeapon(new Weapon("Petrify Nova", 0, true, 0));
        //AddWeapon(new Weapon("Suction Nova", 0, true, 0));
        //AddWeapon(new Weapon("Wind Blade", 0, true, 0));
        //AddWeapon(new Weapon("Drain Scythe", 0, true, 0));
        //AddWeapon(new Weapon("Laser Beam", 0, true, 0));
        //AddWeapon(new Weapon("Shuriken", 0, true, 0));
        //AddWeapon(new Weapon("Pain Wheel", 0, true, 0));
        //AddWeapon(new Weapon("Impact Grenade", 0, true, 0));
        //AddWeapon(new Weapon("Smoke Grenade", 0, true, 0));
        //AddWeapon(new Weapon("Petrify Grenade", 0, true, 0));
        //AddWeapon(new Weapon("Suction Grenade", 0, true, 0));
        //AddWeapon(new Weapon("Wind Blade", 6, true, 0));


    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    private void LoadInventory()
    {
        string weaponsJson = PlayerPrefs.GetString("Weapons");
        if (!string.IsNullOrEmpty(weaponsJson))
        {
            weaponInventory = JsonConvert.DeserializeObject<List<Weapon>>(weaponsJson);
        }
        else
        {
            // If no saved inventory is found, populate with starting inventory
            StartingInventory();
        }

        // Load selected weapon index, default to 0
        selectedWeaponIndex = PlayerPrefs.GetInt("SelectedWeaponIndex", 0);
    }

    private void SaveInventory()
    {
        string weaponsJson = JsonConvert.SerializeObject(weaponInventory);
        PlayerPrefs.SetString("Weapons", weaponsJson);

        // Save selected weapon index
        PlayerPrefs.SetInt("SelectedWeaponIndex", selectedWeaponIndex);
    }

    public void AddWeapon(Weapon weapon)
    {
        weaponInventory.Add(weapon);
    }

    public void RemoveWeapon(int index)
    {
        weaponInventory.RemoveAt(index);

        // If the currently selected weapon is removed, switch to the first weapon in the list
        if (selectedWeaponIndex >= weaponInventory.Count)
        {
            selectedWeaponIndex = 0;
        }
    }

    public Weapon GetSelectedWeapon()
    {
        if (selectedWeaponIndex >= 0 && selectedWeaponIndex < weaponInventory.Count)
        {
            return weaponInventory[selectedWeaponIndex];
        }
        else
        {
            Debug.LogError("Selected weapon index out of range.");
            return null;
        }
    }

    public void SelectNextWeapon()
    {
        selectedWeaponIndex = (selectedWeaponIndex + 1) % weaponInventory.Count;
    }

    public void DecrementWeaponDurability()
    {
        Weapon selectedWeapon = GetSelectedWeapon();
        if (selectedWeapon != null)
        {
            selectedWeapon.durability--;
            SaveInventory();
        }
    }
}

public class Weapon
{
    public string name;
    public int rarity;
    public bool isPermanent;
    public int durability;

    public Weapon(string name, int rarity, bool isPermanent, int durability)
    {
        this.name = name;
        this.rarity = rarity;
        this.isPermanent = isPermanent;
        this.durability = durability;
    }
}