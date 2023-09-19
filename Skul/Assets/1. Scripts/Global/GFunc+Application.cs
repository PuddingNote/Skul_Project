using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 게임관련함수 + Application
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
}