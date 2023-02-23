using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LootBox : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;

    SpriteRenderer Sprite;
    private SpriteRenderer spriteRend;
    Color OGcolor;

    private GameObject goldManager;

    public int minGold;
    public int maxGold;
    public int finalGold;

    private bool hasTriggered = false;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;
        goldManager = GameObject.Find("GoldManager");
        finalGold = Random.Range(minGold, maxGold);
        anim = GetComponent<Animator>();
    }



    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Player" && !hasTriggered)
        {
            anim.SetBool("IsOpen", true);

            player.GetComponentInChildren<LootBoxManager>().ShowLootUI();
            player.GetComponentInChildren<LootBoxManager>().finalGold = finalGold;
            hasTriggered = true;
            StartCoroutine(delayGold());
        }
    }

    IEnumerator delayGold()
    {
        yield return new WaitForSeconds(0.5f);
        goldManager.GetComponent<GoldTracker>().IncreaseCount(finalGold);
        Destroy(gameObject);
    }

   


}
