using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasClickHandler : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private GameObject Joystick;
    private Image JoystickContainer;
    private VirtualJoystick VJ;
    void Start()
    {
        Joystick = GameObject.Find("Joystick Container");
        JoystickContainer = Joystick.GetComponent<Image>();
        VJ = Joystick.GetComponent<VirtualJoystick>();
        Debug.Log(VJ);

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
        Joystick.transform.position = new Vector3(-1000, -1000, 0);
        VJ.OnPointerUp(ped);
    }

}
