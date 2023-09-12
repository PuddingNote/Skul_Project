using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    {
        offset = Time.time * speed;
        bgRender.material.mainTextureOffset = new Vector2(offset, 0f);
    }
}
