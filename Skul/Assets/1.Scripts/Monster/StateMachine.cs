using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    MonsterController mController;

    // 현재 상태에 대한 프로퍼티
    public IMonsterState currentMonsterState
    {
        get;
        private set;
    }

    public StateMachine(IMonsterState defaultState, MonsterController _mController)
    {
        currentMonsterState = defaultState;     // 초기상태 설정
        mController = _mController;             // MonsterController 참조
        SetState(currentMonsterState);          // 초기상태 설정 메서드 호출
    }

    // 상태변경 메서드
    public void SetState(IMonsterState state)
    {
        // 이미 현재 상태와 동일한 상태로 변경하려고 하면 반환
        if (currentMonsterState == state)
        {
            return;
        }

        currentMonsterState.StateExit();               // 현재 상태를 나가기위한 메서드 호출
        currentMonsterState = state;                   // 새로운 상태로 설정
        currentMonsterState.StateEnter(mController);   // 새로운 상태로 들어가기위한 메서드 호출
    }

    // Update 시에 현재 상태의 Update 메서드 호출
    public void DoUpdate()
    {
        currentMonsterState.StateUpdate();
    }

}