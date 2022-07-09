using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private float maxSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        maxSpeed = animator.speed;
    }

    private void OnEnable()
    {
        animator.speed = maxSpeed;

        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Hit");
        animator.ResetTrigger("Death");
    }

    public void TogglePause()
    {
        if (animator.speed > 0)
            animator.speed = 0;
        else
            animator.speed = maxSpeed;
    }

    public void Play(string state)
    {
        animator.SetTrigger(state);

        if (state == "Hit" || state == "Death")
            StartCoroutine(WaitForAnimation(state));
    }

    private IEnumerator WaitForAnimation(string state)
    {
        EnemyController enemy = GetComponent<EnemyController>();
        enemy.TogglePause();

        yield return new WaitForSeconds(GetCurrentAnimationLength());

        enemy.TogglePause();

        if (state == "Death")
            enemy.OnDeath();

        animator.ResetTrigger(state);
    }

    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = animatorState.normalizedTime % 1;
        return animationLength;
    }
}
