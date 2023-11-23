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
        HIT,
        DEAD
    };

    public Monster monster;
    public MonsterState enumState = MonsterState.IDLE;

    public MStateMachine mStateMachine { get; private set; }
    // 각 상태를 갖고 있을 딕셔너리
    private Dictionary<MonsterState, IMonsterState> dictionaryState = new Dictionary<MonsterState, IMonsterState>();

    public int currentHp;       // 현재 HP의 정보를 담을 변수

    public void Start()
    {
        currentHp = monster.hp;
        IMonsterState idle = new MonsterIdle();
        IMonsterState move = new MonsterMove();
        IMonsterState search = new MonsterSearch();
        IMonsterState attack = new MonsterAttack();
        IMonsterState hit = new MonsterHit();
        IMonsterState dead = new MonsterDead();

        // 각 상태를 딕셔너리로 저장
        dictionaryState.Add(MonsterState.IDLE, idle);
        dictionaryState.Add(MonsterState.MOVE, move);
        dictionaryState.Add(MonsterState.SEARCH, search);
        dictionaryState.Add(MonsterState.ATTACK, attack);
        dictionaryState.Add(MonsterState.HIT, hit);
        dictionaryState.Add(MonsterState.DEAD, dead);

        // 입력받은 상태를 처리해 줄 StateMachine 초기화
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
        // 몬스터의 hp가 0보다 작거나 같으면 Dead상태
        if (monster.hp <= 0)
        {
            ChangeState(MonsterState.DEAD);
        }

        // 몬스터 피격상태 체크
        if (monster.hp < currentHp)
        {
            currentHp = monster.hp;

            // 고정형 몬스터는 Hit상태 가기전에 리턴
            if (monster.moveSpeed == 0)
            {
                return;
            }
            ChangeState(MonsterState.HIT);
        }

        if (enumState != MonsterState.HIT && enumState != MonsterState.DEAD)
        {
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
        }

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