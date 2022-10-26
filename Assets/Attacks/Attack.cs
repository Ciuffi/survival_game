using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage;
    public float spread;
    public float castTime;
    public Effect effect;
    public float speed;
    public GameObject projectile;




    public void Shoot(Direction dir)
    {
        GameObject projectileGO = Instantiate(projectile, Utilities.SetDirection(dir, gameObject.transform.position), Quaternion.identity);
        Projectile p = projectileGO.GetComponent<Projectile>();
        p.dir = dir;
        p.speed = speed;

    }


    // Start is called before the first frame update
    void Start()
    {
        if (projectile != null)
        {
            projectile = Resources.Load("projectiles/baseProjectile", typeof(GameObject)) as GameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
