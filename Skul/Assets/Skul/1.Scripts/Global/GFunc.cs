using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // �� �ε� �Լ�
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
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

}
