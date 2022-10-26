using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private VirtualJoystick VJ;
    public float Speed;
    public float DeadZonePercentage;
    public Vector3 direction;
    public List<Attack> attacks;
    private int attackIndex;
    private float AttackTimer;

    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        attackIndex = 0;
        attacks.Add(transform.GetComponentInChildren<Attack>());
        direction = transform.up;


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


        transform.position = new Vector3(TransformX + x * Speed, TransformY + y * Speed, 0);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, VJ.InputAngle));
        direction = VJ.InputDirection;
    }

    void Attack()
    {
        AttackTimer += Time.deltaTime;
        if (AttackTimer >= attacks[attackIndex].castTime)
        {
            attacks[attackIndex].Shoot();
            attackIndex++;
            if (attackIndex == attacks.Count)
            {
                attackIndex = 0;
            }
            AttackTimer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }
}
