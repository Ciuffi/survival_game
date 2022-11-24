using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLockedRotation : MonoBehaviour
{
    private VirtualJoystick VJ;

    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (VJ.InputAngle < 0) //input right
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (VJ.InputAngle > 0) //input left
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
