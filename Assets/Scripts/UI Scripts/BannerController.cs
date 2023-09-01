using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class BannerController : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text bannerText;

    public float flashDuration = 1.0f; // total time the flashing will last
    public float fadeOutDuration = 1.0f; // time it takes to fade out
    public float flashRate = 5f; // time between each flash (lower = faster)


    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void DisplayBannerWithText(string message)
    {
        bannerText.text = message;
        StartCoroutine(FlashAndFadeOut());
    }

    private IEnumerator FlashAndFadeOut()
    {
        float flashTimeElapsed = 0;
        bool isFadingIn = true;
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        // Flashing loop
        while (flashTimeElapsed < flashDuration)
        {
            if (isFadingIn)
            {
                canvasGroup.alpha += Time.deltaTime / flashRate;
                if (canvasGroup.alpha >= 1)
                {
                    isFadingIn = false;
                }
            }
            else
            {
                canvasGroup.alpha -= Time.deltaTime / flashRate;
                if (canvasGroup.alpha <= 0)
                {
                    isFadingIn = true;
                }
            }

            flashTimeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1; // Make sure alpha is at full before fading out

        // Fade out
        float fadeTimeElapsed = 0;
        while (fadeTimeElapsed < fadeOutDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, fadeTimeElapsed / fadeOutDuration);
            fadeTimeElapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        Destroy(gameObject); // Optional: Destroy the banner after fading out
    }
}