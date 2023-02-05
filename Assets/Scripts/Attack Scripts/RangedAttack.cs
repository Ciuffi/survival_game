using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class RangedAttack : Attack
{
    private IEnumerator ShootSingleShot()
    {

        if (cantMove == true)
        {
            //if (isAutoAim)
            // {
            //  AutoAim.SetActive(true);
            //} else
            // {
            //  AutoAim.SetActive(false);
            // }

            for (int i = 0; i < shotsPerAttack; i++)
            {
                Quaternion rotation = owner.GetTransform().rotation;
                Vector3 position = owner.GetTransform().position;
                Vector3 direction = owner.GetDirection();

                Player.GetComponent<PlayerMovement>().StopMoving(); //stop moving before shooting

                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.projectileRange = range;



                yield return new WaitForSeconds(spread);
            }
            Player.GetComponent<PlayerMovement>().StartMoving();
        }
        else
        {

            float startTime = Time.time;
            float runTime = 0;
            for (int i = 0; i < shotsPerAttack; i++)
            {
                Quaternion rotation = owner.GetTransform().rotation;
                Vector3 position = owner.GetTransform().position;
                Vector3 direction = owner.GetDirection();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = rotation;
                p.projectileRange = range;
                yield return new WaitForSeconds(spread);
                runTime += Time.deltaTime;
                if (runTime > attackTime)
                {
                    yield break;
                }
            }
        }
    }

    private IEnumerator ShootShotgun()
    {
        float spacer = 0;
        float angle = 0;
        float shotsLeft = shotsPerAttack;
        spacer = spread / (shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();


        if (cantMove == true)
        {
            if (shotsPerAttack % 2 != 0)
            {
                Player.GetComponent<PlayerMovement>().StopMoving();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
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

                Player.GetComponent<PlayerMovement>().StopMoving();
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);

            }
            yield return null;

            Player.GetComponent<PlayerMovement>().StartMoving();

        }
        else
        {
            if (shotsPerAttack % 2 != 0)
            {
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
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
                GameObject projectileGO = Instantiate(projectile, position + direction / 2, Quaternion.identity);
                Projectile p = projectileGO.GetComponent<Projectile>();
                p.attack = this;
                p.transform.rotation = owner.GetTransform().rotation;
                p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
            }
            yield return null;
        }
    }
    public override void Shoot()
    {
        switch (attackType)
        {
            case AttackFunctions.SingleShot:
                StartCoroutine(ShootSingleShot());
                break;
            case AttackFunctions.Shotgun:
                StartCoroutine(ShootShotgun());
                break;
        }
    }

    public override void ThrowWeapon()
    {
        if (thrownWeaponSprite == null)
        {
            return;
        }
        else
        {
            Quaternion rotation = owner.GetTransform().rotation;
            Vector3 position = owner.GetTransform().position;
            Vector3 direction = owner.GetDirection();


            GameObject wpnToss = Instantiate(thrownWeaponGO, position, Quaternion.identity);
            wpnToss.GetComponent<SpriteRenderer>().sprite = thrownWeaponSprite;
            Rigidbody2D rb = wpnToss.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * throwSpeed * -1, ForceMode2D.Impulse);
            rb.AddTorque(1000f);

            Projectile p = wpnToss.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = rotation;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        if (projectile == null)
        {
            projectile = Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
    }
}
