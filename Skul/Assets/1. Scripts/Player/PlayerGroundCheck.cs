using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public RaycastHit2D hit; // Ground üũ ����

    void Update()
    {
        // �÷��̾��� ��ġ�� Ground�� �ִ��� ���߿� �ִ��� Ȯ���ϴ� BoxCast
        hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 0.2f), 0f, Vector2.down, 0.5f, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }
}
