using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public 

public class BasicSpawner : MonoBehaviour
{

    public float WaveCD = 1.5f;
    public float SpawnXmin = -5;
    public float SpawnXmax = -5;
    public float SpawnYmin = -5;
    public float SpawnYmax = -5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy(){

        while (true)
        {
            int xRandom = Random.Range(-5, 5)
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
