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
        HIT,
        DEAD
    };

    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE;

    public MStateMachine mStateMachine { get; private set; }
    // �� ���¸� ���� ���� ��ųʸ�
    private Dictionary<MonsterState, IMonsterState> dictionaryState = new Dictionary<MonsterState, IMonsterState>();

    public int currentHp;       // ���� HP�� ������ ���� ����

    public void Start()
    {
        currentHp = monster.hp;
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new MonsterSearch();
        IMonsterState attack = new MonsterAttack();
        IMonsterState hit = new MonsterHit();
        IMonsterState dead = new MonsterDead();

        // �� ���¸� ��ųʸ��� ����
        dictionaryState.Add(MonsterState.IDLE, idle);
        dictionaryState.Add(MonsterState.MOVE, move);
        dictionaryState.Add(MonsterState.SEARCH, search);
        dictionaryState.Add(MonsterState.ATTACK, attack);
        dictionaryState.Add(MonsterState.HIT, hit);
        dictionaryState.Add(MonsterState.DEAD, dead);

        // �Է¹��� ���¸� ó���� �� StateMachine �ʱ�ȭ
        mStateMachine = new MStateMachine(idle, this);
    }

    public void Update()
    {
        SelectState();
        mStateMachine.DoUpdate();
    }

    public void ChangeState(MonsterState p_state)
    {
        if (dictionaryState[p_state] == null)
        {
            return;
        }

        mStateMachine.SetState(dictionaryState[p_state]);
    }
    
    public void SelectState()
    {
        // ������ hp�� 0���� �۰ų� ������ Dead����
        if (monster.hp <= 0)
        {
            ChangeState(MonsterState.DEAD);
        }

        // ���� �ǰݻ��� üũ
        if (monster.hp < currentHp)
        {
            currentHp = monster.hp;

            // ������ ���ʹ� Hit���� �������� ����
            if (monster.moveSpeed == 0)
            {
                return;
            }
            ChangeState(MonsterState.HIT);
        }

        if (enumState != MonsterState.HIT && enumState != MonsterState.DEAD)
        {
            // ������ Ž�������� Ÿ���� ���� Move���°� �ƴҰ��
            if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
            {
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
                        ChangeState(MonsterState.SEARCH);
                    }
                }
                else
                {
                    ChangeState(MonsterState.ATTACK);
                }
            }
        }

    }

    // interface�� ��ӹ��� Ŭ������ MonoBehaviour�� ��� ���� ���ؼ� �ڷ�ƾ�� ��� ��������� �Լ�
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    //// �ڷ�ƾ�� ��� ��������� �Լ�
    //public void StopCoroutineDeligate(IEnumerator func)
    //{
    //    StopCoroutine(func);
    //}

}