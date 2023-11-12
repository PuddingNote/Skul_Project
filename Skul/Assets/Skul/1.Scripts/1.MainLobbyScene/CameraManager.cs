using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카메라 관련 스크립트
public class CameraManager : MonoBehaviour
{
    private int setWidth = 1920;    // 화면 너비
    private int setHeight = 1080;   // 화면 높이

    void Awake()
    {
        Screen.SetResolution(setWidth, setHeight, false);   // 화면 해상도 설정 Screen.SetResolution(너비, 높이, 전체 화면 여부)
    }
    
}