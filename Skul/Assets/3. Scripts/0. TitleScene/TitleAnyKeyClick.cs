using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ���� ��ũ��Ʈ
public class TitleAnyKeyClick : MonoBehaviour
{
    public void TitleSceneChange(int value)
    {
        SceneManager.LoadScene(value);
    }
}