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
        GameObject firstCharacter = null;
        foreach (GameObject prefab in PlayerCharactersLibrary.getCharacters())
        {
            GameObject character = Instantiate(prefab, content.transform);
            character.GetComponent<StatComponent>().stat = prefab.GetComponent<StatComponent>().stat;

            // Find the SelectedImage child object and store a reference to it in the CharacterButton script
            GameObject selectedImage = character.transform.Find("Selected").gameObject;
            character.GetComponent<CharacterButton>().selectedImage = selectedImage;

            // Deactivate the SelectedImage object initially
            selectedImage.SetActive(false);

            if (firstCharacter == null)
            {
                firstCharacter = character;
            }
            Debug.Log("First Character: " + firstCharacter);
        }

        if (firstCharacter != null)
        {
            firstCharacter.GetComponent<CharacterButton>().SelectThisCharacter();
        }
    }

    public PlayerCharacterStats GetSelectedStats()
    {
        return selectedCharacter;
    }
}
