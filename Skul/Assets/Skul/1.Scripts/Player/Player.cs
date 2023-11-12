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

    // PlayerData를 초기화하는 함수
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

    // 플레이어 AttackA
    public virtual void AttackA()
    {
        
    }

    // 플레이어 AttackB
    public virtual void AttackB()
    {
        
    }

    // 플레이어 SkillA
    public virtual void SkillA()
    {
        
    }

    // 플레이어 SkillB
    public virtual void SkillB()
    {
        
    }
}
