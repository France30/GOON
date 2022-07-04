using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script should assign different values to different enemies(ex. grunt -> tank, with tank taking 3 - 4 shots to kill)
public class EnemyController : MonoBehaviour
{
    //for testing only
    //will replace this with a field to assign different enemy types
    [SerializeField] private float health;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

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
