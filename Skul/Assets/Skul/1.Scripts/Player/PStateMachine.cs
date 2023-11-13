using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PStateMachine// : MonoBehaviour
{
    public Action<IPlayerState> onChangeState;
    private PlayerController pController;
    public IPlayerState lastState;

    public IPlayerState currentState
    { 
        get;
        private set;
    }

    public PStateMachine(IPlayerState defaultState, PlayerController _pController)
    {
        onChangeState += SetState;
        currentState = defaultState;

        //Debug.Log(currentState);

        pController = _pController;
        SetState(currentState);
    }

    public void SetState(IPlayerState state)
    {
        if (currentState == state)
        {
            return;
        }
        lastState = currentState;
        currentState.StateExit();
        currentState = state;
        currentState.StateEnter(pController);
    }

    public void DoUpdate()
    {
        currentState.StateUpdate();
    }
}
