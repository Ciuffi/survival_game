using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponStatsTalents
{
    public string StatName;
    public RarityWeights weights;
    public WeaponStatBoost common;
    public WeaponStatBoost rare;
    public WeaponStatBoost epic;
    public WeaponStatBoost legendary;
    public WeaponStatsTalents(string statName, RarityWeights weights, WeaponStatBoost common, WeaponStatBoost rare, WeaponStatBoost epic, WeaponStatBoost legendary)
    {
        StatName = statName;
        this.weights = weights;
        this.common = common;
        this.rare = rare;
        this.epic = epic;
        this.legendary = legendary;
    }

    public WeaponStatBoost RollStat()
    {
        RarityTypes rarity = weights.Roll();
        switch (rarity)
        {
            case RarityTypes.Common: return common;
            case RarityTypes.Rare: return rare;
            case RarityTypes.Epic: return epic;
            default: return legendary;
        }
    }
}