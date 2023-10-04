using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearch : IMonsterState
{
    private MonsterController mController;
    private Vector3 targetPos;

    // ���� ���� ȣ��
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        Debug.Log($"{mController.monster.name}Ÿ�ټ�ġ �߰ݽ���");
        mController.enumState = MonsterController.MonsterState.SEARCH;  // SEARCH ���·� ����
        mController.monster.monsterAni.SetBool("isWalk", true);
    }

    public void StateUpdate()
    {
        LookAndFollowTaget();
    }

    // Ÿ���� �Ѿư��� �Լ�
    private void LookAndFollowTaget()
    {
        targetPos = mController.monster.tagetSearchRay.hit.transform.position;  // Ÿ����ġ ��������
        Vector3 targetDirection = (targetPos - mController.monster.transform.position).normalized;  // ���ͷκ��� Ÿ�ٹ��� ���

        // Ÿ�ٰ� �ڽ��� �Ÿ��� x�� ��ġ�� ���� �ٶ󺸴¹��� �� �׶���üũ���̾� ���� ��ȯ
        if (targetDirection.x != 0)
        {
            // targetDirection.x�� 0���� ������ Ÿ���� ����, 0���� ũ�� Ÿ���� �����ʿ� ����
            if (targetDirection.x < 0)
            {
                // ���Ͱ� ������ �ٶ󺸰��ϰ� GroundCheckRay ������Ʈ
                mController.monster.groundCheckRay.isRight = false;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (targetDirection.x > 0)
            {
                // ���Ͱ� �������� �ٶ󺸰��ϰ� GroundCheckRay ������Ʈ
                mController.monster.groundCheckRay.isRight = true;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
        }
        // ���͸� Ÿ�ٹ������� �̵�
        mController.monster.transform.Translate(new Vector3(targetDirection.x, 0, targetDirection.z) * mController.monster.moveSpeed * Time.deltaTime);
    }

    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isWalk", false);
    }

}