using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    MonsterController mController;

    // ���� ���¿� ���� ������Ƽ
    public IMonsterState currentMonsterState
    {
        get;
        private set;
    }

    public StateMachine(IMonsterState defaultState, MonsterController _mController)
    {
        currentMonsterState = defaultState;     // �ʱ���� ����
        mController = _mController;             // MonsterController ����
        SetState(currentMonsterState);          // �ʱ���� ���� �޼��� ȣ��
    }

    // ���º��� �޼���
    public void SetState(IMonsterState state)
    {
        // �̹� ���� ���¿� ������ ���·� �����Ϸ��� �ϸ� ��ȯ
        if (currentMonsterState == state)
        {
            return;
        }

        currentMonsterState.StateExit();               // ���� ���¸� ���������� �޼��� ȣ��
        currentMonsterState = state;                   // ���ο� ���·� ����
        currentMonsterState.StateEnter(mController);   // ���ο� ���·� �������� �޼��� ȣ��
    }

    // Update �ÿ� ���� ������ Update �޼��� ȣ��
    public void DoUpdate()
    {
        currentMonsterState.StateUpdate();
    }

}