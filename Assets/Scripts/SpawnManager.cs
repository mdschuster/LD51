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

    [Header("Formations")] 
    public GameObject[] formations;


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
            float spawnRandom = Random.Range(0f, 1f);
            if (spawnRandom <= 0.5f)
            {
                spawnFormation();
            }
            else
            {
                spawnSingleEnemy();
            }

            spawnTimer = timeBetweenSpawns + Random.Range(-spawnTimeVariation, spawnTimeVariation);
        }

        spawnTimer -= Time.deltaTime;
    }

    private void spawnFormation()
    {
        float dis = 0f;
        Vector3 pos = Vector3.zero;
        //formation position
        while (dis <= 10f)
        {
            pos = new Vector3(Random.Range(-35f, 35f), Random.Range(-35f, 35f), 0f);
            dis = Vector3.Distance(pos, GameManager.Instance().getPlayer().transform.position);
        }
        
        GameObject form = Instantiate(formations[Random.Range(0, formations.Length)], pos, Quaternion.identity);
        //random rotation
        var euler = form.transform.eulerAngles;
        euler.z = Random.Range(0.0f, 360.0f);
        form.transform.eulerAngles = euler;

        Transform[] spawnPoints = form.GetComponentsInChildren<Transform>();
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        foreach (Transform point in spawnPoints)
        {
            if (point == form.transform) continue;
            GameObject enemy = baseEnemyPool.GetPooledObject();
            if(enemy==null) Debug.LogError("Enemy Could Not Spawn");

            enemy.transform.position = point.position;
            enemy.GetComponent<EnemyExplosion>().resetEnemy();
            enemy.GetComponent<Mover>().setSpeed(5f);
            enemy.GetComponent<Mover>().setDirection(direction);
            enemy.SetActive(true);
        }
        Destroy(form);
    }

    private void spawnSingleEnemy()
    {
        GameObject enemy = baseEnemyPool.GetPooledObject();
        if(enemy==null) Debug.LogError("Enemy Could Not Spawn");

        float dis = 0f;
        Vector3 pos = Vector3.zero;
        //formation position
        while (dis <= 20f)
        {
            pos = new Vector3(Random.Range(-35f, 35f), Random.Range(-35f, 35f), 0f);
            dis = Vector3.Distance(pos, GameManager.Instance().getPlayer().transform.position);
        }
        enemy.transform.position = pos;
        enemy.GetComponent<EnemyExplosion>().resetEnemy();
        enemy.GetComponent<Mover>().setSpeed(5f);
        enemy.GetComponent<Mover>().setDirection(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
        enemy.SetActive(true);
    }
}
