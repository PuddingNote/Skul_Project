using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerData관련 ScriptableObject로 생성
[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Object/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;                                  // 이름
    public string PlayerName { get { return playerName; } }

    [SerializeField]
    private int attackDamage;                                   // 데미지
    public int AttackDamage { get { return attackDamage; } }

    [SerializeField]
    private float moveSpeed;                                    // 이동속도
    public float MoveSpeed { get { return moveSpeed; } }
}
