using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject zombie;
    public Transform[] spawnPoints;
    public float spawnTime = 3f;
    public int numberOfEnemies;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    void Spawn()
    {
        numberOfEnemies--;
        if (numberOfEnemies >= 0)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Instantiate(zombie, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
    }
}
