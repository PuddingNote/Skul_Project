using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // ������ ���� ����
    public enum MonsterState
    {
        IDLE = 0,
        MOVE,
        SEARCH,
        ATTACK,
        DEAD
    };

    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE;      // ������ ������¸� üũ�ϱ����� ����
    //private StateMachine _stateMachine;                     // �Է¹��� ���¸� ó���ϱ� ���� StateManchine

    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dictionaryState = new Dictionary<MonsterState, IMonsterState>(); // �� ���¸� ���� ���� ��ųʸ�

    void Start()
    {
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new TargetSearch();
        IMonsterState attack = new MonsterAttack();

        // �� ���¸� ��ųʸ��� ����
        dictionaryState.Add(MonsterState.IDLE, idle);
        dictionaryState.Add(MonsterState.MOVE, move);
        dictionaryState.Add(MonsterState.SEARCH, search);
        dictionaryState.Add(MonsterState.ATTACK, attack);

        // �Է¹��� ���¸� ó���� �� StateMachine �ʱ�ȭ
        stateMachine = new StateMachine(idle, this);
    }

    public void ChangeState(MonsterState p_state)
    {
        if (dictionaryState[p_state] == null)
        {
            return;
        }

        stateMachine.SetState(dictionaryState[p_state]);
    }

    void Update()
    {
        // ������ Ž�������� Ÿ���� ���� Move���°� �ƴҰ��
        if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
        {
            //stateMachine.SetState(dictionaryState[MonsterState.MOVE]); // ChangeState �߰� �� ����

            ChangeState(MonsterState.MOVE);
        }

        // ������ Ž�������� Ÿ���� ���� ���
        if (monster.tagetSearchRay.hit != null)
        {
            float distance = Vector2.Distance(monster.transform.position, monster.tagetSearchRay.hit.transform.position);

            // Ÿ�ٰ� �ڽ��� �Ÿ��� ���ݻ�Ÿ����� ũ�� Search����, ������ Attack���·� ��ȯ
            if (monster.attackRange < distance)
            {
                // ���ݾִϸ��̼��� ������� Search���·� ��ȯ
                if (monster.monsterAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    //stateMachine.SetState(dictionaryState[MonsterState.SEARCH]);

                    ChangeState(MonsterState.SEARCH);
                }
            }
            else
            {
                //stateMachine.SetState(dictionaryState[MonsterState.ATTACK]);

                ChangeState(MonsterState.ATTACK);
            }
        }
        stateMachine.DoUpdate();
    }

    // interface�� ��ӹ��� Ŭ������ MonoBehaviour�� ��� ���� ���ؼ� �ڷ�ƾ�� ��� ��������� �Լ�
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    // �ڷ�ƾ�� ��� ��������� �Լ�
    public void StopCoroutineDeligate(IEnumerator func)
    {
        StopCoroutine(func);
    }

}