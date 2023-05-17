using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    public List<Attack> attacks;
    public AttackState attackState;
    public bool usingAttackBar;
    private GameObject attackContainer;
    private int attackIndex;
    public GameObject WeaponPrefab;
    public SpriteRenderer WeaponSprite;
    public GameObject WeaponOutline;
    public GameObject HandsSprite;

    private Slider attackBar;
    private Image attackBarImage;
    private Slider attackBar2;
    private Image attackBarImage2;
    private GameObject defaultWeapon;
    public Color[] colors = { new Color(255, 146, 8), };

    public Color flashColor;

    PlayerCharacterStats characterStats;
    public AutoAim AutoAimPrefab;

    //attackbar wheel
    private Vector3 originalScale;
    private Vector3 maxScale = new Vector3(1.3f, 1.3f, 1.3f);
    public Image attackWheel;

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

    private void LoadSelectedWeapon()
    {
        string selectedWeaponName = PlayerPrefs.GetString("SelectedWeapon");
        int selectedWeaponRarity = PlayerPrefs.GetInt("SelectedWeaponRarity");

        // Instantiate the weapon prefab based on the name and add it to the player

        AttackBuilder weapon = AttackLibrary.GetAttackBuilder(selectedWeaponName);
        Attack finalWeapon = weapon.Build((Rarity)selectedWeaponRarity);
        AddWeapon(finalWeapon);
    }

    IEnumerator HandleAttackSlider(float castTime)
    {
        float timer = 0;
        bool isFlashing = false;
        Color originalColor = colors[0];
        float flashDuration = castTime / 6f; // Set the duration of the flash here

        while (true)
        {
            if (timer == 0)
            {
                attackBarImage.color = originalColor;
                attackBarImage2.color = originalColor;
            }

            if (castTime - timer <= flashDuration)
            {
                if (!isFlashing)
                {
                    // Start the flash
                    StartCoroutine(FlashColor(flashColor, flashDuration));
                    isFlashing = true;
                }
            }
            else if (isFlashing)
            {
                // End the flash
                StopCoroutine("FlashColor");
                attackBarImage.color = originalColor;
                attackBarImage2.color = originalColor;
                isFlashing = false;
            }

            float opacity = Mathf.Lerp(0.1f, 1f, attackBar.value);
            Color currentColor = attackBarImage.color;
            currentColor.a = opacity;
            attackBarImage.color = currentColor;
            attackBarImage2.color = currentColor;

            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / castTime);
            attackBar.value = progress;
            attackBar2.value = progress;
            yield return new WaitForEndOfFrame();
            if (timer >= castTime)
            {
                timer = 0;
                isFlashing = false;
                yield break;
            }
        }
    }

    IEnumerator HandleAttackWheel(float castTime)
    {
        float timer = 0;
        bool isFlashing = false;
        Color originalColor = colors[0];
        float flashDuration = castTime / 8f; // Set the duration of the flash here

        while (true)
        {
            if (timer == 0)
            {
                attackWheel.color = originalColor;
            }

            if (castTime - timer <= flashDuration)
            {
                if (!isFlashing)
                {
                    // Start the flash
                    StartCoroutine(FlashColor(flashColor, flashDuration));
                    isFlashing = true;
                }
            }
            else if (isFlashing)
            {
                // End the flash
                StopCoroutine("FlashColor");
                attackWheel.color = originalColor;
                isFlashing = false;
            }

            float opacity = Mathf.Lerp(0.1f, 1f, attackWheel.fillAmount);
            Color currentColor = attackWheel.color;
            currentColor.a = opacity;
            attackWheel.color = currentColor;

            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / castTime);
            attackWheel.fillAmount = progress;

            if (progress > 0.85f)
            {
                // If we're in the last 10% of the attackTime, start the scaling animation
                attackWheel.transform.DOScale(maxScale, 0.8f).SetEase(Ease.OutElastic);
            }
            else
            {
                // Reset the wheel to its original scale
                attackWheel.transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutElastic);
            }

            yield return new WaitForEndOfFrame();

            if (timer >= castTime)
            {
                timer = 0;
                isFlashing = false;
                yield break;
            }
        }
    }

    IEnumerator FlashColor(Color flashColor, float duration)
    {
        float timer = 0;
        //Color originalColor = attackBarImage.color;
        Color originalColor2 = attackWheel.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            //Color currentColor = Color.Lerp(originalColor, flashColor, t);
            //attackBarImage.color = currentColor;
            //attackBarImage2.color = currentColor;

            Color currentColor2 = Color.Lerp(originalColor2, flashColor, t);
            attackWheel.color = currentColor2;
            yield return null;
        }
    }

    public void triggerRecoil()
    {
        WeaponPrefab.GetComponent<WpnSpriteRecoil>().Recoil();
    }

    public void triggerWpnOff()
    {
        Attack currentAttack = attacks[attackIndex];
        StartCoroutine(WpnSpriteOff(currentAttack.stats.comboWaitTime / 1.5f));
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
            if (attacks.Count == 0)
                yield return null;
            Attack currentAttack = attacks[attackIndex];
            attackState = AttackState.Casting;

            WeaponSprite.GetComponent<SpriteRenderer>().sprite = currentAttack
                .GetComponent<Attack>()
                .weaponSprite;
            WeaponSprite.GetComponent<SpriteRenderer>().sprite = currentAttack
                .GetComponent<Attack>()
                .weaponSprite;

            //swap animation
            HandsSprite.GetComponent<Animator>().SetBool("IsSwap", true);
            //Debug.Log(currentAttack.stats);
            AutoAimPrefab.UpdateAimRange(
                currentAttack.stats.aimRange,
                currentAttack.stats.aimRangeAdditive,
                currentAttack.stats.isCone,
                currentAttack.stats.coneAngle
            );

            yield return new WaitForSeconds(0.3f);
            HandsSprite.GetComponent<Animator>().SetBool("IsSwap", false);
            WeaponSprite.GetComponent<SpriteRenderer>().enabled = true;
            WeaponOutline.GetComponent<SpriteRenderer>().enabled = true;
            HandsSprite.GetComponent<SpriteRenderer>().enabled = false;
            WeaponPrefab.GetComponent<Collider2D>().enabled = false;

            //casting
            attackWheel.gameObject.SetActive(true);
            if (usingAttackBar)
                StartCoroutine(HandleAttackWheel(currentAttack.stats.castTime));
            //StartCoroutine(HandleAttackSlider(currentAttack.stats.castTime));
            yield return new WaitForSeconds(currentAttack.stats.castTime);

            //attacking
            StopCoroutine("HandleAttackWheel");
            attackWheel.gameObject.SetActive(false);
            attackState = AttackState.Attacking;
            if (currentAttack != null)
                currentAttack.Shoot();

            yield return new WaitForSeconds(currentAttack.attackTime);
            //Debug.Log(currentAttack.attackTime);

            //recovering
            attackState = AttackState.Recovery;
            attackIndex++;

            if (attackIndex >= attacks.Count)
            {
                attackIndex = 0;
            }
            if (currentAttack.stats.recoveryTime > 0)
                yield return new WaitForSeconds(currentAttack.stats.recoveryTime);

            HandsSprite.GetComponent<SpriteRenderer>().enabled = true;
            WeaponSprite.GetComponent<SpriteRenderer>().enabled = false;
            WeaponOutline.GetComponent<SpriteRenderer>().enabled = false;
            WeaponPrefab.GetComponent<Collider2D>().enabled = true;

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

    public void AddWeapon(Attack weapon)
    {
        var newWeapon = Instantiate(weapon, attackContainer.transform);
        newWeapon.owner = GetComponent<Attacker>();
        newWeapon.baseStats = weapon.baseStats;
        newWeapon.stats = weapon.stats;
        newWeapon.weaponUpgrades = weapon.weaponUpgrades;

        attacks.Add(newWeapon);

        //Debug.Log(weapon.GetComponent<Attack>().baseStats.damage);

        //weapon basestats - exists
        //newWeapon baseStats - does not exist
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

        AddWeapon(defaultWeapon.GetComponent<Attack>());
        WeaponSprite.GetComponent<SpriteRenderer>().sprite = defaultWeapon
            .GetComponent<Attack>()
            .weaponSprite;
        WeaponOutline.GetComponent<SpriteRenderer>().sprite = defaultWeapon
            .GetComponent<Attack>()
            .weaponSprite;

        StartCoroutine(Attack());
    }

    // Start is called before the first frame update
    void Start()
    {
        MatchCharacter();

        Transform childTransform = transform.Find("Weapons");
        attackContainer = childTransform.gameObject;
        //AutoAimPrefab = FindObjectOfType<AutoAim>();

        LoadSelectedWeapon();

        attackIndex = 0;
        //attackBar = GameObject.Find("AttackBar").GetComponent<Slider>();
        //attackBarImage = attackBar.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        //attackBar2 = GameObject.Find("AttackBar2").GetComponent<Slider>();
        //attackBarImage2 = attackBar2.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        WeaponSprite.GetComponent<SpriteRenderer>().enabled = false;
        WeaponOutline.GetComponent<SpriteRenderer>().enabled = false;
        HandsSprite.GetComponent<SpriteRenderer>().enabled = true;
        attackWheel = GameObject.Find("Wheel").GetComponent<Image>();
        originalScale = attackWheel.transform.localScale;

        StartCoroutine(Attack());
    }
}
