using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicSpawner : MonoBehaviour
{

    public float waveCD = 10f;

    //public float spawnGap = 0.1f;

    public int waveSize = 4;

    public GameObject enemy1;
    public GameObject enemy2;
    public StatsHandler player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<StatsHandler>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {

        while (true)
        {

            for (var i = 0; i < waveSize; i++)
            {
                float randomChance = Random.Range(0.0f, 1.0f);
                if (randomChance <= 0.5f)
                {
                    Instantiate(enemy1, new Vector3(transform.position.x + i, transform.position.y, -1), Quaternion.identity);
                }
                else
                {
                    Instantiate(enemy2, new Vector3(transform.position.x + i, transform.position.y, -1), Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(waveCD - player.level / 2);
        }

    }


    // Update is called once per frame
    void Update()
    {

    }
}
