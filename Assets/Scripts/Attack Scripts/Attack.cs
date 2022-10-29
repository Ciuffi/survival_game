using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour, Upgrade
{
    public float damage;
    public float spread;
    public float castTime;
    public float startTime;
    public float range = 5;
    public int shotsPerAttack;
    public Effect effect;
    public float speed;
    public float knockback;
    public GameObject projectile;
    public AttackTypes attackType;
    public Attacker owner;


    private IEnumerator ShootSingleShot()
    {
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
        }
    }
    private IEnumerator ShootShotgun()
    {
        float spacer = 0;
        float angle = 0;
        int shotsLeft = shotsPerAttack;
        spacer = spread / (shotsPerAttack - 1);
        Vector3 position = owner.GetTransform().position;
        Vector3 direction = owner.GetDirection();
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
    public void Shoot()
    {
        switch (attackType)
        {
            case AttackTypes.SingleShot:
                StartCoroutine(ShootSingleShot());
                break;
            case AttackTypes.Shotgun:
                StartCoroutine(ShootShotgun());
                break;
            case AttackTypes.Laser:
                StartCoroutine(ShootSingleShot());
                break;
            default:
                break;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        if (projectile == null)
        {
            projectile = Resources.Load("Prefabs/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Attacker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Transform GetTransform()
    {
        return transform;
    }
    public UpgradeType GetUpgradeType()
    {
        return UpgradeType.Weapon;
    }

}
