using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    //public int xPos;
    //public int zPos;
    public int enemyCount;

    [Header("SpawnTime")]
    public float spawnRate = 4f;
    public float spawnRateDropSpeed = 0.001f;
    public float spawnRateDeceleration = 0f;
    public float minSpawnRate = 1f;

    private float lastSpawnTime;
    private float accumulatedTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;

        spawnRateDropSpeed += spawnRateDeceleration * Time.deltaTime;
        spawnRate = Mathf.Max(spawnRate - Time.deltaTime * spawnRateDropSpeed, minSpawnRate);

        if (accumulatedTime - lastSpawnTime > spawnRate)
        {
            lastSpawnTime = accumulatedTime;
            EnemyDrop();
        }
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount < 10000)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnpoint = Random.Range(0, spawnPoints.Length);

            //xPos = Random.Range(-12, -4);
            //zPos = Random.Range(-5, 11);
            //new Vector3(xPos, 1f, zPos)

            Instantiate(enemyPrefabs[randomEnemy], spawnPoints[randomSpawnpoint].position , Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
            enemyCount += 1;
        }
    }

    void OnEnable()
    {
        accumulatedTime = 0f;
        lastSpawnTime = 0f;
    }

}
