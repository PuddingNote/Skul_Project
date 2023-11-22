using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ���� ��ũ��Ʈ
public class TitleAnyKeyClick : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            UIManager.Instance.InitUIManager();
            GameManager.Instance.InitGameManager();
            GameSceneManager.Instance.LoadScene(GData.MAINLOBBY_SCENE_NAME);
        }
    }

    //public void TitleSceneChange(int value)
    //{
    //    SceneManager.LoadScene(value);
    //}

}