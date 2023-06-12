using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public List<Weapon> weaponInventory = new List<Weapon>();
    public int selectedWeaponIndex; // index of the currently selected weapon
    InventoryUIManager inventoryUI;

    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //AttackLibrary.InitializeLibrary();
        LoadInventory();

        inventoryUI = FindObjectOfType<InventoryUIManager>();

        SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to sceneLoaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)  // Check if the scene being loaded is the menu scene
        {
            inventoryUI = FindObjectOfType<InventoryUIManager>();  // Find the InventoryUIManager in the new scene
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe from sceneLoaded when this GameObject is destroyed
    }


    public void StartingInventory()
    {
        AddWeapon(new Weapon("Petrify Nova", 0, false, 1));

        AddWeapon(new Weapon("Classic Rifle", 0, false, 1));

        //AddWeapon(new Weapon("Fire Starter", 0, false, 1));
        //AddWeapon(new Weapon("Consecrate", 0, false, 1));
        //AddWeapon(new Weapon("Earth Shock", 0, false, 1));

        //AddWeapon(new Weapon("Impact Mine", 0, false, 1));

        //AddWeapon(new Weapon("SMG", 0, false, 1));
        //AddWeapon(new Weapon("Revolver", 0, false, 1));
        //AddWeapon(new Weapon("Sniper Rifle", 0, false, 1));
        //AddWeapon(new Weapon("Gatling Gun", 0, false, 1));
        //AddWeapon(new Weapon("Suction Cannon", 0, false, 1));
        //AddWeapon(new Weapon("Double Barrel", 0, false, 1));
        //AddWeapon(new Weapon("Shotgun", 0, false, 1));
        //AddWeapon(new Weapon("Impact Nova", 0, false, 1));
        //AddWeapon(new Weapon("Petrify Nova", 0, false, 1));
        //AddWeapon(new Weapon("Suction Nova", 0, false, 1));
        //AddWeapon(new Weapon("Drain Scythe", 0, false, 1));
        //AddWeapon(new Weapon("Laser Beam", 0, false, 1));
        //AddWeapon(new Weapon("Shuriken", 0, false, 1));
        //AddWeapon(new Weapon("Pain Wheel", 0, false, 1));
        //AddWeapon(new Weapon("Impact Grenade", 0, false, 1));
        //AddWeapon(new Weapon("Smoke Grenade", 0, false, 1));
        //AddWeapon(new Weapon("Petrify Grenade", 0, false, 1));
        //AddWeapon(new Weapon("Suction Grenade", 0, false, 1));
        AddWeapon(new Weapon("Wind Blade", 0, false, 1));



    }

    private void OnApplicationQuit()
    {
        SaveInventory();
    }

    public void ResetInventory()
    {
        PlayerPrefs.DeleteKey("Weapons");
        weaponInventory.Clear();
        StartingInventory();
        SaveInventory();

        inventoryUI.ResetUI();
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


    }

    private void SaveInventory()
    {
        string weaponsJson = JsonConvert.SerializeObject(weaponInventory);
        PlayerPrefs.SetString("Weapons", weaponsJson);

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

    public void SetSelectedWeaponIndex(string weaponName, int weaponRarity)
    {
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            Weapon weapon = weaponInventory[i];
            if (weapon.name == weaponName && weapon.rarity == weaponRarity)
            {
                selectedWeaponIndex = i;
                return;
            }
        }

        Debug.LogError($"Weapon {weaponName} with rarity {weaponRarity} not found in inventory.");
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

    public bool WeaponExists(string weaponName, int weaponRarity)
    {
        return weaponInventory.Exists(w => w.name == weaponName && w.rarity == weaponRarity);
    }

    public void DecrementWeaponDurability()
    {
        Weapon selectedWeapon = GetSelectedWeapon();

        selectedWeapon.durability--;

        if (selectedWeapon.durability <= 0)
        {
            // Find the matching weapon in the inventory and remove it
            Weapon weaponToRemove = 
                weaponInventory.FirstOrDefault(weapon => weapon.name == selectedWeapon.name && weapon.rarity == selectedWeapon.rarity);

            if (weaponToRemove != null)
            {
                weaponInventory.Remove(weaponToRemove);
            }
            else
            {
                Debug.LogError("Weapon not found in inventory.");
            }
        }
        SaveInventory();
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