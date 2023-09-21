using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRay : MonoBehaviour
{
    public RaycastHit2D hit;
    [HideInInspector] public bool isRight = true; // ������ȯ üũ�ϴ� ����
    [HideInInspector] Vector2 direction;

    void Update()
    {
        // isRight�� true�� ���������� �ƴϸ� �������� Ray ���
        if (isRight == true)
        {
            direction = new Vector2(1, -1).normalized;
        }
        else
        {
            direction = new Vector2(-1, -1).normalized;
        }
        Debug.DrawRay(transform.position, direction * 2, Color.red);    // ����׿� : �ٴ� üũ Ray ǥ��

        // �ٴ� �浹 ���� �� hit�� ����
        hit = Physics2D.Raycast(transform.position, direction, 2, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }

}