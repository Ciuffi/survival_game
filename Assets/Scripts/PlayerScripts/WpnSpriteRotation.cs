using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpnSpriteRotation : MonoBehaviour
{
    public GameObject weapon;
    public GameObject player;
    public float xOffset;
    public float yOffset;
    private VirtualJoystick VJ;
    public bool autoAiming;
    public GameObject currentTarget;
    public Vector3 direction;

    public GameObject spriteChild;
    public GameObject followSpritePrefab;
    private GameObject followSpriteInstance;
    private Vector3 originalFollowSpriteScale;

    void Start()
    {
        direction = transform.up;
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
    }


    void Update()
    {
        // Update weapon sprite position to follow the player
        transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z);

        if (autoAiming && currentTarget != null)
        {
            Vector3 targetDirection = (currentTarget.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);

            // Rotate the child GameObject instead of flipping the sprite
            spriteChild.transform.localRotation = Quaternion.Euler(0, 0, 90);

            spriteChild.GetComponent<SpriteRenderer>().flipX = false;
            // Flip the child sprite when the target is on the left half of the screen
            spriteChild.GetComponent<SpriteRenderer>().flipY = targetDirection.x < 0;
        }
        else
        {
            if (VJ.InputDirection.magnitude > 0f)
            {
                float inputAngle = VJ.InputAngle;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, inputAngle));

                // Rotate the child GameObject based on the calculated angle
                spriteChild.transform.localRotation = Quaternion.Euler(0, 0, VJ.InputAngle > 0 ? -90 : 90);

                // Flip the child sprite based on the input direction
                spriteChild.GetComponent<SpriteRenderer>().flipX = VJ.InputDirection.x < 0;
            }
        }
    }


    public void SetAutoAim(bool isAutoAiming, GameObject target)
    {
       
    autoAiming = isAutoAiming;
    if (currentTarget != target)
    {
        // Destroy the previous follow sprite if it exists
        if (followSpriteInstance != null)
        {
            Destroy(followSpriteInstance);
        }

        // Instantiate a new follow sprite and set it to follow the new target
        if (target != null)
        {
            followSpriteInstance = Instantiate(followSpritePrefab, target.transform.position, Quaternion.identity);
            followSpriteInstance.transform.SetParent(target.transform);

            originalFollowSpriteScale = followSpriteInstance.transform.localScale;  // Store the original scale

            Enemy enemy = target.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (enemy.isBasic)
                {
                        followSpriteInstance.transform.localPosition = new Vector3(0, -0.09f, 0);  // Adjust the Y value as needed
                        followSpriteInstance.transform.localScale = originalFollowSpriteScale * 0.3f;
                }
                else if (enemy.isElite)
                {
                        followSpriteInstance.transform.localPosition = new Vector3(0, -0.6f, 0);  // Adjust the Y value as needed

                        followSpriteInstance.transform.localScale = originalFollowSpriteScale * 1f;
                }
                else if (enemy.isBoss)
                {
                        followSpriteInstance.transform.localPosition = new Vector3(0, -0.8f, 0);  // Adjust the Y value as needed

                        followSpriteInstance.transform.localScale = originalFollowSpriteScale * 1.5f;
                }
                else
                {
                        followSpriteInstance.transform.localPosition = new Vector3(0, -0.4f, 0);  // Adjust the Y value as needed

                        followSpriteInstance.transform.localScale = originalFollowSpriteScale * 0.5f;
                }
            }
        }
    }
    currentTarget = target;
    }

    public Vector3 GetDirection()
    {
        if (autoAiming && currentTarget != null)
        {
            return (currentTarget.transform.position - transform.position).normalized;
        }
        else
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            Vector2 inputDirection = playerMovement.lastInputDirection;
            return Quaternion.Euler(0, 0, 0) * new Vector3(inputDirection.x, inputDirection.y, 0).normalized;
        }
    }


    public Transform GetTransform()
    {
        return transform;
    }
}