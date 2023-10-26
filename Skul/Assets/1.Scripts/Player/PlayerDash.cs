using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : IPlayerState
{
    private PlayerController pController;
    private float dashForce = 10f;          // �뽬�ӵ�
    private float dashTime = 0.3f;          // �뽬 ���� �ð�
    private int dashCount = 0;              // 2�� �뽬 ����
    private float dashCooldown = 1f;        // �뽬 ��ٿ�

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DASH;
        //Debug.Log($"Player Dash? {pController.enumState}");
    }

    public void StateUpdate()
    {
        UseDash();
        //FallState();
    }

    public void StateExit()
    {
        // �뽬�� ������ ĵ������ �� velocity�� zero���Ǹ� �ٷ� ���� �ϴ°� ����
        // �뽬 ���¸� ���� �� �÷��̾��� velocity �ʱ�ȭ
        pController.player.playerRb.velocity = Vector2.zero;
        pController.player.playerAni.SetBool("isFallRepeat", false);
    }

    private void UseDash()
    {
        if (Input.GetKeyDown(KeyCode.Z) && pController.canDash == true)
        {
            pController.CoroutineDeligate(Dash());
        }
    }

    //// �뽬�� ������ �� ���߿� �ִ� ��� ���ϸ�ǽ����ϴ� �Լ�
    //private void FallState()
    //{
    //    if (pController.isGround == false)
    //    {
    //        pController.player.playerAni.SetBool("isFallRepict", true);
    //    }
    //}

    // �뽬�ϴ� �ڷ�ƾ
    private IEnumerator Dash()
    {
        IPlayerState lastState;

        // �뽬�� ���¸� Action�� ����
        if (pController.isGroundRay.hit.collider != null)
        {
            lastState = new PlayerIdle();
        }
        else
        {
            lastState = new PlayerJump();
        }
        //Debug.Log($"�뽬����");
        pController.canDash = false;
        pController.player.playerAni.SetBool("isDash", true);
        dashCount += 1;

        // �뽬�� Gravity������ ���� ����
        float originalGravity = pController.player.playerRb.gravityScale;
        pController.player.playerRb.gravityScale = 0f;
        pController.player.playerRb.velocity = new Vector2(pController.player.transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime);

        // �뽬�� ������ Gravity ���� ������ �ǵ���
        pController.player.playerRb.gravityScale = originalGravity;
        pController.player.playerAni.SetBool("isDash", false);

        // �뽬�� ������ ������ ���� ���·� ��ȯ�ϴ� ActionEvent => �뽬 ���� Ż��
        pController.pStateMachine.onChangeState?.Invoke(lastState);

        // 2�� �뽬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown);
            dashCount = 0;
        }
        pController.canDash = true;

    }

}