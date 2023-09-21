using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임관련함수 + Obj
public static partial class GFunc
{
    // 특정 GameObject의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(this GameObject targetObj, string objName)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        // 자식 오브젝트들을 반복하면서 검색
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
                // 재귀적으로 자식 오브젝트 검색
                searchResult = FindChildObj(searchTarget, objName);

                // 방어로직 : 검색 결과가 null인 경우를 처리
                if (searchResult == null || searchResult == default) 
                { 
                    
                }
                else 
                { 
                    return searchResult; 
                }
            }
        }

        return searchResult;
    }

    // 씬의 루트 오브젝트를 서치해서 찾아주는 함수
    public static GameObject GetRootObj(string objName)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs = activeScene_.GetRootGameObjects();

        GameObject targetObj = default;

        // 루트 오브젝트들을 반복하여 검색
        foreach (GameObject rootObj in rootObjs)
        {
            // 일치하는 경우 반환
            if (rootObj.name.Equals(objName))
            {
                targetObj = rootObj;
                return targetObj;
            }
            else 
            { 
                continue; 
            }
        }

        return targetObj;
    }

    // 현재 활성화 되어 있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }

}