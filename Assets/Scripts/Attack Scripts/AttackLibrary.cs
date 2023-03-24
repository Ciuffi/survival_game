using System.Collections.Generic;
using UnityEngine;

public static class AttackLibrary
{
    private static Dictionary<string, AttackBuilder> attackBuilderDictionary =
        new Dictionary<string, AttackBuilder>();

    public static void InitializeLibrary()
    {
        // Example attack 1
        AttackBuilder exampleAttack1Builder = new AttackBuilder()
            .SetAttackName("ExampleAttack1")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/ExampleProjectile1"))
            .SetBaseStats(new AttackStats(damage: 10, speed: 5))
            .SetRarity(0)
            .SetProperties(
                weaponSprite: Resources.Load<Sprite>("Sprites/ExampleWeaponSprite1"),
                thrownWeapon: Resources.Load<GameObject>("ThrownWeapons/ExampleThrownWeapon1"),
                thrownSprite: Resources.Load<Sprite>("Sprites/ExampleThrownSprite1")
            );

        attackBuilderDictionary.Add("ExampleAttack1", exampleAttack1Builder);

        // Example attack 2
        AttackBuilder exampleAttack2Builder = new AttackBuilder()
            .SetAttackName("ExampleAttack2")
            .SetProjectile(Resources.Load<GameObject>("Projectiles/ExampleProjectile2"))
            .SetBaseStats(new AttackStats(damage: 15, speed: 4))
            .SetRarity(1)
            .SetProperties(
                weaponSprite: Resources.Load<Sprite>("Sprites/ExampleWeaponSprite2"),
                thrownWeapon: Resources.Load<GameObject>("ThrownWeapons/ExampleThrownWeapon2"),
                thrownSprite: Resources.Load<Sprite>("Sprites/ExampleThrownSprite2")
            );

        attackBuilderDictionary.Add("ExampleAttack2", exampleAttack2Builder);
    }

    public static AttackBuilder GetAttackBuilder(string attackName)
    {
        if (attackBuilderDictionary.TryGetValue(attackName, out AttackBuilder attackBuilder))
        {
            return attackBuilder;
        }
        else
        {
            Debug.LogError($"AttackBuilder '{attackName}' not found in AttackLibrary.");
            return null;
        }
    }
}
