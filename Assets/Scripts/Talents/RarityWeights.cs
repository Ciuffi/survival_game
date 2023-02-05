using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RarityWeights
{
    public float common;
    public float rare;
    public float epic;
    public float legendary;

    public RarityWeights(float common, float rare, float epic, float legendary)
    {
        this.common = common;
        this.rare = rare + this.common;
        this.epic = epic + this.rare;
        this.legendary = legendary + this.epic;
    }

    public RarityTypes Roll()
    {
        float roll = Random.Range(0, 100);
        if (roll <= common) return RarityTypes.Common;
        if (roll <= rare) return RarityTypes.Rare;
        if (roll <= epic) return RarityTypes.Epic;
        return RarityTypes.Legendary;
    }
}