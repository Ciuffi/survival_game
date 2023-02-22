using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiLock : MonoBehaviour
{
    Vector3 OGtransform;

    // Start is called before the first frame update
    void Start()
    {
        OGtransform = transform.localPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = OGtransform;
    }
}
