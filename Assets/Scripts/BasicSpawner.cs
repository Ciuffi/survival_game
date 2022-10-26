using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicSpawner : MonoBehaviour
{

    public float waveCD = 10f;

    //public float spawnGap = 0.1f;

    public int waveSize = 4;

    public GameObject enemy1;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy(){

        while (true)
        {
           
            for (var i = 0; i < waveSize; i++)
            {
                Instantiate(enemy1, new Vector3(transform.position.x + i, transform.position.y, -1), Quaternion.identity);
            }

            yield return new WaitForSeconds(waveCD);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
