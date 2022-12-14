using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasClickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private GameObject Joystick;
    private Image JoystickContainer;
    private VirtualJoystick VJ;
    private Vector3 startPosition;
    void Start()
    {
        Joystick = GameObject.Find("Joystick Container");
        JoystickContainer = Joystick.GetComponent<Image>();
        VJ = Joystick.GetComponent<VirtualJoystick>();
        startPosition = JoystickContainer.transform.position;
    }

    public void OnDrag(PointerEventData ped)
    {
        VJ.OnDrag(ped);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        Joystick.transform.position = new Vector3(ped.position.x - JoystickContainer.rectTransform.rect.width / 2f, ped.position.y - JoystickContainer.rectTransform.rect.height / 2f, 0);
        VJ.OnPointerDown(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        Joystick.transform.position = startPosition;
        VJ.OnPointerUp(ped);
    }

    public void EnableJoystick()
    {
        JoystickContainer.gameObject.SetActive(true);
        gameObject.GetComponent<Image>().raycastTarget = true;

    }
    public void DisableJoystick()
    {
        JoystickContainer.gameObject.SetActive(false);
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

}
