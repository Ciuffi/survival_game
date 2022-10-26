using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Direction dir { get; set; }
    public float speed;
    void Start()
    {

    }
    void Update()
    {
        transform.position = Utilities.SetDirection(dir, transform.position, speed);
    }
}