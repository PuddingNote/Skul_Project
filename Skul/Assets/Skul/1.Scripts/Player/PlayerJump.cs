using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0;                  // 2�� ���� ����
    private float jumpForce = 7f;               // ���� ���� ����
    private Vector3 direction;                  // �̵� ���� ����
    private Vector3 localScale;                 // ���� ��ȯ ����
    //private GameObject jumpEffect;              //��������Ʈ ����

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.JUMP;
        //jumpEffect = pController.gameObject.FindChildObj("JumpEffect");
        Debug.Log(pController.enumState);

        if (pController.isGroundRay.hit.collider != null)
        {
            jumpCount = 0;
        }
        localScale = pController.player.transform.localScale;
    }

    public void StateUpdate()
    {
        //JumpEffectOff();
        JumpAndMove();
        Jump();
        PlayerFall();
    }

    public void StateExit()
    {
        // �������¸� ���� �� �÷��̾ Ground ���� ������ jumpCount �ʱ�ȭ
        // if (pController.isGround == true)
        // {
        //     jumpCount = 0;
        // }
        pController.player.playerAni.SetBool("isJump", false);
        pController.player.playerAni.SetBool("isFall", false);
    }

    // JumpState ������ä �Է� Ű �������� �̵��ϸ鼭 �ٶ󺸴� �Լ�
    private void JumpAndMove()
    {
        // ���߿� �� ������ ����
        // �Է¹��� Ű �������� �̵��ϸ鼭 �ٶ󺸴� ó��
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && pController.isGroundRay.hit.collider == null)
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

    // �÷��̾ ���ϸ� �����ϸ� Fall�ִϷ� ��ȯ�ϴ� �Լ�
    private void PlayerFall()
    {
        if (pController.player.playerRb.velocity.y < -1)
        {
            pController.player.playerAni.SetBool("isFall", true);
        }
    }

    // �����ϴ� �Լ�
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X) && jumpCount < 2)
        {
            if (jumpCount == 1)
            {
                Vector3 playerVelocity = pController.player.playerRb.velocity;
                pController.player.playerRb.velocity = new Vector3(playerVelocity.x, 0, playerVelocity.z);

                //jumpEffect.SetActive(true);
            }
            pController.player.playerAni.SetBool("isJump", true);
            pController.player.playerRb.velocity = pController.player.transform.up * jumpForce;
            jumpCount += 1;
        }
    }

    //private void JumpEffectOff()
    //{
    //    if (jumpEffect.GetComponentMust<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
    //    {
    //        jumpEffect.SetActive(false);
    //    }
    //}
}
