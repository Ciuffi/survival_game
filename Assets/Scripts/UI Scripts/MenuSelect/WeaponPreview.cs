using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponPreview : MonoBehaviour
{
    Image sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Image>();
        sprite.sprite = null;
        sprite.color = new Color(0, 0, 0, 0);
    }

    public void UpdatePreview(string name, int rarity)
    {
        AttackBuilder weapon = AttackLibrary.GetAttackBuilder(name);
        Attack finalWeapon = weapon.Build((Rarity)rarity);

        sprite.sprite = finalWeapon.GetWeaponSprite();
        sprite.color = Color.white;
    }
}
