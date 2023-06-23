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
    public float attackTime;
    public Attacker owner;
    GameObject Player;
    GameObject Camera;

    private int attackAnimState = 0;

    public List<Sprite> weaponSprite;
    public List<Sprite> thrownSprite;
    public List<Sprite> displaySprite;
    public WpnSpriteRotation weaponContainer;

    public GameObject thrownWeapon;
    private bool firstShot = true;

    public GameObject bulletCasing;
    public List<GameObject> MuzzleFlashPrefab;
    public float muzzleFlashXOffset;
    public float muzzleFlashYOffset;
    public float totalDamageDealt;

    public float shotsCount;
    public int numMulticast;
    public float multicastAlphaAmount;
    public float multicastAlphaFade = 0.20f;

    public bool isAutoAim;

    public GameObject AutoAim;
    private WpnSpriteRecoil recoil;

    void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        Camera = GameObject.FindWithTag("MainCamera");
        if (upgradeContainer == null)
        {
            upgradeContainer = Instantiate(new GameObject("attack_upgrades"), transform).transform;
        }
        recoil = FindObjectOfType<WpnSpriteRecoil>();
    }

    void Start()
    {
        totalDamageDealt = 0;

        if (projectile == null)
        {
            projectile =
                Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Attacker>();

        weaponContainer = FindObjectOfType<WpnSpriteRotation>();
        CalculateStats();

        //Debug.Log(
        //$"Upgrades: Damage: {baseStats.damage}, CastTime: {baseStats.castTime}, CritChance: {baseStats.critChance}, ShotsPerAttack: {baseStats.shotsPerAttack}, ShotgunSpread: {baseStats.shotgunSpread}, ProjectileSize: {baseStats.projectileSize}, Range: {baseStats.range}, Knockback: {baseStats.knockback}"
        //);
    }

    public void OnDamageDealt(float damage)
    {
        totalDamageDealt += damage;
    }

    private void Update() { }

    public void AddWeaponUpgrade(AttackStats upgrade)
    {
        var statsContainer = Instantiate(upgrade.statsContainer, upgradeContainer);
        var attackStatComponent = statsContainer.GetComponent<AttackStatComponent>();
        attackStatComponent.stat = upgrade;

        CalculateStats();
    }

    public void CalculateStats()
    {
        if (Player == null)
        {
            return;
        }

        AttackStats[] upgrades = upgradeContainer
            .GetComponentsInChildren<AttackStatComponent>()
            .Select(x => x.stat)
            .ToArray();

        if (upgrades.Length > 0)
        {
            stats = new AttackStats(baseStats).mergeInStats(upgrades);
        }
        else
        {
            //Debug.Log("no upgrades");
            stats = new AttackStats(baseStats);
        }

        //Debug.Log("merge stats");

        if (Player.GetComponent<StatsHandler>().stats != null)
        {
            //Merge in the player stats
            stats.MergeInPlayerStats(Player.GetComponent<StatsHandler>().stats);
        }

        stats.ApplyMultiplier();

        //update attackTime
        if (attackType == AttackTypes.Shotgun)
        {
            attackTime =
                Mathf.RoundToInt(stats.multicastChance) * stats.multicastWaitTime;
        }
        else if (attackType == AttackTypes.Melee)
        {
            attackTime =
                (stats.comboLength - 1) * stats.comboWaitTime
                + stats.shotsPerAttackMelee * stats.spread
                + Mathf.RoundToInt(stats.multicastChance) * stats.multicastWaitTime;
        }
        else
        {
            attackTime =
                stats.spread * stats.shotsPerAttack
                + Mathf.RoundToInt(stats.multicastChance) * stats.multicastWaitTime;
        }

        FixNegativeStats();
    }

    private void FixNegativeStats()
    {
        stats.damage = stats.damage < 0 ? 0 : stats.damage;
        stats.castTime = stats.castTime < 0 ? 0 : stats.castTime;
        stats.range = stats.range < 1 ? 1 : stats.range;
        stats.shotsPerAttack = stats.shotsPerAttack < 0 ? 0 : stats.shotsPerAttack;
        stats.shotsPerAttackMelee = stats.shotsPerAttackMelee < 0 ? 0 : stats.shotsPerAttackMelee;
        stats.comboLength = stats.comboLength < 0 ? 0 : stats.comboLength;
        stats.pierce = stats.pierce < 0 ? 0 : stats.pierce;
        stats.splitAmount = stats.splitAmount < 0 ? 0 : stats.splitAmount;
        stats.chainTimes = stats.chainTimes < 0 ? 0 : stats.chainTimes;
        stats.thrownSpeed = stats.thrownSpeed < 0 ? 0 : stats.thrownSpeed;
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
        //Debug.Log("attack " + stats.damage + "Multiplier " + stats.damageMultiplier);
        //Debug.Log("Proj Size: " + stats.projectileSize + "Multiplier: " + stats.projectileSizeMultiplier);
        //Debug.Log("Melee Size: " + stats.meleeSize + "Multiplier: " + stats.meleeSizeMultiplier);
        //Debug.Log("shotgun" + stats.shotgunSpread + " and multiplier: " + stats.shotgunSpreadMultiplier);
        //Debug.Log("range and spread" + stats.rangeMultiplier + " and: " + stats.spreadMultiplier);
        //Debug.Log("spread" + stats.spread + " and: " + stats.spreadMultiplier);
        //Debug.Log("speed and multiplier" + stats.speed + " and: " + stats.speedMultiplier);
        //Debug.Log("throw damage: " + stats.thrownDamage + "multiplier: " + stats.thrownDamageMultiplier);
        //Debug.Log(thrownWeapon.GetComponent<Projectile>().damage);

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

            Quaternion rotation = weaponContainer.GetTransform().rotation;
            Vector3 position = weaponContainer.GetTransform().position;
            Vector3 direction = weaponContainer.GetDirection();

            if (!stats.shootOppositeSide) //only shoots forward
            {

                // Forward bullet
                GameObject projectileGO = Instantiate(
                    projectile,
                    (position + direction / 4),
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
                    float spread = stats.spray * (shotsCount - stats.sprayThreshold);
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
                            spriteColor.a = 0.20f;
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
                    (position + direction / 4),
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
                    float spread = stats.spray * (shotsCount - stats.sprayThreshold);
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
                            spriteColor.a = 0.20f;
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
                    float spread = stats.spray * (shotsCount - stats.sprayThreshold);
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
                            spriteColor.a = 0.20f;
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
            if (i < stats.shotsPerAttack - 1)
            {
                yield return new WaitForSeconds(stats.spread);
            }
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
        Quaternion rotation = weaponContainer.GetTransform().rotation;
        Vector3 position = weaponContainer.GetTransform().position;
        Vector3 direction = weaponContainer.GetDirection();

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
                    (position + direction / 4),
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
                            spriteColor.a = 0.20f;
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
                    (position + direction / 4),
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
                            spriteColor.a = 0.20f;
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
                            spriteColor.a = 0.20f;
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
                    (position + direction / 4),
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
                            spriteColor.a = 0.20f;
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
                    (position + direction / 4),
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
                            spriteColor.a = 0.20f;
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
                            spriteColor.a = 0.20f;
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
        //Debug.Log("Casttime: " + stats.castTime);
        //Debug.Log("AttackTime: " + attackTime);

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
        float scaler = stats.meleeShotsScaleUp;

        for (int i = 0; i < stats.comboLength; i++)
        {
            Player.GetComponent<AttackHandler>().triggerWpnOff();
            Quaternion rotation = weaponContainer.GetTransform().rotation;
            Vector3 position = weaponContainer.GetTransform().position;
            Vector3 direction = weaponContainer.GetDirection();

            float scaledDamage = stats.damage * (1 + (stats.comboAttackBuffMultiplier * i));

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
                    p.damage = scaledDamage; //scale damage per shotInAttack
                    p.transform.rotation = rotation;
                    if (c >= 1)
                    {
                        Vector3 currentScale = p.transform.localScale;
                        p.transform.localScale = new Vector3(
                            currentScale.x * stats.meleeSize,
                            currentScale.y * stats.meleeSize,
                            currentScale.z * stats.meleeSize
                        );
                        p.transform.localScale *= (scaler * c);
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
                                spriteColor.a = 0.20f;
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
                    p.damage = scaledDamage; //scale damage per shotInAttack

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
                        p.transform.localScale *= (scaler * c);
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
                                spriteColor.a = 0.20f;
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
                    p2.damage = scaledDamage; //scale damage per shotInAttack

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
                        p2.transform.localScale *= (scaler * c);
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
                                spriteColor.a = 0.20f;
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

                if (c < stats.shotsPerAttackMelee)
                {
                    yield return new WaitForSeconds(stats.spread);
                }

                localSpacer += stats.meleeSpacerGap * stats.meleeSpacerGapMultiplier;

                //update attack state
                attackAnimState++;
                if (attackAnimState == stats.comboLength)
                {
                    attackAnimState = 0;
                }

            }

            //wait until next hit in combo
            if (i < stats.comboLength - 1)
            {
                yield return new WaitForSeconds(stats.comboWaitTime);
            }
            localSpacer = stats.meleeSpacer;

        }

        if (stats.cantMove)
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
            yield return new WaitForSeconds(stats.multicastWaitTime * numMulticast);
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
            wpnToss.transform.localScale *= stats.thrownWeaponSizeMultiplier;
            wpnToss.GetComponent<SpriteRenderer>().sprite = GetUpgradeIcon();
            wpnToss.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetUpgradeIcon();
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
            weaponContainer.GetTransform().position
            + new Vector3(muzzleFlashXOffset, muzzleFlashYOffset, 0f);

        GameObject MuzzleFlash = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

        Quaternion rotation = weaponContainer.GetTransform().rotation;
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
            Vector3 spawnPosition =
                weaponContainer.transform.position + new Vector3(xModifier, yModifier, 0f);

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
        return thrownSprite[(int)stats.rarity / 2];
    }

    public Sprite GetWeaponSprite()
    {
        return weaponSprite[(int)stats.rarity / 2];
    }

    public Sprite GetDisplaySprite()
    {
        return displaySprite[(int)stats.rarity / 2];
    }

    public string GetUpgradeDescription()
    {
        return stats.description;
    }

    public Rarity GetRarity()
    {
        return rarity;
    }

    //Clone all the attack member variables that are objects and not primitives
}
