using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 변경 스크립트
public class TitleAnyKeyClick : MonoBehaviour
{
    public void TitleSceneChange(int value)
    {
        SceneManager.LoadScene(value);
    }
}