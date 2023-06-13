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
    private float baseSpeed;
    private float animSpeed;

    public Animator animator;

    public GameObject afterimage;
    public Animator afterimageAnim;
    private SpriteRenderer afterimageRend;
    private float speed;
    private float speedMultiplier;
    public Vector2 lastInputDirection { get; private set; }
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        direction = transform.up;
        canMove = true;
        oldSpeed = 0;
        localSpeed = 0;
        afterimageRend = afterimage.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }   

    void Move()
    {
        if (VJ.InputDirection.magnitude == 0)
        {
            afterimageRend.enabled = false;
            //lastInputDirection = Vector2.zero; // Joystick is not being used
            rb.velocity = Vector2.zero; // Setting the velocity to zero
            return;
        }

        afterimageRend.enabled = true;
        if (VJ.InputDirection.magnitude != 0)
        {
            lastInputDirection = new Vector2(VJ.InputDirection.x, VJ.InputDirection.y);
        }

        float InputY = VJ.InputDirection.y * 100;
        float InputX = VJ.InputDirection.x * 100;

        float y = Mathf.Abs(InputY) > DeadZonePercentage ? InputY / 100 : 0;

        float x = Mathf.Abs(InputX) > DeadZonePercentage ? InputX / 100 : 0;

        Vector2 move = new Vector2(x * localSpeed, y * localSpeed);
        Vector2 targetPos = rb.position + move * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, VJ.InputAngle));
        direction = VJ.InputDirection;
    }

    // Update the move speed
    public void SetAnimSpeed(float speed, float baseSpeed)
    {
        // Calculate the animator speed based on the move speed ratio
        float speedRatio = (speed * speedMultiplier / baseSpeed);
        float scalingFactor = 0.5f; // adjust this as needed
        float animSpeed = animator.speed * (1.0f + scalingFactor * (speedRatio - 1.0f));
        animator.speed = animSpeed;
        afterimageAnim.speed = animSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = GetComponent<StatsHandler>().stats.speed;
        speedMultiplier = GetComponent<StatsHandler>().stats.speedMultiplier + 1;

        if (canMove == true)
        {
            localSpeed = speed * speedMultiplier;
        }

        Move();

        if (canMove == true)
        {
            animator.SetBool("IsMoving", isMoving);
            afterimageAnim.SetBool("IsMoving", isMoving);
        }
    }

    public void StopMoving()
    {
        canMove = false;
        animator.SetBool("IsMoving", false);
        afterimageAnim.SetBool("IsMoving", false);
        oldSpeed = localSpeed;
        localSpeed = 0;
    }

    public void StartMoving()
    {
        animator.SetBool("IsMoving", isMoving);
        afterimageAnim.SetBool("IsMoving", isMoving);
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
