using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpnSpriteRecoil : MonoBehaviour
{
    public float recoilForce = 500f;
    public float recoilDuration = 0.1f;
    public float recoveryDuration = 0.2f;

    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private GameObject player;
    private Coroutine recoilCoroutine; // Store the reference to the running coroutine

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }

    public void Recoil()
    {
        if (recoilCoroutine != null)
        {
            StopCoroutine(recoilCoroutine); // Stop the last coroutine if it exists
        }

        originalPosition = transform.localPosition;
        recoilCoroutine = StartCoroutine(DoRecoil());
    }

    private IEnumerator DoRecoil()
    {
        // Apply recoil force
        var recoilDirection = -transform.up;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);

        // Wait for recoil duration
        yield return new WaitForSeconds(recoilDuration);

        // Calculate the target position to smoothly return to
        var targetPosition = originalPosition;

        // Continue the recovery until the target position is reached
        while (transform.localPosition != targetPosition)
        {
            // Interpolate towards the target position
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Time.deltaTime / recoveryDuration);

            yield return null;
        }
    }
}
