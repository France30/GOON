using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    //[SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<string> enemies;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private int minEnemyCount = 1, maxEnemyCount = 5;

    [SerializeField] private float spawnTime;

    private void Start()
    {
        //CheckSpawnPoints();
        InvokeRepeating("SpawnEnemies", 0, spawnTime);
    }
   
    /*public void CheckSpawnPoints()
    {
        //Check each spawnpoints. If there are no enemies in that spawn point, spawn more enemies
        foreach(Transform t in spawnPoints)
        {
            if(t.childCount == 0)
            {
                SpawnEnemies(t);
            }
        }
    }*/

    private void SpawnEnemies()
    {
        Transform parent = GetRandomSpawnPoint();
        //Use the object pool manager to get an enemy from the list
        GameObject pooledEnemy = ObjectPoolManager.Instance.GetPooledObject(GetRandomEnemyID());
        Debug.Log(pooledEnemy);
        //Check first if we received a valid gameobject from the pool
        if (pooledEnemy != null)
        {
            pooledEnemy.transform.parent = parent;
            pooledEnemy.transform.localPosition = Vector3.zero;
            //Activate it to use the object
            pooledEnemy.SetActive(true);
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

    /*
    private GameObject GetRandomEnemyPrefab()
    {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }*/
}
