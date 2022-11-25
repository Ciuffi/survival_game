using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour, Attacker
{
    private VirtualJoystick VJ;
    public float DeadZonePercentage;
    public Vector3 direction;
    public bool isMoving
    {
        get => VJ.InputDirection.magnitude != 0;
    }

    public bool canMove;
    public float oldSpeed;
    public float localSpeed;

    public Animator animator;
    public GameObject WeaponSprite;

    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        direction = transform.up;
        canMove = true;
        oldSpeed = 0;
        localSpeed = 0;
    }

    void Move()
    {

        if (VJ.InputDirection.magnitude == 0)
        {
            return;
        }
        float InputY = VJ.InputDirection.y * 100;
        float InputX = VJ.InputDirection.x * 100;
        float TransformY = transform.position.y;
        float TransformX = transform.position.x;
        float y = Mathf.Abs(InputY) > DeadZonePercentage ? InputY / 100 : 0;

        float x = Mathf.Abs(InputX) > DeadZonePercentage ? InputX / 100 : 0;

        transform.position = new Vector3(TransformX + x * localSpeed, TransformY + y * localSpeed, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, VJ.InputAngle));
        direction = VJ.InputDirection;

        WeaponSprite.GetComponent<WpnSpriteRotation>().InputXY(x, y);
    }


    

    // Update is called once per frame
    void Update()
    {
        float speed = GetComponent<StatsHandler>().speed;

        if (canMove == true)
        {
            localSpeed = speed;
        }

        Move();

        if (canMove == true)
        {
            animator.SetBool("IsMoving", isMoving);
        }

    }

    public void StopMoving()
    {
        canMove = false;
        animator.SetBool("IsMoving", false);
        oldSpeed = localSpeed;
        localSpeed = 0;
    }
    public void StartMoving()
    {
        animator.SetBool("IsMoving", isMoving);
        localSpeed = oldSpeed;
        canMove = true;
    }



    public Vector3 GetDirection()
    {
        return direction;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
