using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController :Singleton<GameController>
{
    private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown("p"))
        {
            TogglePause();
        }
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    private void TogglePause()
    {
        GameObject TempPauseScreen = GameObject.Find("TempCanvas").transform.GetChild(0).gameObject; //placeholder only
        if (!isPaused)
            isPaused = true;
        else
            isPaused = false;

        TempPauseScreen.SetActive(isPaused); //placeholder only

        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            EnemyController enemyController = enemy[i].GetComponent<EnemyController>();
            EnemyAnimationController enemyAnimation = enemy[i].GetComponent<EnemyAnimationController>();

            enemyController.TogglePause();
            enemyAnimation.TogglePause();
        }

        EnemySpawner.Instance.TogglePause();
    }
}
