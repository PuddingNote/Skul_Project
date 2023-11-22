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

    // 싱글톤 패턴 적용
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

    // 현재 씬 이름 가져오기
    public string GetThisSceneName()
    {
        thisSceneName = SceneManager.GetActiveScene().name;
        return thisSceneName;
    }

    // 씬 로드 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        //GetThisSceneName();
    }

}
