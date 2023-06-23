using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour
{
    public GameManager gameManager;
    public bool isResume;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }

        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    private void OnDestroy()
    {
        Button button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
        }
    }

    private void OnClick()
    {
        Debug.Log("click");
        if (!isResume)
        {
            gameManager.ShowPauseScreen();
        }
        else
        {
            gameManager.HidePauseScreen();
        }
    }
}