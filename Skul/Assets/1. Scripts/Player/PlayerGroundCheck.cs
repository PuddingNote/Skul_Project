using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public RaycastHit2D hit; // Ground 체크 변수

    void Update()
    {
        // 플레이어의 위치가 Ground에 있는지 공중에 있는지 확인하는 BoxCast
        hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.5f, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }
}
