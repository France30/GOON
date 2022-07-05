using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        enemy.ToggleAgentPath();

        //Debug.Log(GetCurrentAnimationLength());

        yield return new WaitForSeconds(GetCurrentAnimationLength());

        enemy.TogglePause();

        if (state == "Death")
            enemy.OnDeath();
        else
            enemy.ToggleAgentPath();
    }

    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo animatorState = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = animatorState.normalizedTime % 1;
        return animationLength;
    }
}
