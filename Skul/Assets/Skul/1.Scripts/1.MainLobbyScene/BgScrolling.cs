using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ��ũ�Ѹ� ��ũ��Ʈ
public class BgScrolling : MonoBehaviour
{
    private float speed = 1f;               // ��� ��ũ�Ѹ� �ӵ�
    private float offset = default;         // ��� �ؽ�ó offset
    private Renderer bgRender = default;

    void Start()
    {
        bgRender = gameObject.GetComponent<Renderer>();
    }


    void Update()
    {   // ��� ������Ʈ�� offset�� �����ؼ� ��ũ�Ѹ� ȿ�� ����
        offset = Time.time * speed;
        bgRender.material.mainTextureOffset = new Vector2(offset, 0f);
    }
}