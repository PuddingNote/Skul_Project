using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ���� ��ũ��Ʈ
public class TitleAnyKeyClick : MonoBehaviour
{

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            
        }
    }

    public void TitleSceneChange(int value)
    {
        SceneManager.LoadScene(value);
    }

}