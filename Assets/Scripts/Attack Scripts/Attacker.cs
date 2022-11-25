using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Attacker
{
    public Vector3 GetDirection();
    public Transform GetTransform();
}