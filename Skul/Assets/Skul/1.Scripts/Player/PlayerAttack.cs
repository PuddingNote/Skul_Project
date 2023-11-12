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
            //Debug.Log("�⺻����");
            pController.player.playerAni.SetBool("isAttackA", true);
        }
        else
        {
            //Debug.Log("���߰���");
            pController.player.playerAni.SetBool("isJumpAttack", true);
        }
    }

    public void StateUpdate()
    {
        ExitAttack();
    }

    public void StateExit()
    {
        //Debug.Log("��������");
        pController.player.playerAni.SetBool("isAttackA", false);
        pController.player.playerAni.SetBool("isAttackB", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    }

    // ���߰����� �� ��� ���� �ൿ ���ϴ� �Լ�
    private void ExitAttack()
    {
        // ���� �ִϸ��̼��� ������ ��
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            IPlayerState nextState;

            // �÷��̾ ���� ��� Idle, ������ ��� Jump ���·� ��ȯ
            if (pController.isGroundRay.hit.collider != null)
            {
                nextState = new PlayerIdle();
            }
            else
            {
                nextState = new PlayerJump();
            }

            // ������ ������ ���� ���·� ��ȯ
            pController.pStateMachine.onChangeState?.Invoke(nextState);
        }
    }

}
