using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Attack attack;
    void Start()
    {

    }
    void Update()
    {
        Debug.Log(transform.up);
        transform.position += transform.up * attack.speed;

    }
}