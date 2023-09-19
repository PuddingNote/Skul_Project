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
                // 재귀;적으로 자식 오브젝트 검색
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


    // RectTransform 에서 sizeDelta를 찾아서 리턴하는 함수
    public static Vector2 GetRectSizeDelta(this GameObject obj)
    {
        return obj.GetComponentMust<RectTransform>().sizeDelta;
    }


    // 현재 활성화 되어 있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }


    // GameObject의 로컬 포지션을 설정하는 함수
    public static void SetLocalPos(this GameObject obj, float x, float y, float z)
    {
        obj.transform.localPosition = new Vector3(x, y, z);
    }


    // GameObject의 로컬 포지션을 변경하는 함수
    public static void AddLocalPos(this GameObject obj, float x, float y, float z)
    {
        obj.transform.localPosition = obj.transform.localPosition + new Vector3(x, y, z);
    }


    // Transform을 사용해서 GameObject를 이동하는 함수
    public static void Translate(this Transform transform, Vector2 moveVector)
    {
        transform.Translate(moveVector.x, moveVector.y, 0f);
    }


    // GameObject에서 컴포넌트를 가져오는 함수
    public static T GetComponentMust<T>(this GameObject obj)
    {
        T component = obj.GetComponent<T>();

        GFunc.Assert(component.IsValid<T>() != false, string.Format("{0}에서 {1}을(를) 찾을 수 없습니다.", obj.name, component.GetType().Name));

        return component;
    }


    // GameObject의 자식오브젝트에서 컴포넌트를 찾아오는 오버로딩 함수
    public static T GetComponentMust<T>(this GameObject obj, string objName)
    {
        T component = obj.FindChildObj(objName).GetComponent<T>();

        GFunc.Assert(component.IsValid<T>() != false, string.Format("{0}에서 {1}을(를) 찾을 수 없습니다.", obj.name, component.GetType().Name));

        return component;
    }


    // GameObject에서 RectTransform 컴포넌트를 가져오는 함수
    public static RectTransform GetRect(this GameObject obj)
    {
        return obj.GetComponent<RectTransform>();
    }


    // GameObject의 앵커 포지션을 변경하는 함수
    public static void AddAnchoredPos(this GameObject obj, Vector2 position2D)
    {
        obj.GetRect().anchoredPosition += position2D;
    }


    // 새로운 GameObject를 생성하고 컴포넌트를 추가하여 리턴하는 함수
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }

}