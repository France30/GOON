using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{ 
    [SerializeField] private List<string> enemies;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private Wave[] wave;

    private IndicatorScript indicator;

    private int currentEnemyCount;
    private int currentWaveCount = 0;

    private float resumeTime;

    public void TogglePause()
    {
        if (GameController.Instance.IsGamePaused)
            CancelInvoke();
        else
            InvokeRepeating("SpawnEnemies", resumeTime, wave[currentWaveCount].spawnTime);
    }

    private void Start()
    {
        resumeTime = timeBetweenWaves;
        InvokeRepeating("SpawnEnemies", timeBetweenWaves, wave[0].spawnTime);
        indicator = GetComponent<IndicatorScript>();

        GameController.Instance.EnemySpawner = this;
    }

    private void Update()
    {
        bool gamePaused = GameController.Instance.IsGamePaused;
        if (gamePaused) return;

        if (resumeTime > 0)
            resumeTime -= Time.deltaTime;

        bool isEnemyAlive = GameObject.FindGameObjectWithTag("Enemy") != null;
        if (!IsInvoking("SpawnEnemies") && !isEnemyAlive)
            NextWave();
    }

    private void SpawnEnemies()
    {
        bool waveGoalReached = currentEnemyCount >= wave[currentWaveCount].totalEnemies;
        if (waveGoalReached)
        {
            CancelInvoke();
            return;
        }

        resumeTime = wave[currentWaveCount].spawnTime;
        
        Transform parent = GetRandomSpawnPoint();
        int enemyCount;
        int enemiesLeft = wave[currentWaveCount].totalEnemies - currentEnemyCount;
        if (wave[currentWaveCount].maxEnemyCount > enemiesLeft)
            enemyCount = Random.Range(1, enemiesLeft);
        else
            enemyCount = Random.Range(wave[currentWaveCount].minEnemyCount, wave[currentWaveCount].maxEnemyCount);
        currentEnemyCount += enemyCount;

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

                SetEnemyDifficulty(pooledEnemy);
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

    private void SetEnemyDifficulty(GameObject enemy)
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        float waveEnemySpeed = enemyController.Speed * wave[currentWaveCount].speedMultiplier;
        enemyController.Speed = waveEnemySpeed;

        int waveEnemyPoint = enemyController.EnemyScore * wave[currentWaveCount].scoreMultiplier;
        enemyController.EnemyScore = waveEnemyPoint;
    }

    private void NextWave()
    {
        currentEnemyCount = 0;

        if (currentWaveCount < wave.Length - 1)
        {
            currentWaveCount++;
            InvokeRepeating("SpawnEnemies", timeBetweenWaves, wave[currentWaveCount].spawnTime);
            resumeTime = timeBetweenWaves;

            GameController.Instance.GameScreenUI.UpdateWaveCount((currentWaveCount + 1).ToString());
        }
        else
        {
            CancelInvoke();
            GameController.Instance.GameOver();
        }
    }
}
