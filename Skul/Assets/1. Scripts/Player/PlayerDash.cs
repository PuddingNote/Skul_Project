using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : IPlayerState
{
    private PlayerController pController;
    private float dashForce = 10f;
    private float dashTime = 0.3f;
    private int dashCount = 0;
    private float dashCooldown = 1f;
    private Vector3 direction;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DASH;
        Debug.Log($"Player Dash? {pController.enumState}");
    }

    public void StateUpdate()
    {
        UseDash();
        FallState();
    }

    public void StateExit()
    {
        pController.player.playerAni.SetBool("isFallRepict", false);
    }

    private void UseDash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && pController.canDash == true)
        {
            pController.CoroutineDeligate(Dash());
        }
    }

    // �뽬�� ������ �� ���߿� �ִ� ��� ���ϸ�ǽ����ϴ� �Լ�
    private void FallState()
    {
        if (pController.isGround == false)
        {
            pController.player.playerAni.SetBool("isFallRepict", true);
        }
    }

    // �뽬�ϴ� �ڷ�ƾ
    private IEnumerator Dash()
    {
        var lastState = pController.pStateMachine.lastState;
        Debug.Log($"�뽬����");
        pController.canDash = false;
        pController.player.playerAni.SetBool("isDash", true);
        dashCount += 1;
        float originalGravity = pController.player.playerRb.gravityScale;
        pController.player.playerRb.gravityScale = 0f;
        // pController.player.playerRb.velocity = new Vector2(pController.player.transform.localScale.x * dashForce, 0f);

        pController.player.playerRb.velocity = Vector2.zero;
        for (int i = 0; i < 30; i++)
        {
            var tick = dashTime / 30;
            yield return new WaitForSeconds(tick);
            pController.player.transform.Translate(new Vector2(pController.player.transform.localScale.x, 0f) * 8f * Time.fixedDeltaTime);
        }

        // yield return new WaitForSeconds(dashTime);
        pController.player.playerRb.gravityScale = originalGravity;
        pController.player.playerAni.SetBool("isDash", false);
        if (pController.enumState == PlayerController.PlayerState.DASH)
        {
            pController.player.playerRb.velocity = Vector2.zero;
        }

        // 2�� �뽬
        if (dashCount >= 2)
        {
            // �뽬�� ������ ���� ���·� ��ȯ�ϴ� ActionEvent
            pController.pStateMachine.onChangeState?.Invoke(lastState);
            yield return new WaitForSeconds(dashCooldown);
            dashCount = 0;
            pController.canDash = true;
            yield break;
        }
        pController.canDash = true;

        // �뽬�� ������ ���� ���·� ��ȯ�ϴ� ActionEvent
        pController.pStateMachine.onChangeState?.Invoke(lastState);
    }

}
