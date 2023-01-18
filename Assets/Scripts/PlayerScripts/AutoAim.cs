using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public float maxLength = 40.0f;
    public float width = 3f;
    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        // Create a variable to store the hit information
        RaycastHit2D hit;

        // Perform a raycast and store the hit information in the variable
        hit = Physics2D.Raycast(ray.origin, ray.direction, maxLength, enemyLayer);

        // Check if the raycast hit anything
        if (hit.collider != null)
        {
            // Get the enemy's position
            Vector3 enemyPosition = hit.collider.gameObject.transform.position;

            // Get the direction from the player to the enemy
            Vector3 direction = (enemyPosition - (Vector3)transform.position).normalized;

            // Log the enemy's position and direction
            Debug.Log("Enemy position: " + enemyPosition + ", direction: " + direction);
        }
    }
}
