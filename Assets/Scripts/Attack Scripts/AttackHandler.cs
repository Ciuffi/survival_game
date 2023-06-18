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
    public WpnSpriteRecoil WeaponPrefab;
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

    public HashSet<AttackStats> WeaponSetAttackStats = new HashSet<AttackStats>();

    public List<TimelineUI> timelines = new List<TimelineUI>();
    IEnumerator attackCoroutine; // Declare this variable at the class level

    private void Awake()
    {
        timelines = new List<TimelineUI>(FindObjectsOfType<TimelineUI>());
    }

    // Start is called before the first frame update
    void Start()
    {
        attacks.Clear();
        MatchCharacter();

        Transform childTransform = transform.Find("Weapons");
        attackContainer = childTransform.gameObject;
        //AutoAimPrefab = GetComponent<AutoAim>();

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
        WeaponPrefab = FindObjectOfType<WpnSpriteRecoil>();


        StartCoroutine(LoadWeaponAndStartAttack());
    }
    IEnumerator LoadWeaponAndStartAttack()
    {
        // Wait until the next frame to ensure all Start methods have been called
        yield return null;

        LoadSelectedWeapon();

        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
    }

    private void MatchCharacter()
    {
        string storedName = PlayerPrefs.GetString("CharacterName");
        //Debug.Log(storedName);

        GameObject[] characters = PlayerCharactersLibrary.getCharacters();
        //Debug.Log(characters[0].name);

        foreach (GameObject obj in characters)
        {
            if (obj.name == storedName)
            {
                //Debug.Log("match");

                characterStats = obj.GetComponent<StatComponent>().stat;
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

    public void ResetAttackCycle()
    {
        StopCoroutine(attackCoroutine); // Stop the current attack cycle coroutine
        StartCoroutine(attackCoroutine); // Start a new attack cycle coroutine
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
        WeaponPrefab.Recoil();
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
                currentAttack.stats.is360,
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
        WeaponSetAttackStats.Where(stat => stat.weaponSetType == weapon.weaponSetType)
                    .ToList()
                    .ForEach(a => newWeapon.AddWeaponUpgrade(a));
        attacks.Add(newWeapon);

        //weapon basestats - exists
        //newWeapon baseStats - does not exist
    }

    public void RemoveAttack(Attack attackToRemove)
    {
        if (attacks.Contains(attackToRemove))
        {
            Debug.Log("Contains the attack");
               // Check if the attack to remove is the current one or comes before it in the list
        int removeIndex = attacks.IndexOf(attackToRemove);
        if (removeIndex <= attackIndex)
        {
            // Decrement attackIndex to ensure it remains pointing to the correct attack
            attackIndex--;
            // Ensure attackIndex doesn't go below zero
            if (attackIndex < 0)
            {
                attackIndex = 0;
            }
        }

        Destroy(attackToRemove.gameObject);
        attacks.Remove(attackToRemove);
        }

        // Refresh all timelines:
        foreach (var timeline in timelines)
        {
            if (timeline.gameObject.activeInHierarchy)
            {
                timeline.despawnTimeline();
                timeline.spawnTimeline();
            }
        }

        // Stop the attack cycle
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);

        // Start the attack cycle anew
        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
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

}
