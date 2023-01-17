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
    public GameObject HitEffect;
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
            SetAllCollidersStatus(false);
        }

        if (tempColor.a <= 0)
        {
            player.gameObject.GetComponent<StatsHandler>().GainXP(xpAmount);
            Destroy(gameObject);
        }

    }

    public void TakeDamage(float damageAmount, bool isCrit)
    {
        if (health <= 0) return;
        health -= damageAmount;
        Vector3 popupPosition = rb.position;
        popupPosition.x = Random.Range(rb.position.x - 0.075f, rb.position.x + 0.075f);
        popupPosition.y = Random.Range(rb.position.y, rb.position.y + 0.1f);

        Vector3 modifier = transform.position;
        modifier.x = Random.Range(-0.1f, 0.1f);
        modifier.y = Random.Range(-0.1f, 0.1f);

        DamagePopupText damagePopup = Instantiate(DamagePopup, popupPosition, Quaternion.identity).GetComponent<DamagePopupText>();
        Instantiate(HitEffect, transform.position + modifier, Quaternion.identity);

        if (isCrit == true)
        {
            damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, true);
        }
        else
        {
            damagePopup.GetComponent<DamagePopupText>().Setup(damageAmount, false);
        }
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider2D c in GetComponents<Collider2D>())
        {
            c.enabled = active;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
