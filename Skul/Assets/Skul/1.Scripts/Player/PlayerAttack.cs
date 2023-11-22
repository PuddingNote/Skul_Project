using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IPlayerState
{
    private PlayerController pController;
    private Vector3 direction;          // 이동할 방향 변수
    private Vector3 localScale;         // 방향전환 변수

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.ATTACK;
        localScale = pController.player.transform.localScale;
        //Debug.Log(pController.enumState);

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
        ComboAttack();
        ExitJumpAttack();
    }

    public void StateExit()
    {
        //Debug.Log("공격종료");
        pController.player.playerAni.SetBool("isAttackA", false);
        pController.player.playerAni.SetBool("isAttackB", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    }

    // 연계공격
    private void ComboAttack()
    {
        // 공격A 애니메이션 길이가 0.5 ~ 1 사이에 공격키입력시 공격B로 연계
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("AttackA")
        && (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f
        && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                pController.player.playerAni.SetBool("isAttackA", false);
                pController.player.playerAni.SetBool("isAttackB", true);
            }
        }

        // 공중공격 중에는 이동가능
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
        {
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) 
                && pController.isGroundRay.hit.collider == null)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    localScale = new Vector3(1, localScale.y, localScale.z);
                    direction = Vector3.right;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    localScale = new Vector3(-1, localScale.y, localScale.z);
                    direction = Vector3.left;
                }
                pController.player.transform.localScale = localScale;
                pController.player.transform.Translate(direction * pController.player.moveSpeed * Time.deltaTime);
            }
        }
    }

    // 공중공격을 한 경우 다음 행동 정하는 함수
    private void ExitJumpAttack()
    {
        // 공격 애니메이션이 끝나면 들어감
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
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
            //pController.pStateMachine.onChangeState?.Invoke(nextState);
            if (pController.pStateMachine.onChangeState != null)
            {
                pController.pStateMachine.onChangeState(nextState);
            }
        }
    }

}
