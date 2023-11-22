using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PStateMachine
{
    private PlayerController pController;
    public Action<IPlayerState> onChangeState;

    public IPlayerState currentState
    {
        get;
        private set;
    }

    // ������ �ʱ�ȭ�� �⺻���¼���
    public PStateMachine(IPlayerState defaultState, PlayerController _pController)
    {
        // �ʱ�ȭ�� Action�� SetState�Լ� ����
        onChangeState += SetState;
        currentState = defaultState;

        //Debug.Log(currentState);

        pController = _pController;
        SetState(currentState);
    }

    // �Է¹��� ���¸� üũ�Ͽ� ������ȯ�ϴ� �Լ�
    public void SetState(IPlayerState state)
    {
        // ���� �����ϸ� ����
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
