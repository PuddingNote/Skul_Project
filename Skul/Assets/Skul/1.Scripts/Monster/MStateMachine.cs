using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MStateMachine
{
    MonsterController mController;

    // ���� ���¿� ���� ������Ƽ
    public IMonsterState currentState
    {
        get;
        private set;
    }

    public MStateMachine(IMonsterState defaultState, MonsterController _mController)
    {
        currentState = defaultState;     // �ʱ���� ����
        mController = _mController;      // MonsterController ����
        SetState(currentState);          // �ʱ���� ���� �޼��� ȣ��
    }

    // ���º��� �޼���
    public void SetState(IMonsterState state)
    {
        // �̹� ���� ���¿� ������ ���·� �����Ϸ��� �ϸ� ��ȯ
        if (currentState == state)
        {
            return;
        }

        currentState.StateExit();               // ���� ���¸� ���������� �޼��� ȣ��
        currentState = state;                   // ���ο� ���·� ����
        currentState.StateEnter(mController);   // ���ο� ���·� �������� �޼��� ȣ��
    }

    // Update �ÿ� ���� ������ Update �޼��� ȣ��
    public void DoUpdate()
    {
        currentState.StateUpdate();
    }

}