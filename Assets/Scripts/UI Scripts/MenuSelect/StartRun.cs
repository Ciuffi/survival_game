using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartRun : MonoBehaviour
{
    public string chosenName;
    public int chosenStage;
    public CharSelectController charController;
    public StageSelectController stageController;
    public InventoryUIManager inventoryController;
    public bool charSelected;
    public bool wpnSelected;
    public bool stageSelected;
    bool canStart;

    public GameObject stageSelectUI;

    private InventoryUIManager InvManager;
    PanelTransitionManager inventoryButton;

    private void Start()
    {
        PlayerPrefs.DeleteKey("CharacterName");
        PlayerPrefs.DeleteKey("SelectedWeapon");
        PlayerPrefs.DeleteKey("SelectedWeaponRarity");

        GetComponent<Image>().enabled = false;
        canStart = false;

        stageSelectUI.SetActive(false);
        InvManager = FindObjectOfType<InventoryUIManager>();
        inventoryController = FindObjectOfType<InventoryUIManager>();
        inventoryButton = GameObject.Find("Canvas_Btns/InventoryButton").GetComponent<PanelTransitionManager>();

    }
    private void Update()
    {
        wpnSelected = inventoryController.wpnSelected;
        charSelected = charController.GetComponent<CharSelectController>().hasSelected;
        stageSelected = stageController.GetComponent<StageSelectController>().hasSelected;

        if (wpnSelected && charSelected)
        {
            GetComponent<Image>().enabled = true;
        }

        if (charSelected && wpnSelected && stageSelected)
        {
            canStart = true;
        }

    }

    public void StartGame()
    {
        if (charSelected && wpnSelected && !stageSelectUI.activeInHierarchy)
        {
            stageSelectUI.SetActive(true);
            if (inventoryButton.isTransitioned)
            {
                inventoryButton.OnButtonClick();
            }
            return;
        }


        if (canStart)
        {
            string gameObjectName = chosenName;
            string newName = gameObjectName.EndsWith("(Clone)") ? gameObjectName.Substring(0, gameObjectName.Length - 7) : gameObjectName;
            PlayerPrefs.SetString("CharacterName", newName);

            PlayerPrefs.SetString("SelectedWeapon", InvManager.GetSelectedWeapon());
            PlayerPrefs.SetInt("SelectedWeaponRarity", InvManager.GetSelectedWeaponRarity());

            PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
            inventory.DecrementWeaponDurability();

            // Load the next scene
            if (chosenStage == 1)
            {
                SceneManager.LoadScene("Stage1");
            } else if (chosenStage == 2)
            {
                SceneManager.LoadScene("Stage2");
            }
        }
    }


}
