using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckRay : MonoBehaviour
{
    public RaycastHit2D hit;
    [HideInInspector] public bool isRight = true; // ������ȯ üũ�ϴ� ����
    
    void Start()
    {
        /*Do Nothing*/
    }

    void Update()
    {
        // ���׿����ڷ� ������ȯ üũ
        Debug.DrawRay(transform.position, isRight == true ? new Vector2(1, -1).normalized * 2 : new Vector2(-1, -1).normalized * 2, Color.red);
        hit = Physics2D.Raycast(transform.position, isRight == true ? new Vector2(1, -1).normalized : new Vector2(-1, -1).normalized, 2, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }

}