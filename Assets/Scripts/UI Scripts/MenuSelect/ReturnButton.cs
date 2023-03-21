using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public GameObject stageSelectUI;
    public GameObject stageController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ExitStageSelect()
    {
        GameObject previouslySelected = GameObject.FindGameObjectWithTag("SelectedStage");
        if (previouslySelected != null)
        {
            StageButton previouslySelectedButton = previouslySelected.GetComponent<StageButton>();
            previouslySelectedButton.Clear();
            
        }

        stageSelectUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
