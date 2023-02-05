using UnityEngine;
public class WeaponStatBoost : MonoBehaviour
{
    public float damage;
    public float shotsPerAttack;
    public float castTime;
    public float knockback;
    public float critChance;
    public float critDamage;
    public float scale;

    // projectiles
    public float pierce;
    public float range;
    public float spread;
    public float speed;

    // melee
    public float initialSpacer;
    public float comboSpacer;
    public float comboWaitTime;

    public WeaponStatBoost(float damage = 0,
                                            float shotsPerAttack = 0,
                                            float castTime = 0,
                                            float knockback = 0,
                                            float critChance = 0,
                                            float critDamage = 0,
                                            float scale = 0,
                                            float pierce = 0,
                                            float range = 0,
                                            float spread = 0,
                                            float speed = 0,
                                            float initialSpacer = 0,
                                            float comboSpacer = 0,
                                            float comboWaitTime = 0)
    {
        this.damage = damage;
        this.shotsPerAttack = shotsPerAttack;
        this.castTime = castTime;
        this.knockback = knockback;
        this.critChance = critChance;
        this.critDamage = critDamage;
        this.scale = scale;
        this.pierce = pierce;
        this.range = range;
        this.spread = spread;
        this.speed = speed;
        this.initialSpacer = initialSpacer;
        this.comboSpacer = comboSpacer;
        this.comboWaitTime = comboWaitTime;
    }

    public WeaponStatBoost merge(WeaponStatBoost mergeObject)
    {
        this.damage += mergeObject.damage;
        this.shotsPerAttack += mergeObject.shotsPerAttack;
        this.castTime += mergeObject.castTime;
        this.knockback += mergeObject.knockback;
        this.critChance += mergeObject.critChance;
        this.critDamage += mergeObject.critDamage;
        this.scale += mergeObject.scale;
        this.pierce += mergeObject.pierce;
        this.range += mergeObject.range;
        this.spread += mergeObject.spread;
        this.speed += mergeObject.speed;
        this.initialSpacer += mergeObject.initialSpacer;
        this.comboSpacer += mergeObject.comboSpacer;
        this.comboWaitTime += mergeObject.comboWaitTime;
        return this;
    }
}