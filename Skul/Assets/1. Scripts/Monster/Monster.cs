using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D monsterRb;
    [HideInInspector] public AudioSource monsterAudio;
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
        this.monsterRb = gameObject.GetComponentMust<Rigidbody2D>();
        this.monsterAudio = gameObject.GetComponentMust<AudioSource>();
        this.monsterAni = gameObject.GetComponentMust<Animator>();
        this.groundCheckRay = gameObject.GetComponentMust<GroundCheckRay>();
        this.tagetSearchRay = gameObject.GetComponentMust<TargetSearchRay>();
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

    // �����ϴ� �Լ�, �� ������ ���ݹ���� �ٸ��� ������ override ��Ŵ
    public virtual void AttackA()
    {
        
    }

    public virtual void AttackB()
    {
        
    }

}