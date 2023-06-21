using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharSelectController : MonoBehaviour
{
    public GameObject content;
    public List<GameObject> characterPrefabs;
    public PlayerCharacterStats selectedCharacter;
    public GameObject selectedImagePrefab;
    private GameObject selectedImage;
    public bool hasSelected;
    PlayerDataManager playerData;

    void Start()
    {
        playerData = FindObjectOfType<PlayerDataManager>();

        // Instantiate each prefab and add it as a child of the content object
        GameObject firstCharacter = null;
        foreach (GameObject prefab in PlayerCharactersLibrary.getCharacters())
        {

            GameObject character = Instantiate(prefab, content.transform);
            StatComponent statComponent = character.AddComponent<StatComponent>();
            statComponent.stat = prefab.GetComponent<StatComponent>().stat;

            // Check if the character is unlocked
            if (playerData.unlockedCharactersNames.Contains(statComponent.stat.name))
            {
                statComponent.stat.isLocked = false;
            }
            character.GetComponent<CharacterButton>().stats = statComponent.stat;

            string characterName = character.name.EndsWith("(Clone)")
                ? character.name.Substring(0, character.name.Length - 7)
                : character.name;

            character.GetComponent<Image>().sprite = prefab.GetComponent<StatComponent>().stat.icon;

            // Find the SelectedImage child object and store a reference to it in the CharacterButton script
            GameObject selectedImage = character.transform.Find("Selected").gameObject;
            character.GetComponent<CharacterButton>().selectedImage = selectedImage;
            // Deactivate the SelectedImage object initially
            selectedImage.SetActive(false);

            if (firstCharacter == null)
            {
                firstCharacter = character;
            } else
            {
                character.GetComponent<Image>().color = character.GetComponent<CharacterButton>().lockedColor;
            }

            //Debug.Log("First Character: " + firstCharacter);

            characterPrefabs.Add(character);
        }

        if (firstCharacter != null)
        {
            firstCharacter.GetComponent<CharacterButton>().SelectThisCharacter();
            firstCharacter.GetComponent<CharacterButton>().UpdateCharacterPreview(firstCharacter.GetComponent<CharacterButton>().stats);
        }
    }

    public PlayerCharacterStats GetSelectedStats()
    {
        return selectedCharacter;
    }
}
