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
        //Debug.Log("Player Attack?");

        if (pController.isGroundRay.hit.collider != null)
        {
            //Debug.Log("�⺻����");
            pController.player.playerAni.SetBool("isAttack", true);
        }
        else
        {
            //Debug.Log("���߰���");
            pController.player.playerAni.SetBool("isJumpAttack", true);
        }
    }

    public void StateUpdate()
    {
        UseJumpAttack();
    }

    public void StateExit()
    {
        Debug.Log("��������");
        pController.player.playerAni.SetBool("isAttack", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    }

    // ���߰����� �� ��� ���� �ൿ ���ϴ� �Լ�
    private void UseJumpAttack()
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

            //Debug.Log($"���� �� �� ���� ����{nextState}");
            pController.pStateMachine.onChangeState?.Invoke(nextState);
        }
    }

}
