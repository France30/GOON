using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpotlight : MonoBehaviour
{
    [SerializeField] private float alertTime = 5f;

    private float timer;

    private void OnEnable()
    {
        timer = alertTime;
    }

    private void Update()
    {
        if (GameController.Instance.IsGamePaused)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0) DisableSpotlight();
    }

    private void DisableSpotlight()
    {
        gameObject.SetActive(false);
    }
}
