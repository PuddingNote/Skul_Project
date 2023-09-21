using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRay : MonoBehaviour
{
    public RaycastHit2D hit;
    [HideInInspector] public bool isRight = true; // 방향전환 체크하는 변수
    
    void Start()
    {
        
    }

    void Update()
    {
        // isRight가 true면 오른쪽으로 아니면 왼쪽으로 Ray 쏘기
        Debug.DrawRay(transform.position, isRight == true ? new Vector2(1, -1).normalized * 2 : new Vector2(-1, -1).normalized * 2, Color.red);
        
        // 바닥 충돌 감지 후 hit에 저장
        hit = Physics2D.Raycast(transform.position, isRight == true ? new Vector2(1, -1).normalized : new Vector2(-1, -1).normalized, 2, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }

}