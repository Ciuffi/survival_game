using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public int xpPercent;
    float xpAmount;

    public float disappearSpeed;

    Rigidbody2D rb;
    PlayerMovement player;

    SpriteRenderer Sprite;
    private SpriteRenderer spriteRend;

    Color OGcolor;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;

    }

    private void FixedUpdate()
    {
        xpAmount = player.gameObject.GetComponent<StatsHandler>().nextXp * (xpPercent / 100);

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Player")
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Destroy(gameObject);

        }
    }

        // Update is called once per frame
        void Update()
    {

    }
}
