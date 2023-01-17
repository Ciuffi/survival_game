using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLockedRotation : MonoBehaviour
{
    private VirtualJoystick VJ;
    public bool isChest;
    public bool isPlayer;
    public bool isShadow;
    Vector3 SpawnPos;
    private Vector3 relativePosition;
    private Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        SpawnPos = transform.position;

        parentTransform = transform.parent;
        relativePosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));

        if (isChest == true)
        {
            transform.position = SpawnPos;

        }

        if (isPlayer == true)
        {

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
    void LateUpdate()
    {
        if (isShadow == true)
        {

            transform.localPosition = relativePosition;
        }
    }
}
