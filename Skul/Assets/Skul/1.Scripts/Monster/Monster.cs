using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D monsterRb;
    [HideInInspector] public Animator monsterAni;
    [HideInInspector] public GroundCheckRay groundCheckRay;
    [HideInInspector] public TargetSearchRay tagetSearchRay;

    [HideInInspector] public string _name;
    [HideInInspector] public int hp;
    [HideInInspector] public int maxHp;
    [HideInInspector] public int attackDamage;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public float sightRangeX;
    [HideInInspector] public float sightRangeY;
    [HideInInspector] public float attackRange;
    [HideInInspector] public bool hasAdditionalAttack;

    public void InitMonsterData(MonsterData data)
    {
        this.monsterRb                  = gameObject.GetComponent<Rigidbody2D>();
        this.monsterAni                 = gameObject.GetComponent<Animator>();
        this.groundCheckRay             = gameObject.GetComponent<GroundCheckRay>();
        this.tagetSearchRay             = gameObject.GetComponent<TargetSearchRay>();

        this._name                      = data.name;
        this.hp                         = data.MonsterHp;
        this.maxHp                      = data.MonsterMaxHp;
        this.attackDamage               = data.AttackDamage;
        this.moveSpeed                  = data.MoveSpeed;
        this.sightRangeX                = data.SightRangeX;
        this.sightRangeY                = data.SightRangeY;
        this.attackRange                = data.AttackRange;
        this.hasAdditionalAttack        = data.HasAdditionalAttack;
    }

    // 공격하는 함수, 각 몬스터의 공격방식이 다르기 때문에 override 시킴
    public virtual void AttackA()
    {
        
    }

    public virtual void AttackB()
    {
        
    }

}