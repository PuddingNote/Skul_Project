using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IPlayerState
{
    private PlayerController pController;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.ATTACK;
        //Debug.Log($"Player Attack? {pController.enumState}");

        if (pController.isGroundRay.hit.collider != null)
        {
            //Debug.Log("기본공격");
            pController.player.playerAni.SetBool("isAttackA", true);
        }
        else
        {
            //Debug.Log("공중공격");
            pController.player.playerAni.SetBool("isJumpAttack", true);
        }
    }

    public void StateUpdate()
    {
        ExitAttack();
    }

    public void StateExit()
    {
        //Debug.Log("공격종료");
        pController.player.playerAni.SetBool("isAttackA", false);
        pController.player.playerAni.SetBool("isAttackB", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    }

    // 공중공격을 한 경우 다음 행동 정하는 함수
    private void ExitAttack()
    {
        // 공격 애니메이션이 끝나면 들어감
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            IPlayerState nextState;

            // 플레이어가 땅인 경우 Idle, 공중인 경우 Jump 상태로 전환
            if (pController.isGroundRay.hit.collider != null)
            {
                nextState = new PlayerIdle();
            }
            else
            {
                nextState = new PlayerJump();
            }

            // 공격이 끝나면 다음 상태로 전환
            pController.pStateMachine.onChangeState?.Invoke(nextState);
        }
    }

}
