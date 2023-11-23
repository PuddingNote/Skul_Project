using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Global 함수
public static partial class GFunc
{
    // 특정 GameObject의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(this GameObject targetObj, string objName)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        // 모든 자식 오브젝트들을 반복하면서 검색
        for (int i = 0; i < targetObj.transform.childCount; i++)
        {
            searchTarget = targetObj.transform.GetChild(i).gameObject;

            // 일치하는 경우 반환
            if (searchTarget.name.Equals(objName))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                // 일치하지 않으면 재귀함수를 이용해서 다음 자식 오브젝트를 검색
                searchResult = FindChildObj(searchTarget, objName);

                if (searchResult == null || searchResult == default)
                {
                    // 검색 결과가 없거나 기본값이면 다음 자식 오브젝트를 검색
                }
                else
                {
                    return searchResult;
                }
            }
        }

        // 모든 자식 오브젝트를 검색한 후 결과가 없다면 null반환
        return searchResult;
    }

    // 해당 Object및 모든 하위 자식오브젝트들을 재귀적으로 활성화하는 함수
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
