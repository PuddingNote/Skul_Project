using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : StateMachineBehaviour
{
    Transform enemyTransform;
    Enemy enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<Enemy>();
        enemyTransform = animator.GetComponent<Transform>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(enemy.player.position, enemyTransform.position) > 4f)
        {
            animator.SetBool("IsBack", true);
            animator.SetBool("IsFollow", false);
        }
        else if (Vector2.Distance(enemy.player.position,enemyTransform.position) > 1f)
        {
            enemyTransform.position = Vector2.MoveTowards(enemyTransform.position, enemy.player.position, Time.deltaTime * enemy.speed);
        }
        else
        {
            animator.SetBool("IsBack", false);
            animator.SetBool("IsFollow", false);
        }
        enemy.DirectionEnemy(enemy.player.position.x, enemyTransform.position.x);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }
}
