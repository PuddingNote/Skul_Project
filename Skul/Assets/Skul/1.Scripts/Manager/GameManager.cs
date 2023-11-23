using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public float totalTime;             // �÷���Ÿ�� üũ
    public int monsterRemainingSum;     // ���� ���� ��
    public int killCount;               // ųī��Ʈ üũ
    public float totalDamage;           // ���� �� ������ üũ

    public static GameManager Instance
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

    // �̱��� ���� ����
    // �޸� ���� ���� & �߿� ������ ȿ���� ���� & �� ��ȯ�� ������ ����
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitGameManager();
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

    public void InitGameManager()
    {
        totalTime = Time.time;
        monsterRemainingSum = 0;
        killCount = 0;
        totalDamage = 0;
    }

}