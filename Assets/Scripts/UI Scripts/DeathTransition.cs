using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathTransition : MonoBehaviour
{
    public static DeathTransition Instance;
    public Image transitionMask;
    public float delayBeforeStart = 0.5f;
    public float transitionDuration = 2.0f;
    public AnimationCurve circleRadiusCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isTransitionActive = false;

    public GameObject gameManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // This prevents the object from being destroyed between scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // only reassign if the loaded scene is a game scene
        if (scene.buildIndex != 0)
        {
            gameManager = FindObjectOfType<GameManager>().gameObject;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to sceneLoaded when this GameObject is enabled
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from sceneLoaded when this GameObject is disabled
    }

    public void StartTransition()
    {
        if (!isTransitionActive)
        {
            gameObject.SetActive(true);
            StartCoroutine(TransitionEffect());
        }
    }

    private IEnumerator TransitionEffect()
    {
        isTransitionActive = true;
        gameObject.SetActive(true);

        yield return new WaitForSeconds(delayBeforeStart);

        float timer = 0.0f;

        while (timer < transitionDuration)
        {
            float progress = timer / transitionDuration;
            float fillAmount = circleRadiusCurve.Evaluate(progress);
            transitionMask.fillAmount = fillAmount;

            timer += Time.deltaTime;
            yield return null;
        }

        isTransitionActive = false;
        gameManager.GetComponent<GameManager>().playerDeathScreen();
    }
}