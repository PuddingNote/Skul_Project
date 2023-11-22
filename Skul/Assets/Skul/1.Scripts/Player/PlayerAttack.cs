using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : IPlayerState
{
    private PlayerController pController;
    private Vector3 direction;          // �̵��� ���� ����
    private Vector3 localScale;         // ������ȯ ����

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.ATTACK;
        localScale = pController.player.transform.localScale;
        //Debug.Log(pController.enumState);

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
        ComboAttack();
        ExitJumpAttack();
    }

    public void StateExit()
    {
        //Debug.Log("��������");
        pController.player.playerAni.SetBool("isAttackA", false);
        pController.player.playerAni.SetBool("isAttackB", false);
        pController.player.playerAni.SetBool("isJumpAttack", false);
    }

    // �������
    private void ComboAttack()
    {
        // ����A �ִϸ��̼� ���̰� 0.5 ~ 1 ���̿� ����Ű�Է½� ����B�� ����
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

        // ���߰��� �߿��� �̵�����
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

    // ���߰����� �� ��� ���� �ൿ ���ϴ� �Լ�
    private void ExitJumpAttack()
    {
        // ���� �ִϸ��̼��� ������ ��
        if (pController.player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f
            && pController.player.playerAni.GetCurrentAnimatorStateInfo(0).IsName("JumpAttack"))
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
            //pController.pStateMachine.onChangeState?.Invoke(nextState);
            if (pController.pStateMachine.onChangeState != null)
            {
                pController.pStateMachine.onChangeState(nextState);
            }
        }
    }

}
