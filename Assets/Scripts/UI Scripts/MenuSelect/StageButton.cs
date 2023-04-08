using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour, IPointerDownHandler
{
    public int stageID;
    public bool isLocked;

    public GameObject selectedImage;

    private StageSelectController stageSelector;

    public GameObject startBtn;
    private bool hasSelected;


    // Start is called before the first frame update
    void Start()
    {
        // Find the CharacterSelector component in the scene
        stageSelector = FindObjectOfType<StageSelectController>();
        startBtn = GameObject.Find("StartBtn");

        hasSelected = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!hasSelected)
        {
            hasSelected = true;
            stageSelector.GetComponent<StageSelectController>().hasSelected = hasSelected;
        }


        // Deselect the previously selected character, if any
        GameObject previouslySelected = GameObject.FindGameObjectWithTag("SelectedStage");
        if (previouslySelected != null)
        {
            StageButton previouslySelectedButton = previouslySelected.GetComponent<StageButton>();
            if (previouslySelectedButton != null)
            {
                previouslySelectedButton.Deselect();
            }
        }

        // Select this stage
        selectedImage.SetActive(true);
        gameObject.tag = "SelectedStage";

        // Update the selected stage in startRun
        startBtn.GetComponent<StartRun>().chosenStage = stageID;


    }

    public void Deselect()
    {
        selectedImage.SetActive(false);
        gameObject.tag = "Untagged";
    }

    public void Clear()
    {
        hasSelected = false;
        stageSelector.GetComponent<StageSelectController>().hasSelected = hasSelected;
        selectedImage.SetActive(false);
        gameObject.tag = "Untagged";
    }


}
