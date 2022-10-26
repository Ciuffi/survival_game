using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float spread;
    public float castTime;
    public int shotsPerAttack;
    public Effect effect;
    public float speed;
    public GameObject projectile;
    public AttackTypes attackType;
    private Player owner;


    private IEnumerator ShootSingleShot(Vector3 start, Vector3 direction)
    {
        Quaternion rotation = owner.transform.rotation;
        for (int i = 0; i < shotsPerAttack; i++)
        {
            GameObject projectileGO = Instantiate(projectile, start + direction / 2, Quaternion.identity);
            Projectile p = projectileGO.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = rotation;
            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator ShootShotgun(Vector3 start, Vector3 direction)
    {
        float spacer = 0;
        float angle = 0;
        int shotsLeft = shotsPerAttack;
        spacer = spread / (shotsPerAttack - 1);
        if (shotsPerAttack % 2 != 0)
        {
            GameObject projectileGO = Instantiate(projectile, start + direction / 2, Quaternion.identity);
            Projectile p = projectileGO.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = owner.transform.rotation;
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
            GameObject projectileGO = Instantiate(projectile, start + direction / 2, Quaternion.identity);
            Projectile p = projectileGO.GetComponent<Projectile>();
            p.attack = this;
            p.transform.rotation = owner.transform.rotation;
            p.transform.Rotate(new Vector3(0, 0, angle), Space.Self);
            Debug.Log(angle);

        }
        yield return null;
    }
    public void Shoot()
    {
        switch (attackType)
        {
            case AttackTypes.SingleShot:
                StartCoroutine(ShootSingleShot(owner.transform.position, owner.direction));
                break;
            case AttackTypes.Shotgun:
                StartCoroutine(ShootShotgun(owner.transform.position, owner.direction));
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
            projectile = Resources.Load("Projectiles/BasicProjectile", typeof(GameObject)) as GameObject;
        }
        owner = transform.GetComponentInParent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
