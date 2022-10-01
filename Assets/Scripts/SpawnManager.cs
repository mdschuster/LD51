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

        Vector3 pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);
        enemy.transform.position = pos;
        enemy.GetComponent<EnemyExplosion>().resetEnemy();
        enemy.GetComponent<Mover>().setSpeed(5f);
        enemy.GetComponent<Mover>().setDirection(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        enemy.SetActive(true);
    }
}
