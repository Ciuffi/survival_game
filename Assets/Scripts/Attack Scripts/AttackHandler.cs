using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    public List<Attack> attacks;
    public AttackState attackState;
    public bool usingAttackBar;
    private GameObject attackContainer;
    private int attackIndex;
    public GameObject WeaponSprite;
    public GameObject WeaponOutline;
    public GameObject HandsSprite;
    
    private Slider attackBar;
    private Image attackBarImage;
    private Slider attackBar2;
    private Image attackBarImage2;
    private GameObject defaultWeapon;
    public Color[] colors = {
        new Color(255, 146, 8),
        Color.green,
        Color.blue,
        new Color(255, 231, 9),
        Color.red,
        Color.magenta
    };

    PlayerCharacterStats characterStats;

    private void MatchCharacter()
    {
        string storedName = PlayerPrefs.GetString("CharacterName");
        GameObject[] characters = Resources.LoadAll<GameObject>("PlayerCharacters");

        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {
                characterStats = obj.GetComponent<PlayerCharacterStats>();
                break;
            }
        }
    }

    IEnumerator HandleAttackSlider(float castTime)
    {
        float timer = 0;
        while (true)
        {
            if (timer == 0)
            {
                attackBarImage.color = colors[attackIndex];
                attackBarImage2.color = colors[attackIndex];
            }

            Color currentColor = attackBarImage.color;
            currentColor.a = 0.175f;
            attackBarImage.color = currentColor;
            attackBarImage2.color = currentColor;

            timer += Time.deltaTime;
            float progress = Mathf.Clamp01((timer-0.2f) / castTime);
            attackBar.value = progress ;
            attackBar2.value = progress;
            yield return new WaitForEndOfFrame();
            if (timer >= castTime + 0.2f)
            {
                timer = 0;
                yield break;
            }
        }
    }

    public void triggerRecoil()
    {
        WeaponSprite.GetComponent<WpnSpriteRecoil>().Recoil();
    }

    public void triggerWpnOff()
    {
        Attack currentAttack = attacks[attackIndex];
        StartCoroutine(WpnSpriteOff(currentAttack.comboWaitTime/1.5f));
    }

    IEnumerator WpnSpriteOff(float duration)
    {
       
        WeaponSprite.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        WeaponSprite.GetComponent<SpriteRenderer>().enabled = true;
    }


    IEnumerator Attack()
    {
        while (true)
        {

            attackState = AttackState.Casting;
            if (attacks.Count == 0) yield return null;
            Attack currentAttack = attacks[attackIndex];
            WeaponSprite.GetComponent<SpriteRenderer>().sprite = currentAttack.GetComponent<Attack>().weaponSprite;
            WeaponOutline.GetComponent<SpriteRenderer>().sprite = currentAttack.GetComponent<Attack>().weaponSprite;


            //swap animation
            HandsSprite.GetComponent<Animator>().SetBool("IsSwap", true);          
            yield return new WaitForSeconds(0.3f);
            HandsSprite.GetComponent<Animator>().SetBool("IsSwap", false);
            WeaponSprite.GetComponent<SpriteRenderer>().enabled = true;
            WeaponOutline.GetComponent<SpriteRenderer>().enabled = true;
            HandsSprite.GetComponent<SpriteRenderer>().enabled = false;
            WeaponSprite.GetComponent<Collider2D>().enabled = false;


            //casting
            if (usingAttackBar) StartCoroutine(HandleAttackSlider(currentAttack.castTime - 0.4f));
            yield return new WaitForSeconds(currentAttack.castTime);

            //attacking
            StopCoroutine("HandleAttackSlider");
            attackState = AttackState.Attacking;
            if (currentAttack != null) currentAttack.Shoot();
            yield return new WaitForSeconds(currentAttack.attackTime);

            //recovering
            attackState = AttackState.Recovery;
            attackIndex++;
     
            if (attackIndex >= attacks.Count)
            {
                attackIndex = 0;
            }
            if (currentAttack.recoveryTime > 0) yield return new WaitForSeconds(currentAttack.recoveryTime);

            HandsSprite.GetComponent<SpriteRenderer>().enabled = true;
            WeaponSprite.GetComponent<SpriteRenderer>().enabled = false;
            WeaponOutline.GetComponent<SpriteRenderer>().enabled = false;
            WeaponSprite.GetComponent<Collider2D>().enabled = true;


            if (currentAttack.thrownWeapon != null)
            {
                //throw animation
                HandsSprite.GetComponent<Animator>().SetBool("IsThrow", true);
                yield return new WaitForSeconds(0.3f);
                currentAttack.ThrowWeapon();
                HandsSprite.GetComponent<Animator>().SetBool("IsThrow", false);
            }
            
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        weapon.transform.parent = attackContainer.transform;
        attacks.Add(weapon.GetComponent<Attack>());
        weapon.GetComponent<Attack>().owner = GetComponent<Attacker>();
    }

    public void ResetWeapons()
    {
        StopCoroutine(Attack());

        foreach (Transform trans in attackContainer.transform)
        {
            Destroy(trans.gameObject);
        }
        attacks.Clear();
        attackIndex = 0;

        GameObject newWeapon = Instantiate(defaultWeapon, transform.position, Quaternion.identity);

        AddWeapon(newWeapon);
        WeaponSprite.GetComponent<SpriteRenderer>().sprite = newWeapon.GetComponent<Attack>().weaponSprite;
        WeaponOutline.GetComponent<SpriteRenderer>().sprite = newWeapon.GetComponent<Attack>().weaponSprite;

        StartCoroutine(Attack());
    }



    // Start is called before the first frame update
    void Start()
    {
        attackIndex = 0;
        attackBar = GameObject.Find("AttackBar").GetComponent<Slider>();
        attackBarImage = attackBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        attackBar2 = GameObject.Find("AttackBar2").GetComponent<Slider>();
        attackBarImage2 = attackBar2.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        attackContainer = new List<Transform>(GetComponentsInChildren<Transform>()).Find(t =>
        {
            return t.name == "Weapons";
        }).gameObject;

        
        MatchCharacter(); //inherit weapons
        foreach (GameObject weapon in characterStats.startingWeapons)
        {
            GameObject newWeapon = Instantiate(weapon, transform);
            AddWeapon(newWeapon.gameObject);

        }

        //add any other attacks
        new List<Attack>(attackContainer.GetComponentsInChildren<Attack>()).ForEach(a =>
        {
            AddWeapon(a.gameObject);
        });


        WeaponSprite.GetComponent<SpriteRenderer>().enabled = false;
        WeaponOutline.GetComponent<SpriteRenderer>().enabled = false;
        HandsSprite.GetComponent<SpriteRenderer>().enabled = true;

        StartCoroutine(Attack());

    }

}
