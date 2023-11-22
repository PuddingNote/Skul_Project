using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int monsterRemainingNumber;  // ���� ���� ��
    public float totalTime;             // �÷���Ÿ�� üũ
    public float killCount;             // ųī��Ʈ üũ
    public int totalDamage;             // ���� �� ������ üũ
    private static GameManager instance = null;

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

    void Update()
    {

    }

    public void InitGameManager()
    {
        monsterRemainingNumber = 0;
        totalTime = Time.time;
        killCount = 0;
        totalDamage = 0;
    }

}