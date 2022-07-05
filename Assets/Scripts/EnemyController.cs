using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script should assign different values to different enemies(ex. grunt -> tank, with tank taking 3 - 4 shots to kill)
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject target;

    // Update is called once per frame
    private void Update()
    {
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            OnDeath();
    }

    private void OnDeath()
    {
        //temporary, replace with Singleton method
        Destroy(gameObject);
    }
}
