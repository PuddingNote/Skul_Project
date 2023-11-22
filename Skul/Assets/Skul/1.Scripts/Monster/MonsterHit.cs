using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : IMonsterState
{
    private MonsterController mController;
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.HIT;

        // �ִϸ��̼� ��� ��������� ��� ������ ǰ
        mController.monster.monsterAni.StopPlayback();
        mController.monster.monsterAni.SetBool("isHit", true);
    }

    public void StateUpdate()
    {
        if (mController.monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // Hit�ִϸ��̼� ������ idle���·� ������ȯ
            MonsterIdle idle = new MonsterIdle();
            mController.mStateMachine.SetState(idle);
        }
    }
    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isHit", false);
    }
}
