using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private GameObject target;

    private NavMeshAgent enemyAgent;

    private EnemyAnimationController animationController;

    private bool isPaused, isAtDestination;
    private float currentHealth;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isPaused = false;
        isAtDestination = false;

        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.SetDestination(target.transform.position);
        animationController = GetComponent<EnemyAnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        LookAtTarget();
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
        {
            isPaused = true;
            enemyAgent.ResetPath();   
        }
        else
        {
            bool gamePaused = GameController.Instance.IsGamePaused();
            if (gamePaused) return;

            isPaused = false;

            if (currentHealth > 0)
                enemyAgent.SetDestination(target.transform.position);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > 0)
            animationController.Play("Hit");
        else
            animationController.Play("Death");
    }

    public void OnDeath()
    {
        ObjectPoolManager.Instance.DespawnGameObject(gameObject);
    }

    private void LookAtTarget()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
    }

    private void Attack()
    {
        animationController.Play("Attack");
        isAtDestination = true;
    }

    
}
