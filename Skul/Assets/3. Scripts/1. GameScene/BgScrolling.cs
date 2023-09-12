using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경 스크롤링 스크립트
public class BgScrolling : MonoBehaviour
{
    private float speed = 1f;               // 배경 스크롤링 속도
    private float offset = default;         // 배경 텍스처 offset
    private Renderer bgRender = default;

    void Start()
    {
        bgRender = gameObject.GetComponent<Renderer>();
    }


    void Update()
    {   // 배경 오브젝트의 offset을 변경해서 스크롤링 효과 구현
        offset = Time.time * speed;
        bgRender.material.mainTextureOffset = new Vector2(offset, 0f);
    }
}
