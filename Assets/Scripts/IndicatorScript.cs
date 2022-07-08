using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    private GameObject target;

    // Start is called before the first frame update
    private void Start()
    {
        target = GameObject.Find("Main Camera");
    }

    public void Warning(string spawn)
    {
        switch(spawn)
        {
            case "SpawnFront":
                EnemySpawnNorth();
                break;
            case "SpawnBack":
                EnemySpawnSouth();
                break;
            case "SpawnLeft":
                EnemySpawnLeft();
                break;
            case "SpawnRight":
                EnemySpawnRight();
                break;
        }
    }

    private void EnemySpawnNorth()
    {
        if (IsPlayerLookingRight())
        {
            Debug.Log("Enemy To The Left!");
        }
        else if (IsPlayerLookingLeft())
        {
            Debug.Log("Enemy To The Right!");
        }
        else if (IsPlayerLookingNorth())
        {
            Debug.Log("Enemy In Front!");
        }
        else
            Debug.Log("Enemy Behind!");
    }

    private void EnemySpawnRight()
    {
        if (IsPlayerLookingRight())
        {
            Debug.Log("Enemy In Front!");
        }
        else if (IsPlayerLookingLeft())
        {
            Debug.Log("Enemy Behind!");
        }
        else if (IsPlayerLookingNorth())
        {
            Debug.Log("Enemy To The Right!");
        }
        else
            Debug.Log("Enemy To the Left!");
    }

    private void EnemySpawnLeft()
    {
        if (IsPlayerLookingRight())
        {
            Debug.Log("Enemy Behind!");
        }
        else if (IsPlayerLookingLeft())
        {
            Debug.Log("Enemy In Front!");
        }
        else if (IsPlayerLookingNorth())
        {
            Debug.Log("Enemy To The Left!");
        }
        else
            Debug.Log("Enemy To the Right!");
    }

    private void EnemySpawnSouth()
    {
        if (IsPlayerLookingRight())
        {
            Debug.Log("Enemy To The Right!");
        }
        else if (IsPlayerLookingLeft())
        {
            Debug.Log("Enemy To The Left!");
        }
        else if (IsPlayerLookingNorth())
        {
            Debug.Log("Enemy Behind!");
        }
        else
            Debug.Log("Enemy In Front!");
    }

    private bool IsPlayerLookingRight()
    {
        return GetTargetRotation() > 45 && GetTargetRotation() < 135;
    }

    private bool IsPlayerLookingLeft()
    {
        return GetTargetRotation() < -45 && GetTargetRotation() > -135;
    }

    private bool IsPlayerLookingNorth()
    {
        return GetTargetRotation() < 45 && GetTargetRotation() > -45;
    }

    private float GetTargetRotation()
    {
        float Rotation;
        if (target.transform.eulerAngles.y <= 180f)
            Rotation = target.transform.eulerAngles.y;
        else
            Rotation = target.transform.eulerAngles.y - 360f;

        return Rotation;
    }
}
