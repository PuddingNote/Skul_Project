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
    private Sprite skulSprite;                                  // �÷��̾� ���� �̹���
    public Sprite SkulSprite { get { return skulSprite; } }

    [SerializeField]
    private int skulIndex;                                      // �÷��̾� ���� ��ȣ
    public int SkulIndex { get { return skulIndex; } }

    [SerializeField]
    private int attackDamage;                                   // ������
    public int AttackDamage { get { return attackDamage; } }

    [SerializeField]
    private float moveSpeed;                                    // �̵��ӵ�
    public float MoveSpeed { get { return moveSpeed; } }

    [SerializeField]
    private RuntimeAnimatorController controller;               // ��Ÿ�Ӿִ���Ʈ�ѷ�
    public RuntimeAnimatorController Controller { get { return controller; } }

    [SerializeField]
    private float colliderSizeX;                                // �ݶ��̴� ������ X
    public float ColliderSizeX { get { return colliderSizeX; } }

    [SerializeField]
    private float colliderSizeY;                                // �ݶ��̴� ������ Y
    public float ColliderSizeY { get { return colliderSizeY; } }

    [SerializeField]
    private float groundRayLength;                              // �׶���üũ���̾� ����
    public float GroundRayLength { get { return groundRayLength; } }

    [SerializeField]
    private float skillACool;                                   // ��ųA ��ٿ�
    public float SkillACool { get { return skillACool; } }

    [SerializeField]
    private float skillBCool;                                   // ��ųB ��ٿ�
    public float SkillBCool { get { return skillBCool; } }
}