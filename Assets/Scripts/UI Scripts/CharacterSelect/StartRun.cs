using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartRun : MonoBehaviour
{
    public string chosenName;
    public CharSelectController charController;
    bool canStart;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        GetComponent<Image>().enabled = false;
        canStart = false;

    }
    private void Update()
    {
        canStart = charController.GetComponent<CharSelectController>().hasSelected;
    }

    public void StartGame()
    {
        if (canStart)
        {
            string gameObjectName = chosenName;
            string newName = gameObjectName.EndsWith("(Clone)") ? gameObjectName.Substring(0, gameObjectName.Length - 7) : gameObjectName;
            PlayerPrefs.SetString("CharacterName", newName);

            // Load the next scene
            SceneManager.LoadScene("SampleScene");
        }
    }


}
