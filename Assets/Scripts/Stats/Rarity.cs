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
    //Uncommon = 1,
    Rare = 2,
    Epic = 4,
    Legendary = 6
    //Mythic = 5,
}
