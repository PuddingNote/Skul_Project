using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEffect : MonoBehaviour
{
    private Animator deadAni;

    void Start()
    {
        deadAni = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // 애니메이션 끝나면 파괴
        if (deadAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
