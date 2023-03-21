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

    public void Setup(UnityEvent onConfirm, UnityAction<GameObject> onClose, string message)
    {
        messageText.text = message;
        confirmButton.onClick.AddListener(() => { onConfirm.Invoke(); onClose.Invoke(gameObject); });
        exitButton.onClick.AddListener(() => { onClose.Invoke(gameObject); });
    }
}
