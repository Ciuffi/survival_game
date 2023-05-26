using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResetData : MonoBehaviour
{
    PlayerDataManager playerDataManager;
    PlayerInventory playerInventory;
    Button resetButton; // make sure to set this in the Inspector

    // Start is called before the first frame update
    void Start()
    {
        resetButton = GetComponent<Button>();
        playerDataManager = PlayerDataManager.Instance;
        playerInventory = PlayerInventory.Instance;

        resetButton.onClick.AddListener(ResetAllData);
    }

    // Function to reset all data
    void ResetAllData()
    {
        playerInventory.ResetInventory();
        playerDataManager.ResetData();
    }

}
