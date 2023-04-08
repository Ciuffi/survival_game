using UnityEngine;
using UnityEngine.UI;

public interface Upgrade
{
    public UpgradeType GetUpgradeType();

    public Sprite GetUpgradeIcon();

    public string GetUpgradeName();

    public string GetUpgradeDescription();

    public Transform GetTransform();

    public Rarity GetRarity();
}
