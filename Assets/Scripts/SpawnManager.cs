using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WFG.Utilities;

public class SpawnManager : MonoBehaviour
{
    public float timeBetweenSpawns;
    public float spawnTimeVariation;
    private float spawnTimer;

    public ObjectPooler baseEnemyPool;


    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = timeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer <= 0f)
        {
            spawnEnemy();
            spawnTimer = timeBetweenSpawns + Random.Range(-spawnTimeVariation, spawnTimeVariation);
        }

        spawnTimer -= Time.deltaTime;
    }

    private void spawnEnemy()
    {
        GameObject enemy = baseEnemyPool.GetPooledObject();
        if(enemy==null) Debug.LogError("Enemy Could Not Spawn");

        Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0f);
        enemy.transform.position = pos;
        enemy.SetActive(true);
    }
}
