using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private int setWidth = 1920;
    private int setHeight = 1080;

    void Awake()
    {
        Screen.SetResolution(setWidth, setHeight, false);
    }
    
}