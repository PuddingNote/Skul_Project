using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MageSkul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;

    private SpriteRenderer spriteRenderer;       // ���� ������ ���� �ӽ� �߰�

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

    //// AttackA, B�� ��Ʈ ���� ó���ϴ� �Լ�
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
    //private SpriteRenderer spriteRenderer;       // ���� ������ ���� �ӽ� �߰�

    //void OnEnable()
    //{
    //    // Action�� SkillA�� �Ἥ ��Ÿ����Ʈ�ѷ��� SkulHeadless�� �ٲ�� �ذ��� ���� ������ ��� ó���ϱ� ���� ���� ����
    //    onHeadBack += OnHeadBackHandler;

    //    // SkillA�� ���� ��� �ڽ��� �ذ��� ���� SkulHeadless�� ��Ÿ�Ӿִ���Ʈ�ѷ��� �����ϱ� ���� �ʱ�ȭ
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

    //// AttackA, B�� ��Ʈ ���� ó���ϴ� �Լ�
    //private void AttackAandB()
    //{
    //    Vector2 attackArea = new Vector2(1.5f, 1.5f);

    //    // BoxcastAll�� Hitó��
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