using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRay : MonoBehaviour
{
    public RaycastHit2D hit;
    [HideInInspector] public bool isRight = true; // 방향전환 체크하는 변수
    [HideInInspector] Vector2 direction;

    void Update()
    {
        // isRight가 true면 오른쪽으로 아니면 왼쪽으로 Ray 쏘기
        if (isRight == true)
        {
            direction = new Vector2(1, -1).normalized;
        }
        else
        {
            direction = new Vector2(-1, -1).normalized;
        }
        Debug.DrawRay(transform.position, direction * 2, Color.red);    // 디버그용 : 바닥 체크 Ray 표시

        // 바닥 충돌 감지 후 hit에 저장
        hit = Physics2D.Raycast(transform.position, direction, 2, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }

}