using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LootBox : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;

    SpriteRenderer Sprite;
    private SpriteRenderer spriteRend;
    Color OGcolor;

    private GameObject goldManager;
    private BasicSpawner guiltTracker;

    public float stageScaling = 0.05f;
    float finalMultiplier;
    public List<int> minGold;
    public List<int> maxGold;
    public int finalGold;

    private bool hasTriggered = false;
    Animator anim;

    public float bounceHeight, bounceSpeed, bounceDecay;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        goldManager = GameObject.Find("GoldManager");
        anim = GetComponent<Animator>();
        StartBouncing(bounceHeight, bounceSpeed, bounceDecay);

        guiltTracker = FindObjectOfType<BasicSpawner>();

        Scene currentScene = SceneManager.GetActiveScene();
        int sceneIndex = currentScene.buildIndex;
        // Calculate the percentage increase based on the scene index
        finalMultiplier = 1f + (stageScaling * (sceneIndex - 1));


    }

    public void StartBouncing(float startHeight, float startSpeed, float decayRate)
    {
        StartCoroutine(BounceCoroutine(startHeight, startSpeed, decayRate));
    }

    private IEnumerator BounceCoroutine(float startHeight, float startSpeed, float decayRate)
    {
        float startY = transform.position.y;
        float bounceTimer = 0f;
        float bounceHeight = startHeight;
        float bounceSpeed = startSpeed;

        while (bounceHeight > 0f)
        {
            // update the bounce timer
            bounceTimer += Time.deltaTime * bounceSpeed;

            // calculate the new position based on the timer and height
            Vector3 newPos = transform.position;
            newPos.y = startY + Mathf.Sin(bounceTimer) * bounceHeight;

            // update the position
            transform.position = newPos;

            // reduce the bounce height over time
            bounceHeight -= decayRate * Time.deltaTime;
            if (bounceHeight < 0f)
            {
                bounceHeight = 0f;
            }

            yield return null;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Player" && !hasTriggered)
        {
            finalGold = Mathf.RoundToInt((Random.Range(minGold[guiltTracker.currentGuilt], maxGold[guiltTracker.currentGuilt])) * finalMultiplier);

            anim.SetBool("IsOpen", true);
            player.GetComponentInChildren<LootBoxManager>().ShowLootUI();
            hasTriggered = true;
            StartCoroutine(delayGold());
        }
    }

    IEnumerator delayGold()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponentInChildren<LootBoxManager>().finalGold = finalGold;
        goldManager.GetComponent<GoldTracker>().IncreaseCount(finalGold);
        Destroy(gameObject);
    }

   


}
