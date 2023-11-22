using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int monsterRemainingNumber;  // 남은 몬스터 수
    public float totalTime;             // 플레이타임 체크
    public float killCount;             // 킬카운트 체크
    public int totalDamage;             // 가한 총 데미지 체크
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

    // 싱글톤 패턴 적용
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