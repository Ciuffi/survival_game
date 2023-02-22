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
    private float OGduration = 2;

    private bool isOpen;
    public float disappearSpeed;
    private Color originalColor;

    public GameObject goldCounter;
    private float extraIncrementTime;
    public int finalGold;

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
            animator.SetBool("IsOpen", true);
            extraIncrementTime = goldCounter.GetComponent<LootGoldCounter>().GetExtraIncrementTime(finalGold);
            goldCounter.GetComponent<LootGoldCounter>().finalGold = finalGold;
            goldCounter.GetComponent<LootGoldCounter>().finishedCounting = false;

            isOpen = true;

            ParticleSystem Ps = coinExplosion.GetComponent<ParticleSystem>();
            var main = Ps.main;
            main.duration = OGduration + extraIncrementTime;
            Instantiate(coinExplosion, transform.position, Quaternion.identity);

        }
        else
        {
            return;
        }
    }


    // Update is called once per frame
    void Update() 
    {
        if (isOpen)
        {
            if (goldCounter.GetComponent<LootGoldCounter>().finishedCounting == true)
            {
                Color currentColor = spriteRend.color;
                currentColor.a -= disappearSpeed * Time.unscaledDeltaTime;
                spriteRend.color = currentColor;
                StartCoroutine(Delay());
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSecondsRealtime(1.5f);

        player.GetComponentInChildren<LootBoxManager>().ShowLootReward();
        TurnOff();
    }

    public void TurnOff()
    {
        isOpen = false;
        spriteRend.color = originalColor;
        panelAnimated.SetActive(false);
    }
}
