using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonsterData관련 ScriptableObject로 생성
[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField] private string monsterName;                        // 이름
    public string MonsterName { get { return monsterName; } }

    [SerializeField] private int monsterHp;                             // 현재체력
    public int MonsterHp { get { return monsterHp; } }

    [SerializeField] private int monsterMaxHp;                          // 최대체력
    public int MonsterMaxHp { get { return monsterMaxHp; } }

    [SerializeField] private int attackDamage;                          // 데미지
    public int AttackDamage { get { return attackDamage; } }
    
    [SerializeField] private float moveSpeed;                           // 이동속도
    public float MoveSpeed { get { return moveSpeed; } }
    
    [SerializeField] private float sightRangeX;                         // X축 시야범위
    public float SightRangeX { get { return sightRangeX; } }
    
    [SerializeField] private float sightRangeY;                         // y축 시야범위
    public float SightRangeY { get { return sightRangeY; } }
    
    [SerializeField] private float attackRange;                         // 공격범위
    public float AttackRange { get { return attackRange; } }
    
    [SerializeField] private bool hasAdditionalAttack;                  // 스킬보유여부
    public bool HasAdditionalAttack { get { return hasAdditionalAttack; } }

}