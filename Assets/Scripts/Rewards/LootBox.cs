using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour
{
    public float health;
    public int xpPercent;
    float xpAmount;

    public float disappearSpeed;

    public GameObject DamagePopup;
    Rigidbody2D rb;
    PlayerMovement player;

    public Sprite OpenedSprite;
    SpriteRenderer Sprite;
    public Color tempColor;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement;
        Sprite = gameObject.GetComponent<SpriteRenderer>();
        tempColor = GetComponent<SpriteRenderer>().color;
    }

    private void FixedUpdate()
    {
        xpAmount = player.gameObject.GetComponent<StatsHandler>().nextXp * (xpPercent / 100);

        if (health <= 0 && tempColor.a > 0)
        {
            Sprite.sprite = OpenedSprite; 
            tempColor.a -= disappearSpeed * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = tempColor;


        } 
        
        if (tempColor.a <= 0)
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Destroy(gameObject);
        }

    }

    public void TakeDamage(float damageAmount, bool isCrit)
    {
            health -= damageAmount;
            Instantiate(DamagePopup, rb.position, Quaternion.identity);

            if (isCrit == true)
            {
                DamagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, true);
            }
            else
            {
                DamagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, false);
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
