using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    MonsterController mController;

    public IMonsterState currentState
    {
        get;
        private set;
    }

    public StateMachine(IMonsterState defaultState, MonsterController _mController)
    {
        currentState = defaultState;
        mController = _mController;
        SetState(currentState);
    }

    public void SetState(IMonsterState state)
    {
        if (currentState == state)
        {
            return;
        }

        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(mController);
    }

    public void DoFixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

    public void DoUpdate()
    {
        currentState.StateUpdate();
    }

}