using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : IMonsterState
{
    private MonsterController mController;

    // ���� ���� ȣ��
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.IDLE;    // IDLE���·� ����
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