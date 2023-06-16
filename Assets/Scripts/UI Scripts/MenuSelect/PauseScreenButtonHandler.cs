using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseScreenButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject confirmationPopupPrefab;
    public UnityEvent onClickEvent;
    public bool isRestart;
    public string popupMessage;

    GameManager gameManager;

    private void Awake()
    {
        if (isRestart)
        {
            popupMessage = "Restart Run?";
        } else
        {
            popupMessage = "Exit to Menu?";
        }
    }

    private void Start()
    {
        StartCoroutine(WaitForGameManager());
    }

    private IEnumerator WaitForGameManager()
    {
        while (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            yield return null;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShowConfirmationPopup();
    }

    void ShowConfirmationPopup()
    {
        Initialize();

        GameObject popup = Instantiate(confirmationPopupPrefab);
        popup.transform.SetParent(transform.root, false);

        UnityEvent onClickEvent = new UnityEvent();
        onClickEvent.AddListener(gameManager.MenuReset);

        ConfirmationPopupController popupController = popup.GetComponent<ConfirmationPopupController>();
        popupController.Setup(onClickEvent, ClosePopup, popupMessage);
    }

    void Initialize()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void ClosePopup(GameObject popup)
    {
        Destroy(popup);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameManager = FindObjectOfType<GameManager>();
    }

}
