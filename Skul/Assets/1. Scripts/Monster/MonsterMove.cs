using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : IMonsterState
{
    private int offsetX;                    // �̵����� ����
    private bool exitState;                 // �ڷ�ƾ while�� Ż������
    private Vector3 localScale;             // �ٶ󺸴¹��� ��ȯ ����
    private MonsterController mController;

    // ���� ���� ȣ��
    public void StateEnter(MonsterController _mController)
    {
        mController = _mController;
        mController.enumState = MonsterController.MonsterState.MOVE;    // MOVE���·� ����
        Debug.Log($"{mController.monster.name}�̵�����");
        mController.monster.monsterAni.SetBool("isWalk", true);
        exitState = false;
        localScale = mController.monster.transform.localScale;

        mController.CoroutineDeligate(randomPosX());
    }

    public void StateFixedUpdate()
    {
        
    }

    public void StateUpdate()
    {
        Move();
    }

    public void StateExit()
    {
        exitState = true;
        mController.monster.monsterAni.SetBool("isWalk", false);
    }

    // ���� �̵���Ű�� �Լ�
    private void Move()
    {
        // BigWooden�� move X
        if (mController.monster._name == "BigWooden")
        {
            return;
        }
        ChangeIdleAni();
        ChangeLookDirection();
        GroundCheck();

        mController.monster.transform.Translate(new Vector2(offsetX, 0f) * mController.monster.moveSpeed * Time.deltaTime);
    }

    // offsetX���� 0�϶� idle�ִϷ� ��ȯ�ϴ� �Լ�
    private void ChangeIdleAni()
    {
        if (offsetX == 0)
        {
            mController.monster.monsterAni.SetBool("isWalk", false);
            mController.monster.monsterAni.SetBool("isIdle", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isIdle", false);
            mController.monster.monsterAni.SetBool("isWalk", true);
        }
    }

    // �̵��� �������� �ٶ󺸴� ���� ��ȯ�ϴ� �Լ�
    private void ChangeLookDirection()
    {
        // offsetX���� ���� �ٶ󺸴� ����ó��
        if (offsetX != 0)
        {
            // offsetX���� 0���� ������ ����, 0���� ũ�� ������
            if (offsetX < 0)
            {
                mController.monster.groundCheckRay.isRight = false;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (offsetX > 0)
            {
                mController.monster.groundCheckRay.isRight = true;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            // raycast���⵵ ���� ��ȯ
        }
    }

    // Ÿ�ϸ� ���� ����� �� �ݴ�������� ��ȯ�ϴ� �Լ�
    private void GroundCheck()
    {
        // �׶���üũ���̾ ���� �������� ���� ��� �ݴ�������� ��ȯ
        if (mController.monster.groundCheckRay.hit.collider == null)
        {
            if (localScale.x < 0)
            {
                mController.monster.groundCheckRay.isRight = true;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
                offsetX *= -1;
            }
            else if (localScale.x > 0)
            {
                mController.monster.groundCheckRay.isRight = false;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
                offsetX *= -1;
            }
        }
    }

    // 2�ʸ��� �̵���ų ������ ���ϰ� �ٶ󺸴� ������ �ٲٴ� �ڷ�ƾ�Լ�
    private IEnumerator randomPosX()
    {
        while (exitState == false)
        {
            if (exitState)
            {
                yield break;
            }
            offsetX = Random.Range(-1, 2);
            yield return new WaitForSeconds(3f);
        }
    }

}