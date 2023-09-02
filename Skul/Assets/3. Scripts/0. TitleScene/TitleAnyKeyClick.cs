using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleAnyKeyClick : MonoBehaviour
{
    public void TitleSceneChange(int value)
    {
        SceneManager.LoadScene(value);
    }
}