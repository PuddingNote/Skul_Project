using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global �Լ�
public static partial class GFunc
{
    // Ư�� GameObject�� �ڽ� ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject FindChildObj(this GameObject targetObj, string objName)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        // ��� �ڽ� ������Ʈ���� �ݺ��ϸ鼭 �˻�
        for (int i = 0; i < targetObj.transform.childCount; i++)
        {
            searchTarget = targetObj.transform.GetChild(i).gameObject;

            // ��ġ�ϴ� ��� ��ȯ
            if (searchTarget.name.Equals(objName))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                // ��ġ���� ������ ����Լ��� �̿��ؼ� ���� �ڽ� ������Ʈ�� �˻�
                searchResult = FindChildObj(searchTarget, objName);

                if (searchResult == null || searchResult == default)
                {
                    // �˻� ����� ���ų� �⺻���̸� ���� �ڽ� ������Ʈ�� �˻�
                }
                else
                {
                    return searchResult;
                }
            }
        }

        // ��� �ڽ� ������Ʈ�� �˻��� �� ����� ���ٸ� null��ȯ
        return searchResult;
    }

    // �ش� Object�� ��� ���� �ڽĿ�����Ʈ���� ��������� Ȱ��ȭ�ϴ� �Լ�
    public static void SetActiveRecursively(GameObject obj, bool state)
    {
        if (obj == null)
        {
            return;
        }

        obj.SetActive(state);

        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, state);
        }
    }

}
