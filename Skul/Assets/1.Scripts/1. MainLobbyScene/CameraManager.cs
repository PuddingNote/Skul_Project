using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ī�޶� ���� ��ũ��Ʈ
public class CameraManager : MonoBehaviour
{
    private int setWidth = 1920;    // ȭ�� �ʺ�
    private int setHeight = 1080;   // ȭ�� ����

    void Awake()
    {
        Screen.SetResolution(setWidth, setHeight, false);   // ȭ�� �ػ� ���� Screen.SetResolution(�ʺ�, ����, ��ü ȭ�� ����)
    }
    
}