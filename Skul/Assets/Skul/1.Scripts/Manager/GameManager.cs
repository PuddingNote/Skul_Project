using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public float totalTime;             // 플레이타임 체크
    public int monsterRemainingSum;     // 남은 몬스터 수
    public int killCount;               // 킬카운트 체크
    public float totalDamage;           // 가한 총 데미지 체크

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
    // 메모리 낭비 방지 & 중요 데이터 효율적 관리 & 씬 전환시 데이터 유지
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