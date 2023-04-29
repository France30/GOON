using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    [SerializeField] private GameObject spawnSpotlightFront;
    [SerializeField] private GameObject spawnSpotlightBack;
    [SerializeField] private GameObject spawnSpotlightLeft;
    [SerializeField] private GameObject spawnSpotlightRight;

    public void Warning(string spawn)
    {
        GameController.Instance.GameScreenUI.EnableDangerUI();

        switch(spawn)
        {
            case "SpawnFront":
                spawnSpotlightFront.SetActive(true);
                break;
            case "SpawnBack":
                spawnSpotlightBack.SetActive(true);
                break;
            case "SpawnLeft":
                spawnSpotlightLeft.SetActive(true);
                break;
            case "SpawnRight":
                spawnSpotlightRight.SetActive(true);
                break;
        }
    }
}
