using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAimGunAdjustment : MonoBehaviour
{
    public GameObject AutoAim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction;

        if (AutoAim.GetComponent<AutoAim>().closestEnemy != null)
        {
            direction = AutoAim.GetComponent<AutoAim>().autoAimDirection;
        } else
        {
            direction = gameObject.transform.forward;
        }

        gameObject.transform.rotation = Quaternion.LookRotation(direction);

    }
}
