using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : IMonsterState
{
    private MonsterController mController;

    // 상태 들어갈때 호출
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.IDLE;    // IDLE상태로 설정
    }

    public void StateFixedUpdate() 
    {
        
    }

    public void StateUpdate() 
    {
        
    }

    public void StateExit()
    {
        
    }

}