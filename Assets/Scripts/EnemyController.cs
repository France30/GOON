using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject target;

    // Update is called once per frame
    private void Update()
    {
        Move();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
            OnDeath();
    }

    private void Move()
    {
        
    }

    private void OnDeath()
    {
        //temporary, replace with Singleton method
        Destroy(gameObject);
    }
}
