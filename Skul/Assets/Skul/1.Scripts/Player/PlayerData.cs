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

    [SerializeField]
    private RuntimeAnimatorController controller;               // 런타임애니컨트롤러
    public RuntimeAnimatorController Controller { get { return controller; } }

    [SerializeField]
    private float colliderSizeX;                                // 콜라이더 사이즈 X
    public float ColliderSizeX { get { return colliderSizeX; } }

    [SerializeField]
    private float colliderSizeY;                                // 콜라이더 사이즈 Y
    public float ColliderSizeY { get { return colliderSizeY; } }

    [SerializeField]
    private float groundRayLength;                              // 그라운드체크레이어 길이
    public float GroundRayLength { get { return groundRayLength; } }
}
