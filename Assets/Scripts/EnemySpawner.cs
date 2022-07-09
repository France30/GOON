using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<string> enemies;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private int minEnemyCount = 1, maxEnemyCount = 5;

    [SerializeField] private float startWaveTime;
    [SerializeField] private float spawnTime;

    private IndicatorScript indicator;

    private void Start()
    {
        InvokeRepeating("SpawnEnemies", startWaveTime, spawnTime);
        indicator = GetComponent<IndicatorScript>();
    }
    {

    private void SpawnEnemies()
    {
        Transform parent = GetRandomSpawnPoint();
        int enemyCount = Random.Range(minEnemyCount, maxEnemyCount);
        //Debug.Log("spawning " + enemyCount + " in the " + parent.name);
        indicator.Warning(parent.name);

        for (int i = 0; i < enemyCount; i++)
        {
            //Use the object pool manager to get an enemy from the list
            GameObject pooledEnemy = ObjectPoolManager.Instance.GetPooledObject(GetRandomEnemyID());
            //Debug.Log(pooledEnemy);
            //Check first if we received a valid gameobject from the pool
            if (pooledEnemy != null)
            {
                pooledEnemy.transform.parent = parent;
                pooledEnemy.transform.localPosition = Vector3.zero;
                //Activate it to use the object
                pooledEnemy.SetActive(true);
            }

        }
    }

    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)];
    }

    private string GetRandomEnemyID()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }
}
