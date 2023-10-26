using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator normalWoodenAni;

    private RaycastHit2D hit;
    private float direction;            // 방향
    private Vector3 attackdirection;    // 공격방향

    void Awake()
    {
        monsterController = gameObject.GetComponent<MonsterController>();   
        monsterData = Resources.Load("NormalWooden") as MonsterData;    // NormalWooden 리소스를 로드해서 MonsterData로 변환
        InitMonsterData(monsterData);                                   // 몬스터 데이터 초기화

        // 몬스터컨트롤러에서 몬스터타입 변수로 접근가능하게 하기 위한 처리
        // 몬스터컨트롤러의 몬스터타입 변수에 자신을 몬스터로 캐스팅하여 저장
        monsterController.monster = (Monster)(this as Monster);
        normalWoodenAni = gameObject.GetComponent<Animator>();
    }

    // 공격하는 함수(공격을 애니메이션 이벤트로 처리함)
    public override void AttackA()
    {
        Vector2 attackArea = new Vector2(0.5f, 1.5f);               // 공격 영역
        direction = transform.localScale.x;                         // 몬스터 방향 확인
        attackdirection = new Vector3(direction, 0f).normalized;    // 공격 방향 설정

        // Boxcast로 피격처리
        hit = Physics2D.BoxCast(transform.position, attackArea, 0f, attackdirection, 1f, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit.collider != null)
        {
            // 플레이어 체력 감소
            PlayerController target = hit.collider.gameObject.GetComponent<PlayerController>();     
            int attackDamage = monsterController.monster.attackDamage;
            target.playerHp -= attackDamage;
            Debug.Log($"노말우든 공격! 플레이어 Hp = {target.playerHp}/{target.playerMaxHp}");
        }
    }

    // 공격애니메이션이 종료되면 코루틴 실행
    public void ExitAttack()
    {
        StartCoroutine(AttackDelay());
    }

    // 공격딜레이 정하는 코루틴 함수
    private IEnumerator AttackDelay()
    {
        // 공격딜레이 중에는 idle모션 처리
        normalWoodenAni.SetBool("isAttackA", false);
        normalWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2f);
        normalWoodenAni.SetBool("isIdle", false);

        // 2초후 몬스터의 현재 상태가 공격이 아니라면 코루틴 종료 => 코루틴들어오고 상태가 변했을 경우 밑에 공격모션을 취소하기 위한 예외처리
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            //Debug.Log($"2초후 상태{monsterController.enumState}");
            yield break;
        }

        // 다시 공격 애니메이션 활성화
        normalWoodenAni.SetBool("isAttackA", true);
    }

    // 디버그용 : 공격 영역 표시
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (attackdirection * 1f), new Vector2(0.5f, 1.5f));
    }

}