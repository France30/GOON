using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    private int currentScore = 0;
    public int HighScore { get; private set; }

    public bool IsGamePaused { get; private set; }
    public bool IsGameOver { get; private set; }
    public bool IsGameScene { get; private set; }

    public EnemySpawner EnemySpawner { private get; set; }
    public GameScreenUI GameScreenUI { get; private set; }  
    public PlayerController Player { get; private set; }

    public override void Awake()
    {
        base.Awake();
        IsGamePaused = false;
        IsGameOver = true;
        IsGameScene = false;
    }

    public void LoadGame()
    {
        if(!IsGameOver) DisableGame();
        StartCoroutine(InitializeGame());
    }

    public void QuitToMainMenu()
    {
        if (!IsGameOver) DisableGame();

        IsGameOver = true;
        IsGameScene = false;

        BuildManager.Instance.GetMainMenuBuild();
        AudioManager.Instance.Stop("GameSceneBGM");
    }

    public void AddScore(int point)
    {
        currentScore += point;
        GameScreenUI.UpdateScoreUI(currentScore.ToString());
    }

    public void GameOver()
    {
        IsGameOver = true;

        string gameOverMessage;
        if (Player.Health > 0)
        {
            gameOverMessage = "YOU WIN";
            AudioManager.Instance.Play("PlayerWin");
        }
        else
        {
            gameOverMessage = "YOU LOSE";
            AudioManager.Instance.Play("PlayerLose");
        }

        HighScore = currentScore;
        GameScreenUI.EnableGameOverUI(gameOverMessage);
        BuildManager.Instance.ToggleBuildPause();
        DisableGame();
    }

    private void Update()
    {
        if (IsGameOver) return;

        if (InputManager.Instance.PauseButton && IsGameScene)
            TogglePause();
    }

    private IEnumerator InitializeGame()
    {
        IsGamePaused = false;
        IsGameOver = false;
        HighScore = 0;
        currentScore = 0;        

        yield return SceneManager.LoadSceneAsync("Game Scene");

        BuildManager.Instance.GetGameBuild();

        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        GameScreenUI = GameObject.Find("GameScreenUI").GetComponent<GameScreenUI>();
        
        IsGameScene = true;
        AudioManager.Instance.Play("GameSceneBGM");
    }

    private void TogglePause()
    {
        IsGamePaused = !IsGamePaused;

        BuildManager.Instance.ToggleBuildPause();

        GameScreenUI.TogglePauseUI();
        EnemySpawner.TogglePause();
        PauseEnemies();       
    }

    private void PauseEnemies()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            EnemyController enemyController = enemy[i].GetComponent<EnemyController>();
            EnemyAnimationController enemyAnimation = enemy[i].GetComponent<EnemyAnimationController>();
            EnemyAudioController enemyAudio = enemy[i].GetComponent<EnemyAudioController>();

            enemyController.TogglePause();
            enemyAnimation.TogglePause();
            enemyAudio.TogglePause();
        }
    }

    private void DisableGame()
    {
        if (EnemySpawner != null)
        {
            EnemySpawner.CancelInvoke();
            EnemySpawner.enabled = false;
        }

        if (BuildManager.Instance.IsPCBuild)
            GameObject.Find("SciFiHandGun").SetActive(false);

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            ObjectPoolManager.Instance.DespawnGameObject(enemy[i]);
        }
    }
}
