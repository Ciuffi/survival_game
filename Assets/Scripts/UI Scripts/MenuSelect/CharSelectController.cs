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
            PlayerCharacterStats prefabStats = prefab.GetComponent<StatComponent>().stat;

            // Filter based on unlock level
            if (prefabStats.level <= playerData.playerLevel)
            {
                GameObject character = Instantiate(prefab, content.transform);
                StatComponent statComponent = character.AddComponent<StatComponent>();
                statComponent.stat = prefabStats;

                // Check if the character is unlocked
                if (playerData.unlockedCharactersNames.Contains(prefabStats.name))
                {
                    statComponent.stat.isLocked = false;
                }
                character.GetComponent<CharacterButton>().stats = statComponent.stat;

                string characterName = character.name.EndsWith("(Clone)")
                    ? character.name.Substring(0, character.name.Length - 7)
                    : character.name;

                character.GetComponent<Image>().sprite = prefabStats.icon;

                // Find the SelectedImage child object and store a reference to it in the CharacterButton script
                GameObject selectedImage = character.transform.Find("Selected").gameObject;
                character.GetComponent<CharacterButton>().selectedImage = selectedImage;
                selectedImage.SetActive(false); // Deactivate the SelectedImage object initially

                if (firstCharacter == null)
                {
                    firstCharacter = character;
                }
                else
                {
                    character.GetComponent<Image>().color = character.GetComponent<CharacterButton>().lockedColor;
                }

                characterPrefabs.Add(character);
            }
        }

        if (firstCharacter != null)
        {
            firstCharacter.GetComponent<CharacterButton>().SelectThisCharacter();
            firstCharacter.GetComponent<CharacterButton>().UpdateCharacterPreview(firstCharacter.GetComponent<CharacterButton>().stats);
        }
    }

    public void RefreshCharacterSelection()
    {
        // Clear existing characters
        foreach (GameObject character in characterPrefabs)
        {
            Destroy(character);
        }
        characterPrefabs.Clear();

        // Instantiate each prefab and add it as a child of the content object
        GameObject firstCharacter = null;
        foreach (GameObject prefab in PlayerCharactersLibrary.getCharacters())
        {
            PlayerCharacterStats prefabStats = prefab.GetComponent<StatComponent>().stat;

            // Filter based on unlock level
            if (prefabStats.level <= playerData.playerLevel)
            {
                GameObject character = Instantiate(prefab, content.transform);
                StatComponent statComponent = character.AddComponent<StatComponent>();
                statComponent.stat = prefabStats;

                // Check if the character is unlocked
                if (playerData.unlockedCharactersNames.Contains(prefabStats.name))
                {
                    statComponent.stat.isLocked = false;
                }
                character.GetComponent<CharacterButton>().stats = statComponent.stat;

                string characterName = character.name.EndsWith("(Clone)")
                    ? character.name.Substring(0, character.name.Length - 7)
                    : character.name;

                character.GetComponent<Image>().sprite = prefabStats.icon;

                // Find the SelectedImage child object and store a reference to it in the CharacterButton script
                GameObject selectedImage = character.transform.Find("Selected").gameObject;
                character.GetComponent<CharacterButton>().selectedImage = selectedImage;
                selectedImage.SetActive(false); // Deactivate the SelectedImage object initially

                if (firstCharacter == null)
                {
                    firstCharacter = character;
                }
                else
                {
                    character.GetComponent<Image>().color = character.GetComponent<CharacterButton>().lockedColor;
                }

                characterPrefabs.Add(character);
            }
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
