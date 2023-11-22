using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PStateMachine// : MonoBehaviour
{
    private PlayerController pController;
    public Action<IPlayerState> onChangeState;

    // 읽기 전용
    public IPlayerState currentState
    {
        get;
        private set;
    }

    // 생성자 초기화시 기본상태세팅
    public PStateMachine(IPlayerState defaultState, PlayerController _pController)
    {
        // 초기화시 Action에 SetState함수 저장
        onChangeState += SetState;
        //onChangeState += (state) =>
        //{
        //    SetState(state);
        //};
        currentState = defaultState;

        //Debug.Log(currentState);

        pController = _pController;
        SetState(currentState);
    }

    // 입력받은 상태를 체크하여 상태전환하는 함수
    public void SetState(IPlayerState state)
    {
        // 전과 동일하면 리턴
        if (currentState == state)
        {
            return;
        }
        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(pController);
    }

    public void DoUpdate()
    {
        currentState.StateUpdate();
    }
}
