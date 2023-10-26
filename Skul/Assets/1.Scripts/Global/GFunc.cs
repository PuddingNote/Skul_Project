using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// ���Ӱ����Լ�
public static partial class GFunc
{
    // ���� ���� �Լ�
    public static void QuitThisGame()
    {
#if UNITY_EDITOR
        // UNITY_EDITOR ��ó���� ��ȣ�� Ȱ��ȭ�� ��� (�����Ϳ��� ���� ���� ���)
        // ������ ����
        UnityEditor.EditorApplication.isPlaying = false;
#else   
        // UNITY_EDITOR ��ó���� ��ȣ�� ��Ȱ��ȭ�� ��� (���� ���忡�� ���� ���� ���)
        // ���ø����̼� ����
        Application.Quit();
#endif
    }


    // �� �ε� �Լ�
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    // Ư�� GameObject�� �ڽ� ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject FindChildObj(this GameObject targetObj, string objName)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;

        // �ڽ� ������Ʈ���� �ݺ��ϸ鼭 �˻�
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
                // ��������� �ڽ� ������Ʈ �˻�
                searchResult = FindChildObj(searchTarget, objName);

                // ������ : �˻� ����� null�� ��츦 ó��
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


    // ���� ��Ʈ ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject GetRootObj(string objName)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs = activeScene_.GetRootGameObjects();

        GameObject targetObj = default;

        // ��Ʈ ������Ʈ���� �ݺ��Ͽ� �˻�
        foreach (GameObject rootObj in rootObjs)
        {
            // ��ġ�ϴ� ��� ��ȯ
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


    // ���� Ȱ��ȭ �Ǿ� �ִ� ���� ã���ִ� �Լ�
    public static Scene GetActiveScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        return activeScene;
    }


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