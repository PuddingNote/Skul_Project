using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : IMonsterState
{
    private MonsterController mController;

    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.DEAD;
        mController.monster.gameObject.SetActive(false);
    }

    public void StateUpdate()
    {

    }

    public void StateExit()
    {

    }
}
