using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddMoney : MonoBehaviour
{
    PlayerDataManager playerDataManager;
    public int goldToAdd = 500; // Amount of gold to add when the button is clicked

    // Start is called before the first frame update
    void Start()
    {
        playerDataManager = PlayerDataManager.Instance;
        // Get the Button component on this game object
        Button button = GetComponent<Button>();

        // Add an on-click listener to the button
        button.onClick.AddListener(AddGold);
    }

    private void AddGold()
    {
        // Add the specified amount of gold to the player's total
        playerDataManager.AddGold(goldToAdd);
    }

}
