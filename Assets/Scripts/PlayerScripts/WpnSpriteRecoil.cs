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

    private void Update()
    {
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

        // Return to original position
        var t = 0f;
        while (t < recoveryDuration)
        {

            t += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, t / recoveryDuration);

            yield return null;
        }
    }
}
