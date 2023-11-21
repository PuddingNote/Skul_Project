using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : IPlayerState
{
    private PlayerController pController;
    private float dashForce = 10f;          // 대쉬속도
    private float dashTime = 0.3f;          // 대쉬 지속 시간
    private int dashCount = 0;              // 2단 대쉬 변수
    private float dashCooldown = 1f;        // 대쉬 쿨다운

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DASH;
        Debug.Log(pController.enumState);
    }

    public void StateUpdate()
    {
        UseDash();
    }

    public void StateExit()
    {
        // 대쉬를 점프로 캔슬하는게 맞나?
        // 대쉬를 점프로 캔슬했을 때 velocity가 zero가되면 바로 낙하 하는걸 방지
        // 대쉬 상태를 나갈 때 플레이어의 velocity 초기화
        pController.player.playerRb.velocity = Vector2.zero;
        pController.player.playerAni.SetBool("isFall", false);
    }

    private void UseDash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && pController.canDash == true)
        {
            pController.CoroutineDeligate(Dash());
        }
    }

    // 대쉬하는 코루틴
    IEnumerator Dash()
    {
        IPlayerState lastState;

        // 대쉬전 상태를 Action에 저장
        if (pController.isGroundRay.hit.collider != null)
        {
            lastState = new PlayerIdle();
        }
        else
        {
            lastState = new PlayerJump();
        }
        //Debug.Log($"대쉬시작");
        pController.canDash = false;
        pController.player.playerAni.SetBool("isDash", true);
        dashCount += 1;

        // 대쉬중 Gravity영향을 받지 않음
        float originalGravity = pController.player.playerRb.gravityScale;
        pController.player.playerRb.gravityScale = 0f;
        pController.player.playerRb.velocity = new Vector2(pController.player.transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);

        // 대쉬가 끝나면 Gravity 원래 값으로 되돌림
        pController.player.playerRb.gravityScale = originalGravity;
        pController.player.playerAni.SetBool("isDash", false);

        // 대쉬가 끝나면 강제로 이전 상태로 전환
        //pController.pStateMachine.onChangeState?.Invoke(lastState);
        if (pController.pStateMachine.onChangeState != null)
        {
            pController.pStateMachine.onChangeState(lastState);
        }

        // 2단 대쉬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown);
            dashCount = 0;
        }
        pController.canDash = true;

    }

}
