using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public string _name;
    [HideInInspector] public int skulIndex;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float groundCheckLength;
    [HideInInspector] public float skillACool;
    [HideInInspector] public float skillBCool;
    [HideInInspector] public Sprite skulSprite;
    [HideInInspector] public Rigidbody2D playerRb;
    [HideInInspector] public Animator playerAni;
    [HideInInspector] private CapsuleCollider2D playerCollider;

    // PlayerData�� �ʱ�ȭ�ϴ� �Լ�
    public void InitPlayerData(PlayerData data)
    {
        this._name                                      = data.name;
        this.skulIndex                                  = data.SkulIndex;
        this.attackDamage                               = data.AttackDamage;
        this.moveSpeed                                  = data.MoveSpeed;
        this.groundCheckLength                          = data.GroundRayLength;
        this.skillACool                                 = data.SkillACool;
        this.skillBCool                                 = data.SkillBCool;
        this.skulSprite                                 = data.SkulSprite;
        this.playerRb                                   = gameObject.GetComponent<Rigidbody2D>();
        this.playerAni                                  = gameObject.GetComponent<Animator>();
        this.playerAni.runtimeAnimatorController        = data.Controller;
        this.playerCollider                             = gameObject.GetComponent<CapsuleCollider2D>();
        this.playerCollider.size                        = new Vector2(data.ColliderSizeX, data.ColliderSizeY);
    }

    //// �÷��̾� AttackA
    //public virtual void AttackA()
    //{
        
    //}

    //// �÷��̾� AttackB
    //public virtual void AttackB()
    //{
        
    //}

    //// �÷��̾� SkillA
    //public virtual void SkillA()
    //{
        
    //}

    //// �÷��̾� SkillB
    //public virtual void SkillB()
    //{
        
    //}
}
