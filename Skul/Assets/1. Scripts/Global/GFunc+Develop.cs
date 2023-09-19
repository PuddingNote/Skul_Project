using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임관련함수 + Develop
public static partial class GFunc
{
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