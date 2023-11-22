using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private static GameSceneManager instance = null;

    public string thisSceneName;

    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                return null;
            }
            return instance;
        }
    }

    // �̱��� ���� ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void Update()
    {
        GetThisSceneName();
    }

    // ���� �� �̸� ��������
    public string GetThisSceneName()
    {
        thisSceneName = SceneManager.GetActiveScene().name;
        return thisSceneName;
    }

    // �� �ε� �Լ�
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //GetThisSceneName();
    }

}
