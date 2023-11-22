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

        // 애니메이션 모션 퍼즈상태인 경우 강제로 품
        mController.monster.monsterAni.StopPlayback();
        mController.monster.monsterAni.SetBool("isHit", true);
    }

    public void StateUpdate()
    {
        if (mController.monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            // Hit애니매이션 끝나면 idle상태로 강제전환
            MonsterIdle idle = new MonsterIdle();
            mController.mStateMachine.SetState(idle);
        }
    }
    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isHit", false);
    }
}
