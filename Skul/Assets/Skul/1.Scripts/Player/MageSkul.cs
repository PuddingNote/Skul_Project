using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MageSkul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;

    private SpriteRenderer spriteRenderer;       // 색상값 조정을 위해 임시 추가

    void OnEnable()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerData = Resources.Load("3.Scriptable Object/MageSkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.red;

        Debug.Log("MageSkul");
    }

    //public override void AttackA()
    //{
    //    AttackAandB();
    //}

    //public override void AttackB()
    //{
    //    AttackAandB();
    //}

    //// AttackA, B의 히트 판정 처리하는 함수
    //private void AttackAandB()
    //{
    //    Debug.Log("Attack");
    //}

    //public override void SkillA()
    //{
    //    Debug.Log("SkillA");
    //}

    //public override void SkillB()
    //{
    //    Debug.Log("SkillB");
    //}

    //public PlayerData playerData;
    //private PlayerController playerController;
    //private RuntimeAnimatorController SkulHeadless;
    //public Action onHeadBack;
    //private GameObject skillAObj;
    //private SpriteRenderer spriteRenderer;       // 색상값 조정을 위해 임시 추가

    //void OnEnable()
    //{
    //    // Action에 SkillA를 써서 런타임컨트롤러가 SkulHeadless로 바뀌고 해골을 줍지 못했을 경우 처리하기 위한 내용 저장
    //    onHeadBack += OnHeadBackHandler;

    //    // SkillA를 썼을 경우 자신의 해골을 날려 SkulHeadless로 런타임애니컨트롤러를 변경하기 위한 초기화
    //    SkulHeadless = Resources.Load("2.Animations/PlayerAni/SkulHeadless/SkulHeadless") as RuntimeAnimatorController;
    //    playerController = gameObject.GetComponent<PlayerController>();
    //    playerData = Resources.Load("3.Scriptable Object/MageSkulData") as PlayerData;
    //    InitPlayerData(playerData);
    //    playerController.player = (Player)(this as Player);

    //    spriteRenderer = GetComponent<SpriteRenderer>();
    //    spriteRenderer.color = Color.red;

    //    Debug.Log("MageSkul");
    //}

    //void OnHeadBackHandler()
    //{
    //    playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
    //}

    //public override void AttackA()
    //{
    //    AttackAandB();
    //}

    //public override void AttackB()
    //{
    //    AttackAandB();
    //}

    //// AttackA, B의 히트 판정 처리하는 함수
    //private void AttackAandB()
    //{
    //    Vector2 attackArea = new Vector2(1.5f, 1.5f);

    //    // BoxcastAll로 Hit처리
    //    RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
    //    foreach (var hit in hits)
    //    {
    //        if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
    //        {
    //            Monster monster = hit.collider.gameObject.GetComponent<Monster>();
    //            monster.hp -= playerData.AttackDamage;
    //            Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
    //        }
    //    }
    //}

    //public override void SkillA()
    //{
    //    Debug.Log("use SkillA");
    //}

    //public override void SkillB()
    //{
    //    if (skillAObj != null)
    //    {
    //        Debug.Log("use SkillB");

    //        playerController.player.transform.position = skillAObj.transform.position;
    //        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
    //        Destroy(skillAObj);
    //    }

    //}

}