using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : IPlayerState
{
    private PlayerController pController;
    private Vector3 localScale;         // 바라보는방향 전환 변수
    private Vector3 direction;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.MOVE;

        //Debug.Log(pController.enumState);

        pController.player.playerAni.SetBool("isWalk", true);
        localScale = pController.player.transform.localScale;
    }

    public void StateUpdate()
    {
        MoveAndDirection();
    }

    public void StateExit()
    {
        pController.player.playerAni.SetBool("isWalk", false);
    }

    // 입력하는 키 방향으로 이동 및 방향 바꾸는 함수
    private void MoveAndDirection()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
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
