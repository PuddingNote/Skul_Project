using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerData���� ScriptableObject�� ����
[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;                                  // �̸�
    public string PlayerName { get { return playerName; } }

    [SerializeField]
    private int attackDamage;                                   // ������
    public int AttackDamage { get { return attackDamage; } }

    [SerializeField]
    private float moveSpeed;                                    // �̵��ӵ�
    public float MoveSpeed { get { return moveSpeed; } }
}
