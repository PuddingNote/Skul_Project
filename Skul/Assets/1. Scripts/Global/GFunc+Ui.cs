using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 게임관련함수 + UI
public static partial class GFunc
{
    // TextMeshPro 형태의 컴포넌트의 텍스트를 설정하는 함수
    public static void SetTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();

        // 가져온 TextMeshPro 컴포넌트가 없거나 기본값(default)인 경우 아무 작업도 수행하지 않음
        if (tmpTxt == null || tmpTxt == default(TMP_Text))
        {
            return;
        }

        // 가져온 TextMeshPro 컴포넌트가 존재하는 경우
        tmpTxt.text = text_;
    }
}
