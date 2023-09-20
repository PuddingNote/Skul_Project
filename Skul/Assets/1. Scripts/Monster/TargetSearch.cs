using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearch : IMonsterState
{
    private MonsterController mController;
    private Vector3 targetPos;

    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        Debug.Log($"{mController.monster.name}Ÿ�ټ�ġ�Ϸ� �߰ݽ���");
        mController.enumState = MonsterController.MonsterState.SEARCH;
        mController.monster.monsterAni.SetBool("isWalk", true);
    }

    public void StateFixedUpdate()
    {
        /*Do Nothing*/
    }

    public void StateUpdate()
    {
        LookAndFollowTaget();
    }

    // Ÿ���� �Ѿư��� �Լ�
    private void LookAndFollowTaget()
    {
        targetPos = mController.monster.tagetSearchRay.hit.transform.position;
        Vector3 targetDirection = (targetPos - mController.monster.transform.position).normalized;
        // Ÿ�ٰ� �ڽ��� �Ÿ��� x�� ��ġ�� ���� �ٶ󺸴¹��� �� �׶���üũ���̾� ���� ��ȯ
        if (targetDirection.x != 0)
        {
            // targetDirection.x�� 0���� ������ Ÿ���� ����, 0���� ũ�� Ÿ���� �����ʿ� ����
            if (targetDirection.x < 0)
            {
                mController.monster.groundCheckRay.isRight = false;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (targetDirection.x > 0)
            {
                mController.monster.groundCheckRay.isRight = true;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
        }
        mController.monster.transform.Translate(new Vector3(targetDirection.x, 0, targetDirection.z) * mController.monster.moveSpeed * Time.deltaTime);
    }

    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isWalk", false);
    }

}