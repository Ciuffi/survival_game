using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootPopupAnimator : MonoBehaviour, IPointerDownHandler
{
    private GameObject panel;
    private GameObject panelAnimated;
    GameObject player;

    public Animator animator;
    public SpriteRenderer spriteRend;
    public ParticleSystem coinExplosion;

    private bool isOpen;
    public float disappearSpeed;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        animator.SetBool("IsOpen", false);
        panel = GameObject.Find("LootContainer");
        panelAnimated = GameObject.Find("LootPopup");
        player = GameObject.FindWithTag("Player");
        spriteRend = GetComponent<SpriteRenderer>();
        originalColor = spriteRend.color;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetBool("IsOpen", true);
            Instantiate(coinExplosion, transform.position, Quaternion.identity);
        } else
        {
            return;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            Color currentColor = spriteRend.color;
            currentColor.a -= disappearSpeed * Time.unscaledDeltaTime;
            spriteRend.color = currentColor;

            if (currentColor.a <= 0)
            {
                player.GetComponentInChildren<LootBoxManager>().ShowLootReward();
                TurnOff();
            }
        }
    }

    public void TurnOff()
    {
        isOpen = false;
        spriteRend.color = originalColor;
        panelAnimated.SetActive(false);
    }
}
