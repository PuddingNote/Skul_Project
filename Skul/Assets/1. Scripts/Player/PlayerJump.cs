using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0;                  //2�� ���� ����
    private float jumpForce = 7f;
    private Vector3 direction;
    private Vector3 localScale;                 //������ȯ ����

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.JUMP;
        Debug.Log($"Player Jump? {pController.enumState}");
        if (pController.isGround == true)
        {
            jumpCount = 0;
        }
        pController.isGround = false;
        localScale = pController.player.transform.localScale;
    }

    public void StateUpdate()
    {
        JumpAndMove();
        Jump();

    }

    public void StateExit()
    {
        // player �� isGrounded�ΰ� üũ�ؼ� grounded�� 0 �ƴϸ� �ȹٲ�
        Debug.Log(pController.isGround);
        if (pController.isGround == true)
        {
            jumpCount = 0;
        }

        pController.player.playerAni.SetBool("isJump", false);
    }

    // JumpState ������ä �Է� Ű �������� �̵��ϸ鼭 �ٶ󺸴� �Լ�
    private void JumpAndMove()
    {
        // ���߿� �� ������ ����
        // �Է¹��� Ű �������� �̵��ϸ鼭 �ٶ󺸴� ó��
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && pController.isGround == false)
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

    // �����ϴ� �Լ�
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.C) && jumpCount < 2)
        {
            pController.player.playerAni.SetBool("isJump", true);
            pController.player.playerRb.velocity = pController.player.transform.up * jumpForce;
            jumpCount += 1;
        }
    }
}
