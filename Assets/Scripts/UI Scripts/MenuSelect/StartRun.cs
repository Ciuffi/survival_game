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
    bool charSelected;
    bool stageSelected;
    bool canStart;

    public GameObject stageSelectUI;

    private InventoryUIManager InvManager;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        GetComponent<Image>().enabled = false;
        canStart = false;

        stageSelectUI.SetActive(false);
        InvManager = FindObjectOfType<InventoryUIManager>();

    }
    private void Update()
    {
        charSelected = charController.GetComponent<CharSelectController>().hasSelected;
        stageSelected = stageController.GetComponent<StageSelectController>().hasSelected;

        if (charSelected && stageSelected)
        {
            canStart = true;
        }

    }

    public void StartGame()
    {
        if (charSelected && !stageSelectUI.activeInHierarchy)
        {
            stageSelectUI.SetActive(true);
            return;
        }


        if (canStart)
        {
            string gameObjectName = chosenName;
            string newName = gameObjectName.EndsWith("(Clone)") ? gameObjectName.Substring(0, gameObjectName.Length - 7) : gameObjectName;
            PlayerPrefs.SetString("CharacterName", newName);

            PlayerPrefs.SetString("SelectedWeapon", InvManager.GetSelectedWeapon());
            PlayerPrefs.SetInt("SelectedWeaponRarity", InvManager.GetSelectedWeaponRarity());

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
