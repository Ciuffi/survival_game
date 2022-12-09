using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSpriteHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite newSprite;
    public SpriteRenderer thisSprite;
    void Start()
    {
        thisSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SwapSprite()
    {
        thisSprite.sprite = newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
