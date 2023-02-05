using UnityEngine;
using System.Linq;
public abstract class Attack : MonoBehaviour
{
    public string attackName;
    // BaseStats
    public float baseDamage;
    public float baseShotsPerAttack;
    public float baseCastTime;
    public float baseRecoveryTime;
    public float baseKnockback;
    public float baseCritChance;
    public float baseCritDamage;
    public float baseScale;
    public float baseShakeTime;
    public float baseShakeStrength;
    public float baseShakeRotation;
    public float baseThrowSpeed;

    // baseProJectile
    public float basePierce;
    public float baseRange;
    public float baseSpeed;
    public float baseSpread;

    // baseMelee
    public float baseInitialSpacer;
    public float baseComboSpacer;
    public float baseComboWaitTime;
    public float baseMeleeAttackSize;


    // Calculated stats
    public float damage;
    public float shotsPerAttack;
    public float castTime;
    public float recoveryTime;
    public float knockback;
    public float critChance;
    public float critDamage;
    public float scale;
    public float shakeTime;
    public float shakeStrength;
    public float shakeRotation;
    public float throwSpeed;

    // Projectile
    public float pierce;
    public float range;
    public float spread;
    public float speed;
    public GameObject thrownWeaponGO;
    public Sprite thrownWeaponSprite;

    // Melee
    public float initialSpacer;
    public float comboSpacer;
    public float comboWaitTime;
    public float meleeAttackSize;

    public GameObject Player;
    public GameObject Camera;
    public AttackFunctions attackType;

    public Sprite heldWeaponSprite;
    public GameObject projectile;
    public GameObject meleeAttackGO;

    public Attacker owner;
    public float attackTime
    {
        get => attackType == AttackFunctions.Shotgun ? 0 : spread * shotsPerAttack;
    }

    public bool cantMove
    {
        get => !Player.GetComponent<PlayerMovement>().canMove;
    }

    // Split out melee and ranged variables
    public Attack Init(string attackName,
                                            float baseDamage,
                                            float baseShotsPerAttack,
                                            float baseCastTime,
                                            float baseRecoveryTime,
                                            float baseKnockback,
                                            float baseCritChance,
                                            float baseCritDamage,
                                            float baseScale,
                                            float baseShakeTime,
                                            float baseShakeStrength,
                                            float baseShakeRotation,
                                            float baseThrowSpeed,
                                            float basePierce,
                                            float baseRange,
                                            float baseSpeed,
                                            float baseSpread,
                                            float baseInitialSpacer,
                                            float baseComboSpacer,
                                            float baseComboWaitTime,
                                            float baseMeleeAttackSize,
                                            AttackFunctions attackType,
                                            Sprite thrownWeapon,
                                            Sprite heldWeapon)
    {
        this.attackName = attackName;
        this.baseDamage = baseDamage;
        this.baseShotsPerAttack = baseShotsPerAttack;
        this.baseCastTime = baseCastTime;
        this.baseRecoveryTime = baseRecoveryTime;
        this.baseKnockback = baseKnockback;
        this.baseCritChance = baseCritChance;
        this.baseCritDamage = baseCritDamage;
        this.baseScale = baseScale;
        this.baseShakeTime = baseShakeTime;
        this.baseShakeStrength = baseShakeStrength;
        this.baseShakeRotation = baseShakeRotation;
        this.baseThrowSpeed = baseThrowSpeed;
        this.basePierce = basePierce;
        this.baseRange = baseRange;
        this.baseSpeed = baseSpeed;
        this.baseSpread = baseSpread;
        this.baseInitialSpacer = baseInitialSpacer;
        this.baseComboSpacer = baseComboSpacer;
        this.baseComboWaitTime = baseComboWaitTime;
        this.baseMeleeAttackSize = baseMeleeAttackSize;
        this.thrownWeaponSprite = thrownWeapon;
        this.heldWeaponSprite = heldWeapon;
        this.attackType = attackType;
        Camera = GameObject.FindWithTag("MainCamera");
        Player = GameObject.FindWithTag("Player");
        return this;
    }

    public void CalculateStats()
    {
        ResetStats();
        GetComponents<WeaponStatBoost>().ToList().ForEach(stat =>
        {
            damage = stat.damage;
            shotsPerAttack = stat.shotsPerAttack;
            castTime = stat.castTime;
            knockback = stat.knockback;
            critChance = stat.critChance;
            critDamage = stat.critDamage;
            scale = stat.scale;
            pierce = stat.pierce;
            range = stat.range;
            speed = stat.speed;
            spread = stat.spread;
            initialSpacer = stat.initialSpacer;
            comboSpacer = stat.comboSpacer;
            comboWaitTime = stat.comboWaitTime;
        });
    }

    public void ResetStats()
    {
        damage = baseDamage;
        shotsPerAttack = baseShotsPerAttack;
        castTime = baseCastTime;
        recoveryTime = baseRecoveryTime;
        knockback = baseKnockback;
        critChance = baseCritChance;
        critDamage = baseCritDamage;
        scale = baseScale;
        shakeTime = baseShakeTime;
        shakeStrength = baseShakeStrength;
        shakeRotation = baseShakeRotation;
        throwSpeed = baseThrowSpeed;
        pierce = basePierce;
        range = baseRange;
        speed = baseSpeed;
        spread = baseSpread;
        initialSpacer = baseInitialSpacer;
        comboSpacer = baseComboSpacer;
        comboWaitTime = baseComboWaitTime;
    }

    public abstract void Shoot();
    public abstract void ThrowWeapon();
}