using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float health = 1f;
    [SerializeField] private GameObject target;
    
    private NavMeshAgent enemyAgent;

    private EnemyAnimationController animationController;

    private bool isPaused = false, isAtDestination = false;

    private void Awake()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.SetDestination(target.transform.position);

        animationController = GetComponent<EnemyAnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPaused || isAtDestination) return;

        if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
        {
            if (!enemyAgent.hasPath || enemyAgent.velocity.sqrMagnitude == 0f)
                Attack();
        }
    }

    public void TogglePause()
    {
        if (!isPaused)
            isPaused = true;
        else
            isPaused = false;
    }

    public void ToggleAgentPath()
    {
        if (enemyAgent.hasPath)
            enemyAgent.ResetPath();
        else
            enemyAgent.SetDestination(target.transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health > 0)
            animationController.Play("Hit");
        else
            animationController.Play("Death");
    }

    public void OnDeath()
    {
        //temporary, replace with Singleton method
        Destroy(gameObject);
    }

    private void Attack()
    {
        animationController.Play("Attack");
        isAtDestination = true;
        enemyAgent.ResetPath();
    }

    
}