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
    public MonsterState enumState = MonsterState.IDLE; // ������ ������¸� üũ�ϱ����� ����
    private StateMachine _stateMachine; // �Է¹��� ���¸� ó���ϱ� ���� StateManchine
    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dicState = new Dictionary<MonsterState, IMonsterState>(); //�� ���¸� ���� ���� ��ųʸ�

    void Start()
    {
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new TagetSearch();
        IMonsterState attack = new MonsterAttack();
        // �� ���¸� ��ųʸ��� ����
        dicState.Add(MonsterState.IDLE, idle);
        dicState.Add(MonsterState.MOVE, move);
        dicState.Add(MonsterState.SEARCH, search);
        dicState.Add(MonsterState.ATTACK, attack);

        // �Է¹��� ���¸� ó���� �� StateMachine �ʱ�ȭ
        stateMachine = new StateMachine(idle, this);
    }

    void Update()
    {
        // ������ Ž�������� Ÿ���� ���� Move���°� �ƴҰ��
        if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
        {
            stateMachine.SetState(dicState[MonsterState.MOVE]);
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
                    stateMachine.SetState(dicState[MonsterState.SEARCH]);
                }
            }
            else
            {
                stateMachine.SetState(dicState[MonsterState.ATTACK]);
            }
        }
        stateMachine.DoUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.DoFixedUpdate();
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