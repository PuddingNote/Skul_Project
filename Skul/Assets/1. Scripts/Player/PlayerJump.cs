using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : IPlayerState
{
    private PlayerController pController;
    private int jumpCount = 0;                  //2단 점프 변수
    private float jumpForce = 7f;
    private Vector3 direction;
    private Vector3 localScale;                 //방향전환 변수

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
        // player 가 isGrounded인걸 체크해서 grounded면 0 아니면 안바꿈
        Debug.Log(pController.isGround);
        if (pController.isGround == true)
        {
            jumpCount = 0;
        }

        pController.player.playerAni.SetBool("isJump", false);
    }

    // JumpState 유지한채 입력 키 방향으로 이동하면서 바라보는 함수
    private void JumpAndMove()
    {
        // 공중에 떠 있으면 시작
        // 입력받은 키 방향으로 이동하면서 바라보는 처리
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

    // 점프하는 함수
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
