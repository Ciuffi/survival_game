using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> attacks;


    // Start is called before the first frame update
    void Start()
    {
        attacks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
