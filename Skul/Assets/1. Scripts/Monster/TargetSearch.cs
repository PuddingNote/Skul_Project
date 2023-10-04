using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearch : IMonsterState
{
    private MonsterController mController;
    private Vector3 targetPos;

    // 상태 들어갈때 호출
    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        Debug.Log($"{mController.monster.name}타겟서치 추격시작");
        mController.enumState = MonsterController.MonsterState.SEARCH;  // SEARCH 상태로 설정
        mController.monster.monsterAni.SetBool("isWalk", true);
    }

    public void StateUpdate()
    {
        LookAndFollowTaget();
    }

    // 타겟을 쫓아가는 함수
    private void LookAndFollowTaget()
    {
        targetPos = mController.monster.tagetSearchRay.hit.transform.position;  // 타겟위치 가져오기
        Vector3 targetDirection = (targetPos - mController.monster.transform.position).normalized;  // 몬스터로부터 타겟방향 계산

        // 타겟과 자신의 거리의 x값 위치를 비교해 바라보는방향 및 그라운드체크레이어 방향 전환
        if (targetDirection.x != 0)
        {
            // targetDirection.x가 0보다 작으면 타겟은 왼쪽, 0보다 크면 타겟은 오른쪽에 있음
            if (targetDirection.x < 0)
            {
                // 몬스터가 왼쪽을 바라보게하고 GroundCheckRay 업데이트
                mController.monster.groundCheckRay.isRight = false;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(-1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
            else if (targetDirection.x > 0)
            {
                // 몬스터가 오른쪽을 바라보게하고 GroundCheckRay 업데이트
                mController.monster.groundCheckRay.isRight = true;
                var localScale = mController.monster.transform.localScale;
                localScale = new Vector3(1, localScale.y, localScale.z);
                mController.monster.transform.localScale = localScale;
            }
        }
        // 몬스터를 타겟방향으로 이동
        mController.monster.transform.Translate(new Vector3(targetDirection.x, 0, targetDirection.z) * mController.monster.moveSpeed * Time.deltaTime);
    }

    public void StateExit()
    {
        mController.monster.monsterAni.SetBool("isWalk", false);
    }

}