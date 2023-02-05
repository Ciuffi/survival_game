using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MeleeAttack : Attack
{
    private IEnumerator Melee()
    {
        float spacer = 0;
        float angle = 0;
        float shotsLeft = shotsPerAttack;
        spacer = spread / (shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();
        Quaternion rotation = owner.GetTransform().rotation;

        float localSpacer = initialSpacer;
        Player.GetComponent<PlayerMovement>().StopMoving();

        Vector3 originalScale = MeleeAttack.transform.localScale;
        Vector3 scaler = new Vector3(meleeScale, meleeScale, meleeScale);

        for (int c = 0; c < comboLength; c++)
        {
            if (shotsPerAttack % 2 != 0)
            {
                Vector3 directionSpacer = Vector3.Scale(direction, new Vector3(localSpacer, localSpacer, localSpacer));
                GameObject projectileGO = Instantiate(MeleeAttack, position + directionSpacer, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                if (c >= 1)
                {
                    p.transform.localScale += scaler * c;
                }
                Camera.GetComponent<ScreenShakeController>().StartShake(shakeTime, shakeStrength, shakeRotation);
            }
            else
            {
                spacer = spacer / 2;
            }
            for (int i = 0; i < (shotsLeft % 2 == 0 ? shotsPerAttack : shotsPerAttack - 1); i++)
            {
                if (i == 0 && shotsPerAttack % 2 == 0)
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
                Vector3 directionSpacer = Vector3.Scale(direction, new Vector3(localSpacer, localSpacer, localSpacer));
                GameObject projectileGO = Instantiate(MeleeAttack, position + directionSpacer, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);

                Camera.GetComponent<ScreenShakeController>().StartShake(shakeTime, shakeStrength, shakeRotation);

            }
            yield return new WaitForSeconds(comboWaitTime);

            // after one hit in the combo, do this

            if (meleeScale > 0)
            {
                localSpacer += meleeSpacerGap * (1 + meleeScale);
            }
            else
            {
                localSpacer += meleeSpacerGap;
            }
        }
        yield return null;
        //can move again
        Player.GetComponent<PlayerMovement>().StartMoving();
        //reset gap between hits
        localSpacer = meleeSpacer;
    }
    public override void Shoot()
    {
        switch (attackType)
        {
            case AttackFunctions.melee:
                StartCoroutine(Melee());
                break;
        }
    }

    public override void ThrowWeapon()
    {
        // Do nothing for melee
    }

}