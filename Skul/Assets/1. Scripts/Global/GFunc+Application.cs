using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ���Ӱ����Լ� + Application
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
}