public static class RarityExtensions
{
    public static Rarity CompareRarity(this Rarity first, Rarity second)
    {
        return (first > second) ? first : second;
    }
}

public enum Rarity
{
    Common = 0,
    Uncommon = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
    Mythic = 5,
}
