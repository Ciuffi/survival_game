using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartRun : MonoBehaviour
{
    public string chosenName;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void StartGame()
    {
        string gameObjectName = chosenName;
        string newName = gameObjectName.EndsWith("(Clone)") ? gameObjectName.Substring(0, gameObjectName.Length - 7) : gameObjectName;
        PlayerPrefs.SetString("CharacterName", newName); 

        // Load the next scene
        SceneManager.LoadScene("SampleScene");
    }


}
