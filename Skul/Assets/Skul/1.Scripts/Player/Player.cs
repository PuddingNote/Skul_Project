using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string _name;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public Rigidbody2D playerRb;
    [HideInInspector] public Animator playerAni;
    [HideInInspector] private CapsuleCollider2D playerCollider;
    [HideInInspector] public float groundCheckLength;

    // PlayerData�� �ʱ�ȭ�ϴ� �Լ�
    public void InitPlayerData(PlayerData data)
    {
        this._name                                      = data.name;
        this.attackDamage                               = data.AttackDamage;
        this.moveSpeed                                  = data.MoveSpeed;
        this.playerRb                                   = gameObject.GetComponent<Rigidbody2D>();
        this.playerAni                                  = gameObject.GetComponent<Animator>();
        this.playerAni.runtimeAnimatorController        = data.Controller;
        this.playerCollider                             = gameObject.GetComponent<CapsuleCollider2D>();
        this.playerCollider.size                        = new Vector2(data.ColliderSizeX, data.ColliderSizeY);
        this.groundCheckLength                          = data.GroundRayLength;
    }

    // �÷��̾� AttackA
    public virtual void AttackA()
    {
        
    }

    // �÷��̾� AttackB
    public virtual void AttackB()
    {
        
    }

    // �÷��̾� SkillA
    public virtual void SkillA()
    {
        
    }

    // �÷��̾� SkillB
    public virtual void SkillB()
    {
        
    }
}
