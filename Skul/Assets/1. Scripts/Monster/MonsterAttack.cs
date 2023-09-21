using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : IMonsterState
{
    private MonsterController mController;

    // ���� ���� ȣ��
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.ATTACK;  // ATTACK ���·� ����

        // ����Ÿ�Կ� ���� ���۰��� ����
        if (mController.monster.hasAdditionalAttack == true)
        {
            Debug.Log($"{mController.monster._name}���ݽ���B");
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
        else
        {
            Debug.Log($"{mController.monster._name}���ݽ���A");
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }

    }

    public void StateFixedUpdate()
    {
        
    }

    public void StateUpdate()
    {
        // ���� �������� �ִϸ��̼��� ������ ������ ����
        if (mController.monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            return;
        }

        // ����Ÿ���� 1���� ����
        if (mController.monster.hasAdditionalAttack == false)
        {
            return;
        }
        ChangeAttackType();

    }

    public void StateExit()
    {
        if (mController.monster.hasAdditionalAttack == false)
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
            mController.monster.monsterAni.SetBool("isAttackB", false);
        }

    }

    // Ÿ���� �Ÿ��� �����Ͽ� ����Ÿ���� �ٲٴ� �Լ�
    private void ChangeAttackType()
    {
        Vector3 targetPos = mController.monster.tagetSearchRay.hit.transform.position;
        float distance = Vector2.Distance(targetPos, mController.monster.transform.position);

        // Ÿ�ٰ� �ڽ��� �Ÿ��� ���ݹ��� ���� �۰ų������� AttackA(�ٰŸ�), ũ�� AttackB(���Ÿ�) ����
        if (distance <= mController.monster.attackRange)
        {
            mController.monster.monsterAni.SetBool("isAttackB", false);
            mController.monster.monsterAni.SetBool("isAttackA", true);
        }
        else
        {
            mController.monster.monsterAni.SetBool("isAttackA", false);
            mController.monster.monsterAni.SetBool("isAttackB", true);
        }
    }

}