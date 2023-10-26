using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    // 몬스터의 상태 종류
    public enum MonsterState
    {
        IDLE = 0,
        MOVE,
        SEARCH,
        ATTACK,
        DEAD
    };

    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE;      // 몬스터의 현재상태 체크 변수

    public StateMachine stateMachine { get; private set; }
    private Dictionary<MonsterState, IMonsterState> dictionaryState = new Dictionary<MonsterState, IMonsterState>(); // 각 상태를 갖고 있을 딕셔너리

    void Start()
    {
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new TargetSearch();
        IMonsterState attack = new MonsterAttack();
        IMonsterState dead = new MonsterDead();

        // 각 상태를 딕셔너리로 저장
        dictionaryState.Add(MonsterState.IDLE, idle);
        dictionaryState.Add(MonsterState.MOVE, move);
        dictionaryState.Add(MonsterState.SEARCH, search);
        dictionaryState.Add(MonsterState.ATTACK, attack);
        dictionaryState.Add(MonsterState.DEAD, dead);

        // 입력받은 상태를 처리해 줄 StateMachine 초기화
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
        // 몬스터의 hp가 0보다 작거나 같으면 Dead상태
        if (monster.hp <= 0)
        {
            ChangeState(MonsterState.DEAD);
        }

        // 몬스터의 탐색범위에 타겟이 없고 Move상태가 아닐경우
        if (monster.tagetSearchRay.hit == null && enumState != MonsterState.MOVE)
        {
            ChangeState(MonsterState.MOVE);
        }

        // 몬스터의 탐색범위에 타겟이 있을 경우
        if (monster.tagetSearchRay.hit != null)
        {
            float distance = Vector2.Distance(monster.transform.position, monster.tagetSearchRay.hit.transform.position);

            // 타겟과 자신의 거리가 공격사거리보다 크면 Search상태, 작으면 Attack상태로 전환
            if (monster.attackRange < distance)
            {
                // 공격애니메이션이 끝날경우 Search상태로 전환
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
        stateMachine.DoUpdate();
    }

    // interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    //// 코루틴을 대신 종료시켜줄 함수
    //public void StopCoroutineDeligate(IEnumerator func)
    //{
    //    StopCoroutine(func);
    //}

}