using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharSelectController : MonoBehaviour
{
    public GameObject content;
    public List<GameObject> characterPrefabs;
    public PlayerCharacterStats selectedCharacter;
    public GameObject selectedImagePrefab;
    private GameObject selectedImage;
    public bool hasSelected;

    void Start()
    {

        // Instantiate each prefab and add it as a child of the content object
        foreach (GameObject prefab in characterPrefabs)
        {
            GameObject character = Instantiate(prefab, content.transform);


            // Find the SelectedImage child object and store a reference to it in the CharacterButton script
            GameObject selectedImage = character.transform.Find("Selected").gameObject;
            character.GetComponent<CharacterButton>().selectedImage = selectedImage;

            // Deactivate the SelectedImage object initially
            selectedImage.SetActive(false);
        }

    }

    public PlayerCharacterStats GetSelectedStats()
    {
        return selectedCharacter;
    }
}