using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonsterData���� ScriptableObject�� ����
[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData", order = int.MaxValue)]
public class MonsterData : ScriptableObject
{
    [SerializeField] private string monsterName;                        // �̸�
    public string MonsterName { get { return monsterName; } }

    [SerializeField] private int monsterHp;                             // ����ü��
    public int MonsterHp { get { return monsterHp; } }

    [SerializeField] private int monsterMaxHp;                          // �ִ�ü��
    public int MonsterMaxHp { get { return monsterMaxHp; } }

    [SerializeField] private int attackDamage;                          // ������
    public int AttackDamage { get { return attackDamage; } }
    
    [SerializeField] private float moveSpeed;                           // �̵��ӵ�
    public float MoveSpeed { get { return moveSpeed; } }
    
    [SerializeField] private float sightRangeX;                         // X�� �þ߹���
    public float SightRangeX { get { return sightRangeX; } }
    
    [SerializeField] private float sightRangeY;                         // y�� �þ߹���
    public float SightRangeY { get { return sightRangeY; } }
    
    [SerializeField] private float attackRange;                         // ���ݹ���
    public float AttackRange { get { return attackRange; } }
    
    [SerializeField] private bool hasAdditionalAttack;                  // ��ų��������
    public bool HasAdditionalAttack { get { return hasAdditionalAttack; } }

}