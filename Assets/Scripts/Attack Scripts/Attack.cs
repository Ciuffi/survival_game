using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

public class Attack : MonoBehaviour, Upgrade
{
    public GameObject projectile;
    public AttackStats baseStats;
    public List<AttackStats> weaponUpgrades;
    public WeaponSetType weaponSetType;
    public Transform upgradeContainer;
    public AttackStats stats;
    public Rarity rarity = 0; //0-common, 1-uncommon, 2-rare, 4-epic, 6-legendary
    public List<int> chosenNumbers = new List<int>();

    public Effect effect;
    public AttackTypes attackType;
    public Attacker owner;
    GameObject Player;
    GameObject Camera;

    private int attackAnimState = 0;

    public Sprite weaponSprite;
    public bool isAutoAim;
    public GameObject AutoAim;

    public GameObject thrownWeapon;
    public Sprite thrownSprite;
    private bool firstShot = true;

    public GameObject bulletCasing;
    public List<GameObject> MuzzleFlashPrefab;
    public float muzzleFlashXOffset;
    public float muzzleFlashYOffset;
    private VirtualJoystick VJ;
    public float totalDamageDealt;

    public float shotsCount;
    public int numMulticast;
    public float multicastAlphaAmount;

    // Start is called before the first frame update
    void Start()
    {
        totalDamageDealt = 0;

        if (projectile == null)
        {
            projectile =
                Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Attacker>();

        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        VJ = GameObject.Find("Joystick Container").GetComponent<VirtualJoystick>();
        upgradeContainer = Instantiate(new GameObject("attack_upgrades"), transform).transform;
        CalculateStats();

        //Debug.Log(
        //$"Upgrades: Damage: {baseStats.damage}, CastTime: {baseStats.castTime}, CritChance: {baseStats.critChance}, ShotsPerAttack: {baseStats.shotsPerAttack}, ShotgunSpread: {baseStats.shotgunSpread}, ProjectileSize: {baseStats.projectileSize}, Range: {baseStats.range}, Knockback: {baseStats.knockback}"
        //);
    }

    public void OnDamageDealt(float damage)
    {
        totalDamageDealt += damage;
    }

    private void Update()
    {
        if (stats != null)
        {
            if (attackType == AttackTypes.Shotgun)
            {
                stats.attackTime = stats.multicastTimes * stats.multicastWaitTime;
            }
            else if (attackType == AttackTypes.Melee)
            {
                stats.attackTime =
                    (stats.comboLength - 1) * stats.comboWaitTime
                    + stats.shotsPerAttackMelee * stats.spread
                    + stats.multicastTimes * stats.multicastWaitTime;
                // Add the definition for Melee attack type
            }
            else
            {
                stats.attackTime =
                    stats.spread * stats.shotsPerAttack
                    + stats.multicastTimes * stats.multicastWaitTime;
            }

        }
    }

    public void CalculateStats()
    {
        AttackStats[] upgrades = upgradeContainer.GetComponentsInChildren<AttackStats>();

        if (upgrades.Length > 0)
        {
            Debug.Log("yes upgrades");
            stats = new AttackStats(baseStats).mergeInStats(upgrades);
        }
        else
        {
            Debug.Log("no upgrades");
            stats = new AttackStats(baseStats);
            Debug.Log(stats.damage);
        }

        // Debug.Log(baseStats.damage);

        Debug.Log("merge stats");
        //Merge in the player stats
        stats.MergeInPlayerStats(Player.GetComponent<StatsHandler>().stats);
    }

    private void rollMulticast()
    {
        stats.multicastTimes = 0; //reset
        if (stats.multicastChance > 0)
        {
            if (stats.multicastChance < 1)
            {
                float randomRoll = Random.Range(0f, 1f);
                if (randomRoll <= stats.multicastChance)
                {
                    stats.multicastTimes++;
                }
            }
            else
            {
                stats.multicastTimes += (int)stats.multicastChance;
                float leftoverChance = stats.multicastChance - (float)stats.multicastTimes;
                if (leftoverChance > 0f)
                {
                    float randomRoll = Random.Range(0f, 1f);
                    if (randomRoll <= leftoverChance)
                    {
                        stats.multicastTimes++;
                    }
                }
            }
        }
    }

    private IEnumerator ShootSingleShot(float multicastAlpha)
    {
        if (stats.cantMove)
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

        for (int i = 0; i < stats.shotsPerAttack; i++)
        {
            SpawnMuzzleFlash();
            SpawnBulletCasing();
            Player.GetComponent<AttackHandler>().triggerRecoil();

            Quaternion rotation = owner.GetTransform().rotation;
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();

            if (!stats.shootOppositeSide) //only shoots forward
            {
                // Forward bullet
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                Quaternion forwardRotation = rotation;

                if (shotsCount >= stats.sprayThreshold) // calculate spray pattern
                {
                    float spread = stats.spread * (shotsCount - stats.sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    forwardRotation *= spreadDirection;
                }
                p.transform.rotation = forwardRotation;

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                Quaternion forwardRotation = rotation;

                if (shotsCount >= stats.sprayThreshold) // calculate spray pattern
                {
                    float spread = stats.spread * (shotsCount - stats.sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    forwardRotation *= spreadDirection;
                }

                p.transform.rotation = forwardRotation;

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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

                float scale = currentScale.x * stats.projectileSize;
                Vector3 backwardDirection = -transform.right;
                backwardDirection = backwardDirection.normalized;
                // Flip the rotation variable 180 degrees and put it in a variable called backwardRotation
                Quaternion backwardRotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 180);
                if (shotsCount >= stats.sprayThreshold)
                {
                    float spread = stats.spread * (shotsCount - stats.sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    backwardRotation *= spreadDirection;
                }

                // Backward bullet
                GameObject projectileGO2 = Instantiate(
                    projectile,
                    (position - direction / 2),
                    Quaternion.identity
                );
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.localScale = new Vector3(scale, scale, scale);
                p2.transform.rotation = backwardRotation;

                // Calculate the position of the backward bullet based on the updated direction
                Vector3 backwardPosition = position - direction / 2;

                p2.transform.position = backwardPosition;

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
            yield return new WaitForSeconds(stats.spread);
        }

        if (stats.cantMove)
        {
            Player.GetComponent<PlayerMovement>().StartMoving();
        }
    }

    private IEnumerator ShootShotgun(float multicastAlpha)
    {
        float spacer = 0;
        float angle = 0;
        int shotsLeft = stats.shotsPerAttack;
        spacer = stats.shotgunSpread / (stats.shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();
        Quaternion rotation = owner.GetTransform().rotation;

        if (numMulticast >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(stats.multicastWaitTime);
        }

        firstShot = false;

        if (stats.shotsPerAttack % 2 != 0)
        {
            SpawnMuzzleFlash();
            SpawnBulletCasing();
            if (stats.cantMove)
            {
                Player.GetComponent<PlayerMovement>().StopMoving();
            }

            if (!stats.shootOppositeSide) //only shoots forward
            {
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                Quaternion forwardRotation = rotation;

                if (shotsCount >= stats.sprayThreshold) // calculate spray pattern
                {
                    float spread = stats.spread * (shotsCount - stats.sprayThreshold + 1);
                    float randomSpread = Random.Range(-spread, spread);
                    Quaternion spreadDirection = Quaternion.Euler(0, 0, randomSpread);
                    forwardRotation *= spreadDirection;
                }

                p.transform.rotation = forwardRotation;

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
                GameObject projectileGO2 = Instantiate(
                    projectile,
                    (position - direction / 2),
                    Quaternion.identity
                );
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.rotation = Quaternion.LookRotation(-direction);
                p2.transform.up = -direction; // set the projectile's up direction to the opposite of the direction
                p2.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
        for (
            int i = 0;
            i < (shotsLeft % 2 == 0 ? stats.shotsPerAttack : stats.shotsPerAttack - 1);
            i++
        )
        {
            if (i == 0 && stats.shotsPerAttack % 2 == 0)
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

            if (!stats.shootOppositeSide) //only shoots forward
            {
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 2),
                    Quaternion.identity
                );
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.up = direction;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                Vector3 currentScale = p.transform.localScale;
                p.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
                GameObject projectileGO2 = Instantiate(
                    projectile,
                    (position - direction / 2),
                    Quaternion.identity
                );
                Projectile p2 = projectileGO2.GetComponent<Projectile>();
                p2.attack = this;
                p2.transform.rotation = Quaternion.LookRotation(-direction);
                p2.transform.up = -direction; // set the projectile's up direction to the opposite of the direction
                p2.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
                p2.transform.localScale = new Vector3(
                    currentScale.x * stats.projectileSize,
                    currentScale.y * stats.projectileSize,
                    currentScale.z * stats.projectileSize
                );

                if (stats.multicastTimes > 0)
                {
                    SpriteRenderer[] spriteRenderers = p2.GetComponentsInChildren<SpriteRenderer>(
                        true
                    );
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
        if (stats.cantMove)
        {
            Player.GetComponent<PlayerMovement>().StartMoving();
        }
    }

    private IEnumerator Melee(float multicastAlpha)
    {
        if (numMulticast >= 1 && !firstShot)
        {
            yield return new WaitForSeconds(stats.multicastWaitTime);
        }

        firstShot = false;
        float localSpacer = stats.meleeSpacer;

        if (stats.cantMove)
        {
            Player.GetComponent<PlayerMovement>().StopMoving();
        }

        Vector3 originalScale = projectile.transform.localScale;
        Vector3 scaler = new Vector3(
            stats.meleeShotsScaleUp,
            stats.meleeShotsScaleUp,
            stats.meleeShotsScaleUp
        );

        for (int i = 0; i < stats.comboLength; i++)
        {
            Player.GetComponent<AttackHandler>().triggerWpnOff();
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();
            Quaternion rotation = owner.GetTransform().rotation;

            float perAttackScaling = 1 + (stats.comboAttackBuff * i);

            //chain spawn
            for (int c = 0; c < stats.shotsPerAttackMelee + 1; c++)
            {
                Vector3 directionSpacer = Vector3.Scale(
                    direction,
                    new Vector3(localSpacer, localSpacer, localSpacer)
                );

                if (!stats.shootOppositeSide) //only shoots forward
                {
                    GameObject projectileGO = Instantiate(
                        projectile,
                        (position + directionSpacer / 2),
                        Quaternion.identity
                    );
                    Projectile p = projectileGO.GetComponent<Projectile>();
                    p.attack = this;
                    p.attack.stats.damage *= perAttackScaling; //scale damage per shotInAttack
                    p.transform.rotation = rotation;
                    if (c >= 1)
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                        p.transform.localScale += scaler * c;
                    }
                    else
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                    }
                    //change animation state
                    if (stats.swapAnimOnAttack)
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
                    if (stats.multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers =
                            p.GetComponentsInChildren<SpriteRenderer>(true);
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
                    GameObject projectileGO = Instantiate(
                        projectile,
                        (position + directionSpacer / 2),
                        Quaternion.identity
                    );
                    Projectile p = projectileGO.GetComponent<Projectile>();
                    p.attack = this;
                    p.attack.stats.damage *= perAttackScaling; //scale damage per shotInAttack

                    p.transform.rotation = rotation;
                    p.transform.up = direction;
                    if (c >= 1)
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                        p.transform.localScale += scaler * c;
                    }
                    else
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                    }
                    //change animation state
                    if (stats.swapAnimOnAttack)
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
                    if (stats.multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers =
                            p.GetComponentsInChildren<SpriteRenderer>(true);
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

                    GameObject projectileGO2 = Instantiate(
                        projectile,
                        (position - directionSpacer / 2),
                        Quaternion.identity
                    );
                    Projectile p2 = projectileGO2.GetComponent<Projectile>();
                    p2.attack = this;
                    p.attack.stats.damage *= perAttackScaling; //scale damage per shotInAttack

                    p2.transform.rotation = Quaternion.LookRotation(-directionSpacer);
                    p2.transform.up = -directionSpacer; // set the projectile's up direction to the opposite of the direction
                    if (c >= 1)
                    {
                        Vector3 currentScale = p2.transform.localScale;
                        p2.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                        p2.transform.localScale += scaler * c;
                    }
                    else
                    {
                        Vector3 currentScale = p2.transform.localScale;
                        p2.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                    }
                    //change animation state
                    if (stats.swapAnimOnAttack)
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
                    if (stats.multicastTimes > 0)
                    {
                        SpriteRenderer[] spriteRenderers =
                            p2.GetComponentsInChildren<SpriteRenderer>(true);
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

                Camera
                    .GetComponent<ScreenShakeController>()
                    .StartShake(stats.shakeTime, stats.shakeStrength, stats.shakeRotation);
                yield return new WaitForSeconds(stats.spread);

                if (stats.meleeShotsScaleUp > 0)
                {
                    localSpacer += stats.meleeSpacerGap * (1 + stats.meleeShotsScaleUp);
                }
                else
                {
                    localSpacer += stats.meleeSpacerGap;
                }

                //reset gap between hits
                localSpacer = stats.meleeSpacer;

                //update attack state
                attackAnimState++;
                if (attackAnimState == stats.comboLength)
                {
                    attackAnimState = 0;
                }

                //wait until next hit in combo
                yield return new WaitForSeconds(stats.comboWaitTime);
            }

            if (stats.cantMove)
            {
                Player.GetComponent<PlayerMovement>().StartMoving();
            }
        }
    }

    // private IEnumerator Utility() //buff effect on self
    // {

    //  }
    private IEnumerator WaitThenShoot(int numMulticast)
    {
        if (numMulticast > 0)
        {
            yield return new WaitForSeconds(stats.multicastWaitTime * numMulticast);
        }

        switch (attackType)
        {
            case AttackTypes.Projectile:

                {
                    StartCoroutine(ShootSingleShot(multicastAlphaAmount));
                    multicastAlphaAmount += stats.multicastAlphaFade;
                }
                break;

            case AttackTypes.Shotgun:

                {
                    StartCoroutine(ShootShotgun(multicastAlphaAmount));
                    multicastAlphaAmount += stats.multicastAlphaFade;
                }
                break;
            case AttackTypes.Melee:

                {
                    StartCoroutine(Melee(multicastAlphaAmount));
                    multicastAlphaAmount += stats.multicastAlphaFade;
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

        for (int i = 0; i < (stats.multicastTimes + 1); i++)
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
            wpnToss.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = thrownSprite;
            Rigidbody2D rb = wpnToss.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * stats.thrownSpeed * -1, ForceMode2D.Impulse);
            rb.AddTorque(1200f);

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
        Vector3 spawnPosition =
            transform.position + new Vector3(muzzleFlashXOffset, muzzleFlashYOffset, 0f);

        GameObject MuzzleFlash = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Quaternion rotation = owner.GetTransform().rotation;
        MuzzleFlash.transform.rotation = rotation;
    }

    public void SpawnBulletCasing()
    {
        if (bulletCasing == null)
        {
            return;
        }
        else
        {
            // Calculate random position modifiers
            float xModifier = Random.Range(-0.1f, 0.1f);
            float yModifier = Random.Range(-0.5f, 0.1f);

            // Calculate position for the new object
            Vector3 spawnPosition = transform.position + new Vector3(xModifier, yModifier, 0f);

            // Instantiate the new object
            GameObject newBulletCasing = Instantiate(
                bulletCasing,
                spawnPosition,
                Quaternion.identity
            );
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

            foreach (int value in chosenNumbers) { }
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

    public string GetUpgradeName()
    {
        return stats.name;
    }

    public Sprite GetUpgradeIcon()
    {
        return thrownSprite;
    }

    public string GetUpgradeDescription()
    {
        return stats.description;
    }

    public Rarity GetRarity()
    {
        return rarity;
    }

    public void AddWeaponUpgrade(AttackStats upgrade)
    {
        upgrade.transform.parent = upgradeContainer;
        CalculateStats();
    }
}
