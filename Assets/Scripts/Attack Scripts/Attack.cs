using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Attack : MonoBehaviour, Upgrade
{
    public float damage;
    private float damageUP;

    public float spread;
    private float spreadUP;

    public float shotgunSpread;

    public float spray;
    public int sprayThreshold;
    private int shotsCount = 0;

    public float castTime;
    private float castTimeUP;
    public float attackTime;
    public float recoveryTime;
    private float recoveryTimeUp;

    public float range = 5;
    public float rangeUP;

    public int shotsPerAttack;
    private int shotsPerAttackUP;

    public int shotsPerAttackMelee;

    public bool cantMove;

    public float speed;
    private float speedUP;

    public float knockback;
    private float knockbackUP;

    public int pierce;
    private int pierceUP;

    public float critChance; // 1 = 100% crit chance, 0 = 0% crit chance
    public float critDmg; //1 = 100% of normal damage on a crit, 2 = 200% damage, etc.

    public bool shootOpppositeSide = false;

    public float projectileSize;
    private float OGprojectileSize;
    public float meleeSize;
    private float OGmeleeSize;

    public float multicastChance; //every 1.0f = one guarenteed multicast.
    public float multicastWaitTime;
    private float defaultMulticastWaitTime;
    public int multicastTimes;
    private int numMulticast = 0;
    private float multicastAlphaFade = 0.15f;
    private float multicastAlphaAmount;

    public GameObject projectile;
    private Vector3 scaleUP;

    public int rarity = 0; //0-common, 1-rare, 2-epic, 4-legendary
    public List<int> chosenNumbers = new List<int>();

    public Effect effect;
    public AttackTypes attackType;
    public Attacker owner;
    GameObject Player;
    GameObject Camera;

    public GameObject MeleeAttack;
    public int comboLength; // # of melee attacks instantiated in a row
    public float comboWaitTime;
    public float comboAttackBuff; //percent buff

    public float meleeShotsScaleUp; // scales up by % amount after each attack in the shotsPerattack
    public float meleeSpacer = 0.7f; //spacer for first melee attack
    public float meleeSpacerGap = 1f; //spacer added for subsequent shotsperattack
    public bool swapAnimOnAttack;
    private int attackAnimState = 0;

    public float shakeTime;
    public float shakeStrength;
    public float shakeRotation;

    public Sprite weaponSprite;
    public bool isAutoAim;
    public GameObject AutoAim;

    public GameObject thrownWeapon;
    public Sprite thrownSprite;
    public float thrownDamage;
    public float throwSpeed;
    private bool firstShot = true;

    public GameObject bulletCasing;
    public List<GameObject> MuzzleFlashPrefab;
    public float muzzleFlashXOffset;
    public float muzzleFlashYOffset;
    private VirtualJoystick VJ;

    private float OGspread,
        OGshotgunSpread,
        OGmulticastChance,
        OGcastTime,
        OGcomboWaitTime,
        OGrange,
        OGthrowSpeed;
    private int OGshotPerAttack,
        OGshotsPerAttackMelee,
        OGcomboLength;
    private bool OGshootOpposite;

    private Quaternion spreadDirection2;
    public float totalDamageDealt;

    // Start is called before the first frame update
    void Start()
    {
        totalDamageDealt = 0;

        if (projectile == null)
        {
            projectile = Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Attacker>();

        int y = rarity;
        GenerateRarity(y, 1, 5);


        if (chosenNumbers.Contains(1)) //Upgrade Type 1 - Damage
        {
            damage = damageUP;
        }

        if (chosenNumbers.Contains(2)) //Upgrade Type 2 - spread /+ shotsPerAttack
        {
            spread = spreadUP;
            shotsPerAttack = shotsPerAttackUP;
        }
        if (chosenNumbers.Contains(3)) //Upgrade Type 3 - castTime /+ startTime
        {
            castTime = castTimeUP;
            recoveryTime = recoveryTimeUp;
        }
        if (chosenNumbers.Contains(4)) //Upgrade Type 4 - Range /+ speed
        {
            range = rangeUP;
            speed = speedUP;
        }
        if (chosenNumbers.Contains(5)) //Upgrade Type 5 - Knockback
        {
            knockback = knockbackUP;
        }
        if (chosenNumbers.Contains(6)) //Upgrade Type 6 - Scale  
        {
            //projectile.transform.localScale = scaleUP;
        }

        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        defaultMulticastWaitTime = multicastWaitTime;

        OGmulticastChance = multicastChance;
        OGcastTime = castTime;
        OGshotPerAttack = shotsPerAttack;
        OGcomboLength = comboLength;
        OGcomboWaitTime = comboWaitTime;
        OGthrowSpeed = throwSpeed;
        OGshootOpposite = shootOpppositeSide;
        OGrange = range;
        OGprojectileSize = projectileSize;
        OGmeleeSize = meleeSize;
        OGshotsPerAttackMelee = shotsPerAttackMelee;
        OGspread = spread;
        OGshotgunSpread = shotgunSpread;

        CalculateStats();

    }

    public void OnDamageDealt(float damage)
    {
        totalDamageDealt += damage;
    }

    private void Update()
    {
        if (attackType == AttackTypes.Shotgun)
        {
            attackTime = 0 + (multicastTimes * multicastWaitTime);
        }
        else if (attackType == AttackTypes.Melee)
        {
            // Add the definition for Melee attack type
            attackTime = (comboLength - 1) * comboWaitTime + (spread * shotsPerAttack) + (multicastTimes * multicastWaitTime);
        }
        else
        {
            // Add the default definition for Projectile attack type
            attackTime = spread * shotsPerAttack + (multicastTimes * multicastWaitTime);
        }

    }
    public void CalculateStats()
    {
        //Debug.Log("calculatin");
        spread = OGspread * Player.GetComponent<StatsHandler>().spreadMultiplier;
        shotgunSpread = OGshotgunSpread + Player.GetComponent<StatsHandler>().shotgunSpread;

        multicastChance = OGmulticastChance + Player.GetComponent<StatsHandler>().multicastChance;
        castTime = OGcastTime * Player.GetComponent<StatsHandler>().castTimeMultiplier;
        shotsPerAttack = OGshotPerAttack + Player.GetComponent<StatsHandler>().shotsPerAttack;
        if (shotsPerAttack <= 0)
        {
            shotsPerAttack = 1;
        }
        shotsPerAttackMelee = OGshotsPerAttackMelee + Player.GetComponent<StatsHandler>().shotsPerAttackMelee;
        if (shotsPerAttackMelee < 0)
        {
            shotsPerAttackMelee = 0;
        }
        comboLength = OGcomboLength + Player.GetComponent<StatsHandler>().meleeComboLength;
        if (comboLength <= 0)
        {
            comboLength = 1;
        }
        comboWaitTime = OGcomboWaitTime * Player.GetComponent<StatsHandler>().meleeWaitTimeMultiplier;
        throwSpeed = OGthrowSpeed * Player.GetComponent<StatsHandler>().thrownSpeedMultiplier;
        range = OGrange * Player.GetComponent<StatsHandler>().rangeMultiplier;
        if (!shootOpppositeSide)
        {
            shootOpppositeSide = Player.GetComponent<StatsHandler>().shootOppositeSide;
        }
        projectileSize = Player.GetComponent<StatsHandler>().projectileSizeMultiplier;
        meleeSize = Player.GetComponent<StatsHandler>().meleeSizeMultiplier;

    }

    private void rollMulticast()
    {
        multicastTimes = 0; //reset


        if (multicastChance > 0)
        {
            if (multicastChance < 1)
            {
                float randomRoll = Random.Range(0f, 1f);
                if (randomRoll <= multicastChance)
                {
                    multicastTimes++;
                }
            }
            else
            {
                multicastTimes += (int)multicastChance;
                float leftoverChance = multicastChance - (float)multicastTimes;
                if (leftoverChance > 0f)
                {
                    float randomRoll = Random.Range(0f, 1f);
                    if (randomRoll <= leftoverChance)
                    {
                        multicastTimes++;
                    }
                }
            }
        }
    }

    private IEnumerator ShootSingleShot(float multicastAlpha)
    {

        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StopMoving();
        }

        //if (isAutoAim)
        // {
        //  AutoAim.SetActive(true);
        //} else
        // {
        //  AutoAim.SetActive(false);
        // }

        for (int i = 0; i < shotsPerAttack; i++)
        {
            SpawnMuzzleFlash();
            SpawnBulletCasing();
            Player.GetComponent<AttackHandler>().triggerRecoil();

            Quaternion rotation = owner.GetTransform().rotation;
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();

            if (!shootOpppositeSide) //only shoots forward
            {
                // Forward bullet
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                Quaternion forwardRotation = rotation;

                if (shotsCount >= sprayThreshold) // calculate spray pattern
                {
                    float spread = spray * (shotsCount - sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    forwardRotation *= spreadDirection;
                }

                p.transform.rotation = forwardRotation;

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }
            }
            else //does shoot opposite side
            {
                // Forward bullet
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                Quaternion forwardRotation = rotation;

                if (shotsCount >= sprayThreshold) // calculate spray pattern
                {
                    float spread = spray * (shotsCount - sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    forwardRotation *= spreadDirection;
                }

                p.transform.rotation = forwardRotation;

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }

                // Backward bullet
                GameObject projectileGO2 = Instantiate(projectile, (position - direction / 2), Quaternion.identity);
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                Quaternion backwardRotation = Quaternion.LookRotation(-transform.right, Vector3.forward);
                if (shotsCount >= sprayThreshold)
                {
                    float spread = spray * (shotsCount - sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    backwardRotation *= spreadDirection;
                }
                backwardRotation *= forwardRotation;
                p2.transform.rotation = backwardRotation;

                // Calculate the position of the backward bullet based on the updated direction
                Vector3 backwardPosition = position - direction / 2;

                p2.transform.position = backwardPosition;
                p2.transform.up = Quaternion.AngleAxis(180f, Vector3.forward) * backwardRotation * spreadDirection2 * -direction;

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }
            }

                shotsCount += 1;
            yield return new WaitForSeconds(spread);
        }

        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StartMoving();
        }
    }

    private IEnumerator ShootShotgun(float multicastAlpha)
    {
        float spacer = 0;
        float angle = 0;
        int shotsLeft = shotsPerAttack;
        spacer = shotgunSpread / (shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();
        Quaternion rotation = owner.GetTransform().rotation;

        if (numMulticast >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(multicastWaitTime);
        }

        firstShot = false;


        if (shotsPerAttack % 2 != 0)
        {
            SpawnMuzzleFlash();
            SpawnBulletCasing();
            if (cantMove)
            {
                Player.GetComponent<PlayerMovement>().StopMoving();
            }

            if (!shootOpppositeSide) //only shoots forward
            {
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }

            }
            else //does shoot opposite side
            {
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.up = direction;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }

                GameObject projectileGO2 = Instantiate(projectile, (position - direction / 2), Quaternion.identity);
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.rotation = Quaternion.LookRotation(-direction);
                p2.transform.up = -direction; // set the projectile's up direction to the opposite of the direction
                p2.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }

            }

        }
        else
        {
            spacer = spacer / 2;
        }
        for (int i = 0; i < (shotsLeft % 2 == 0 ? shotsPerAttack : shotsPerAttack - 1); i++)
        {
            if (i == 0 && shotsPerAttack % 2 == 0)
            {
                angle = spacer / 2;
            }
            else if (i % 2 == 0)
            {
                angle = Mathf.Abs(angle) + spacer;
            }
            else
            {
                angle = -angle;
            }

            SpawnBulletCasing();
            SpawnMuzzleFlash();

            if (!shootOpppositeSide) //only shoots forward
            {
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }


            }
            else //does shoot opposite side
            {
                GameObject projectileGO = Instantiate(projectile, (position + direction / 2), Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.up = direction;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }

                GameObject projectileGO2 = Instantiate(projectile, (position - direction / 2), Quaternion.identity);
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.rotation = Quaternion.LookRotation(-direction);
                p2.transform.up = -direction; // set the projectile's up direction to the opposite of the direction
                p2.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                p2.transform.localScale = new Vector3(currentScale.x * projectileSize, currentScale.y * projectileSize, currentScale.z * projectileSize);

                if (multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(true);
                    foreach (SpriteRenderer sr in spriteRenderers)
                    {
                        Color spriteColor = sr.color;
                        if (spriteColor.a <= multicastAlpha)
                        {
                            spriteColor.a = 0.05f;
                        }
                        else
                        {
                            spriteColor.a -= multicastAlpha;
                        }
                        sr.color = spriteColor;
                    }
                }
            }
        }
        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StartMoving();
        }

    }

    private IEnumerator Melee(float multicastAlpha)
    {

        if (numMulticast >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(multicastWaitTime);
        }

        firstShot = false;
        float localSpacer = meleeSpacer;

        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StopMoving();
        }

        Vector3 originalScale = MeleeAttack.transform.localScale;
        Vector3 scaler = new Vector3(meleeShotsScaleUp, meleeShotsScaleUp, meleeShotsScaleUp);

        for (int i = 0; i < comboLength; i++)
        {
            Player.GetComponent<AttackHandler>().triggerWpnOff();
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();
            Quaternion rotation = owner.GetTransform().rotation;

            float perAttackScaling = 1 + (comboAttackBuff * i);

            //chain spawn 
            for (int c = 0; c < shotsPerAttackMelee + 1; c++)
            {
                Vector3 directionSpacer = Vector3.Scale(direction, new Vector3(localSpacer, localSpacer, localSpacer));

                if (!shootOpppositeSide) //only shoots forward
                {
                    GameObject projectileGO = Instantiate(MeleeAttack, (position + directionSpacer / 2), Quaternion.identity);
                    Projectile p = projectileGO.GetComponent<Projectile>();
                    p.attack = this;
                    p.attack.damage *= perAttackScaling; //scale damage per shotInAttack
                    p.transform.rotation = rotation;
                    if (c >= 1)
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);
                        p.transform.localScale += scaler * c;

                    }
                    else
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);

                    }
                    //change animation state 
                    if (swapAnimOnAttack)
                    {
                        Animator attackAnimator = p.GetComponent<Animator>();
                        switch (attackAnimState)
                        {
                            case 0:
                                attackAnimator.SetInteger("AttackCount", 0);
                                break;
                            case 1:
                                attackAnimator.SetInteger("AttackCount", 1);
                                break;
                            case 2:
                                attackAnimator.SetInteger("AttackCount", 2);
                                break;
                        }
                    }
                    if (multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                        foreach (SpriteRenderer sr in spriteRenderers)
                        {
                            Color spriteColor = sr.color;
                            if (spriteColor.a <= multicastAlpha)
                            {
                                spriteColor.a = 0.05f;
                            }
                            else
                            {
                                spriteColor.a -= multicastAlpha;
                            }
                            sr.color = spriteColor;
                        }
                    }

                }
                else //does shoot opposite side
                {
                    GameObject projectileGO = Instantiate(MeleeAttack, (position + directionSpacer / 2), Quaternion.identity);
                    Projectile p = projectileGO.GetComponent<Projectile>();
                    p.attack = this;
                    p.attack.damage *= perAttackScaling; //scale damage per shotInAttack

                    p.transform.rotation = rotation;
                    p.transform.up = direction;
                    if (c >= 1)
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);
                        p.transform.localScale += scaler * c;

                    }
                    else
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);

                    }
                    //change animation state 
                    if (swapAnimOnAttack)
                    {
                        Animator attackAnimator = p.GetComponent<Animator>();
                        switch (attackAnimState)
                        {
                            case 0:
                                attackAnimator.SetInteger("AttackCount", 0);
                                break;
                            case 1:
                                attackAnimator.SetInteger("AttackCount", 1);
                                break;
                            case 2:
                                attackAnimator.SetInteger("AttackCount", 2);
                                break;
                        }
                    }
                    if (multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(true);
                        foreach (SpriteRenderer sr in spriteRenderers)
                        {
                            Color spriteColor = sr.color;
                            if (spriteColor.a <= multicastAlpha)
                            {
                                spriteColor.a = 0.05f;
                            }
                            else
                            {
                                spriteColor.a -= multicastAlpha;
                            }
                            sr.color = spriteColor;
                        }
                    }

                    GameObject projectileGO2 = Instantiate(MeleeAttack, (position - directionSpacer / 2), Quaternion.identity);
                    Projectile p2 = projectileGO2.GetComponent<Projectile>();
                    p2.attack = this;
                    p.attack.damage *= perAttackScaling; //scale damage per shotInAttack

                    p2.transform.rotation = Quaternion.LookRotation(-directionSpacer);
                    p2.transform.up = -directionSpacer; // set the projectile's up direction to the opposite of the direction
                    if (c >= 1)
                    {
                        Vector3 currentScale = p2.transform.localScale;
                        p2.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);
                        p2.transform.localScale += scaler * c;

                    }
                    else
                    {
                        Vector3 currentScale = p2.transform.localScale;
                        p2.transform.localScale = new Vector3(currentScale.x * meleeSize, currentScale.y * meleeSize, currentScale.z * meleeSize);

                    }
                    //change animation state 
                    if (swapAnimOnAttack)
                    {
                        Animator attackAnimator2 = p2.GetComponent<Animator>();
                        switch (attackAnimState)
                        {
                            case 0:
                                attackAnimator2.SetInteger("AttackCount", 0);
                                break;
                            case 1:
                                attackAnimator2.SetInteger("AttackCount", 1);
                                break;
                            case 2:
                                attackAnimator2.SetInteger("AttackCount", 2);
                                break;
                        }
                    }
                    if (multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(true);
                        foreach (SpriteRenderer sr in spriteRenderers)
                        {
                            Color spriteColor = sr.color;
                            if (spriteColor.a <= multicastAlpha)
                            {
                                spriteColor.a = 0.05f;
                            }
                            else
                            {
                                spriteColor.a -= multicastAlpha;
                            }
                            sr.color = spriteColor;
                        }
                    }
                }

                Camera.GetComponent<ScreenShakeController>().StartShake(shakeTime, shakeStrength, shakeRotation);
                yield return new WaitForSeconds(spread);

                // after one hit in the combo, do this
                if (meleeShotsScaleUp > 0)
                {
                    localSpacer += meleeSpacerGap * (1 + meleeShotsScaleUp);
                }
                else
                {
                    localSpacer += meleeSpacerGap;
                }
            }

            //reset gap between hits
            localSpacer = meleeSpacer;

            //update attack state 
            attackAnimState++;
            if (attackAnimState == comboLength)
            {
                attackAnimState = 0;
            }

            //wait until next hit in combo
            yield return new WaitForSeconds(comboWaitTime);
        }

        if (cantMove)
        {
            Player.GetComponent<PlayerMovement>().StartMoving();
        }

    }


    // private IEnumerator Utility() //buff effect on self
    // {

    //  }
    private IEnumerator WaitThenShoot(int numMulticast)
    {
        if (numMulticast > 0)
        {
            yield return new WaitForSeconds(multicastWaitTime * numMulticast);
        }

        switch (attackType)
        {
            case AttackTypes.Projectile:
                { 
                    StartCoroutine(ShootSingleShot(multicastAlphaAmount));
                    multicastAlphaAmount += multicastAlphaFade;

                }
                break;

            case AttackTypes.Shotgun:
                {
                    StartCoroutine(ShootShotgun(multicastAlphaAmount));
                    multicastAlphaAmount += multicastAlphaFade;

                }
                break;
            case AttackTypes.Melee:
                { 
                    StartCoroutine(Melee(multicastAlphaAmount));
                    multicastAlphaAmount += multicastAlphaFade;

                }
                break;

            //case AttackTypes.Utility:
            //StartCoroutine(Utility());
            //break;
            default:
                break;
        }

    }

    public void Shoot()
    {
        firstShot = true;
        multicastAlphaAmount = 0f;
        rollMulticast();
        
        for (int i = 0; i < (multicastTimes + 1); i++)
        {
            shotsCount = 0; //reset Spray pattern
            StartCoroutine(WaitThenShoot(numMulticast));
            numMulticast++;
        }
        numMulticast = 0;
    }

    public void ThrowWeapon()
    {
        if (thrownWeapon == null)
        {
            return;
        }
        else
        {
            Quaternion rotation = owner.GetTransform().rotation;
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();


            GameObject wpnToss = Instantiate(thrownWeapon, position, Quaternion.identity);
            wpnToss.GetComponent<SpriteRenderer>().sprite = thrownSprite;
            Rigidbody2D rb = wpnToss.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * throwSpeed * -1, ForceMode2D.Impulse);
            rb.AddTorque(1000f);

            Projectile p = wpnToss.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = rotation;
        }

    }

    public void SpawnMuzzleFlash()
    {
        if (MuzzleFlashPrefab.Count == 0)
        {
            return;
        }

        // Select a random MuzzleFlashPrefab from the list
        GameObject selectedPrefab = MuzzleFlashPrefab[Random.Range(0, MuzzleFlashPrefab.Count)];


        // Instantiate the selected MuzzleFlashPrefab at the specified position
        Vector3 spawnPosition = transform.position + new Vector3(muzzleFlashXOffset, muzzleFlashYOffset, 0f);

        GameObject MuzzleFlash = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Quaternion rotation = owner.GetTransform().rotation;
        MuzzleFlash.transform.rotation = rotation;
    }

public void SpawnBulletCasing()
    {
        if (bulletCasing == null)
        {
            return;
        } else
        {
            // Calculate random position modifiers
            float xModifier = Random.Range(-0.1f, 0.1f);
            float yModifier = Random.Range(-0.5f, 0.1f);

            // Calculate position for the new object
            Vector3 spawnPosition = transform.position + new Vector3(xModifier, yModifier, 0f);

            // Instantiate the new object
            GameObject newBulletCasing = Instantiate(bulletCasing, spawnPosition, Quaternion.identity);

        }
    }



    public List<int> GenerateRarity(int count, int minValue, int maxValue)
    {
        List<int> possibleNumbers = new List<int>();

        for (int index = minValue; index <= maxValue; index++)
            possibleNumbers.Add(index);


        while (chosenNumbers.Count < count)
        {
            int position = Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);

            foreach (int value in chosenNumbers)
            {
            }
        }
        return chosenNumbers;


    }



    public Transform GetTransform()
    {
        return transform;
    }
    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.Weapon;
    }

}
