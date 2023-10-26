using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// 게임관련함수
public static partial class GFunc
{
    // 게임 종료 함수
    public static void QuitThisGame()
    {
#if UNITY_EDITOR
        // UNITY_EDITOR 전처리기 기호가 활성화된 경우 (에디터에서 실행 중인 경우)
        // 에디터 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else   
        // UNITY_EDITOR 전처리기 기호가 비활성화된 경우 (실제 빌드에서 실행 중인 경우)
        // 어플리케이션 종료
        Application.Quit();
#endif
    }


    // 씬 로드 함수
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


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


    #region Print log func
    // DEBUG.MODE 전처리기 기호가 활성화된 경우에만 호출 가능한 로그 출력 함수
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    // DEBUG.MODE 전처리기 기호가 활성화된 경우에만 호출 가능한 로그 출력 함수 (컨텍스트 지정)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message, UnityEngine.Object context)
    {
#if DEBUG_MODE
        Debug.Log(message, context);
#endif
    }

    // DEBUG_MODE 전처리기 기호가 활성화된 경우에만 호출 가능한 로그 출력 함수 (경고 메시지)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif 
    }
    #endregion


    #region Assert for debug
    // DEBUG_MODE 전처리기 기호가 활성화된 경우에만 호출 가능한 어설션 함수
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE  // Debug.Assert를 호출하여 조건을 검사하고 실패하면 어설션 표시
        Debug.Assert(condition);
#endif
    }

    // DEBUG_MODE 전처리기 기호가 활성화된 경우에만 호출 가능한 어설션 함수 (메시지 포함)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition, object message)
    {
#if DEBUG_MODE  // Debug.Assert를 호출하여 조건을 검사하고 실패하면 어설션 표시하고 메시지 출력
        Debug.Assert(condition, message);
#endif
    }
    #endregion


    #region Vaild Func
    // 객체의 유효성을 확인하는 제네릭 확장 메서드
    public static bool IsValid<T>(this T component_)
    {
        // 객체가 null이 아닌 경우 유효함을 반환
        bool isValid = component_.Equals(null) == false;
        return isValid;
    }
    #endregion


}