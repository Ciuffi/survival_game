using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    public Transform Target; //sets target for tracking movement
    public float speed = 3f;
    public float StopDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         //rotate to look at the player
         transform.LookAt(target.position);
         transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation
         
         
         //move towards the player
         if (Vector3.Distance(transform.position,target.position)> StopDistnace){//move if distance from target is greater than 1
             transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
         }
    }
}
