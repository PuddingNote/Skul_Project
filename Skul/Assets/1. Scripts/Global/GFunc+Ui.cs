using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ���Ӱ����Լ� + UI
public static partial class GFunc
{
    // TextMeshPro ������ ������Ʈ�� �ؽ�Ʈ�� �����ϴ� �Լ�
    public static void SetTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();

        // ������ TextMeshPro ������Ʈ�� ���ų� �⺻��(default)�� ��� �ƹ� �۾��� �������� ����
        if (tmpTxt == null || tmpTxt == default(TMP_Text))
        {
            return;
        }

        // ������ TextMeshPro ������Ʈ�� �����ϴ� ���
        tmpTxt.text = text_;
    }
}
