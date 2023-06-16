using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationPopupController : MonoBehaviour
{
    public Button confirmButton;
    public Button exitButton;
    public TextMeshProUGUI messageText;
    private UnityEvent onConfirm;
    private UnityAction<GameObject> onClose;

    public void Setup(UnityEvent onConfirmEvent, UnityAction<GameObject> onCloseAction, string message)
    {
        onConfirm = onConfirmEvent;
        onClose = onCloseAction;

        messageText.text = message;
        confirmButton.onClick.AddListener(ConfirmAction);
        exitButton.onClick.AddListener(ClosePopup);
    }

    void ConfirmAction()
    {
        onConfirm?.Invoke();
        onClose?.Invoke(gameObject);
    }

    void ClosePopup()
    {
        onClose?.Invoke(gameObject);
    }
}
