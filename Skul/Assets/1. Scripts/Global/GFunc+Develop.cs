using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���Ӱ����Լ� + Develop
public static partial class GFunc
{
    #region Print log func
    // DEBUG.MODE ��ó���� ��ȣ�� Ȱ��ȭ�� ��쿡�� ȣ�� ������ �α� ��� �Լ�
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);
#endif
    }

    // DEBUG.MODE ��ó���� ��ȣ�� Ȱ��ȭ�� ��쿡�� ȣ�� ������ �α� ��� �Լ� (���ؽ�Ʈ ����)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message, UnityEngine.Object context)
    {
#if DEBUG_MODE
        Debug.Log(message, context);
#endif
    }

    // DEBUG_MODE ��ó���� ��ȣ�� Ȱ��ȭ�� ��쿡�� ȣ�� ������ �α� ��� �Լ� (��� �޽���)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
#if DEBUG_MODE
        Debug.LogWarning(message);
#endif 
    }
    #endregion



    #region Assert for debug
    // DEBUG_MODE ��ó���� ��ȣ�� Ȱ��ȭ�� ��쿡�� ȣ�� ������ ��� �Լ�
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE  // Debug.Assert�� ȣ���Ͽ� ������ �˻��ϰ� �����ϸ� ��� ǥ��
        Debug.Assert(condition);
#endif
    }

    // DEBUG_MODE ��ó���� ��ȣ�� Ȱ��ȭ�� ��쿡�� ȣ�� ������ ��� �Լ� (�޽��� ����)
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition, object message)
    {
#if DEBUG_MODE  // Debug.Assert�� ȣ���Ͽ� ������ �˻��ϰ� �����ϸ� ��� ǥ���ϰ� �޽��� ���
        Debug.Assert(condition, message);
#endif
    }
    #endregion



    #region Vaild Func
    // ��ü�� ��ȿ���� Ȯ���ϴ� ���׸� Ȯ�� �޼���
    public static bool IsValid<T>(this T component_)
    {
        // ��ü�� null�� �ƴ� ��� ��ȿ���� ��ȯ
        bool isValid = component_.Equals(null) == false;
        return isValid;
    }
    #endregion


}