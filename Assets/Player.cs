using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private VirtualJoystick VJ;
    public float Speed;
    public float DeadZonePercentage;


    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
    }

    void move()
    {
        float InputY = VJ.InputDirection.y * 100;
        float InputX = VJ.InputDirection.x * 100;
        float TransformY = transform.position.y;
        float TransformX = transform.position.x;
        float y = Mathf.Abs(InputY) > DeadZonePercentage ? InputY / 100 : 0;

        float x = Mathf.Abs(InputX) > DeadZonePercentage ? InputX / 100 : 0;


        transform.position = new Vector3(TransformX + x * Speed, TransformY + y * Speed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
}
