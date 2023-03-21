using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public GameObject content;
    public List<GameObject> stages;
    public GameObject selectedImagePrefab;
    private GameObject selectedImage;
    public bool hasSelected;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate each prefab and add it as a child of the content object
        foreach (GameObject prefab in stages)
        {
            GameObject stage = Instantiate(prefab, content.transform);

            // Find the SelectedImage child object and store a reference to it in the CharacterButton script
            GameObject selectedImage = stage.transform.Find("SelectedRect").gameObject;

            stage.GetComponent<StageButton>().selectedImage = selectedImage;

            // Deactivate the SelectedImage object initially
            selectedImage.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
