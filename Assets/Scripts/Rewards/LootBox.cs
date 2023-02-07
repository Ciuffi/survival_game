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

   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        spriteRend = Sprite.GetComponent<SpriteRenderer>();
        OGcolor = Sprite.GetComponent<SpriteRenderer>().color;

        
    }

    private void FixedUpdate()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col == null)
        {
            return;
        }

        if (col.gameObject.tag == "Player")
        {
            player.GetComponentInChildren<LootBoxManager>().ShowLootUI();    
            Destroy(gameObject);

        }
    }

   


}
