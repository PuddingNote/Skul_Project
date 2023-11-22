using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    private RuntimeAnimatorController SkulHeadless;
    public Action onHeadBack;
    private GameObject skillAObj;

    private SpriteRenderer spriteRenderer;       // ���� ������ ���� �ӽ� �߰�

    void OnEnable()
    {
        // Action�� SkillA�� �Ἥ ��Ÿ����Ʈ�ѷ��� SkulHeadless�� �ٲ�� �ذ��� ���� ������ ��� ó���ϱ� ���� ���� ����
        onHeadBack += () =>
        {
            // ���� �÷��̾ Skul�� �ƴϸ� ����
            if (playerController.player._name != "SkulData")
            {
                return;
            }
            playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
        };

        // SkillA�� ���� ��� �ڽ��� �ذ��� ���� SkulHeadless�� ��Ÿ�Ӿִ���Ʈ�ѷ��� �����ϱ� ���� �ʱ�ȭ
        SkulHeadless = Resources.Load("2.Animations/PlayerAni/SkulHeadless/SkulHeadless") as RuntimeAnimatorController;
        playerController = gameObject.GetComponent<PlayerController>();
        playerData = Resources.Load("3.Scriptable Object/SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;

        Debug.Log("Skul");
    }

    public void SkulAttackA()
    {
        AttackAandB();
    }

    public void SkulAttackB()
    {
        AttackAandB();
    }

    public void SkulJumpAttack()
    {
        AttackAandB();
    }

    // AttackA, B�� ��Ʈ ���� ó���ϴ� �Լ�
    private void AttackAandB()
    {
        Vector2 attackArea = new Vector2(1.5f, 1.5f);

        // BoxcastAll�� Hitó��
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                int damage = playerData.AttackDamage;
                Monster monster = hit.collider.gameObject.GetComponent<Monster>();
                if (monster != null)
                {
                    monster.hp -= damage;
                    GameManager.Instance.totalDamage += damage;
                    Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
                }
            }
            GameObject hitEffect = Instantiate(Resources.Load("0.Prefabs/HitEffect") as GameObject);
            hitEffect.transform.position = hit.transform.position - new Vector3(0f, 0.5f, 0f);
            hitEffect.transform.localScale = new Vector2(playerController.player.transform.localScale.x, hitEffect.transform.localScale.y);
        }
    }

    public void SkulSkillA()
    {
        skillAObj = Instantiate(Resources.Load("0.Prefabs/SkulSkillAEffect") as GameObject);
        skillAObj.GetComponent<SkulSkillA>().Init(this);
        playerAni.runtimeAnimatorController = SkulHeadless;

        // �Է¹ް��ִ� �ִϸ��̼� bool���� ����� ��帮�� ��Ÿ����Ʈ�ѷ��� �ٲ����ʾ� ������ ���¸� �ʱ�ȭ����
        // ��帮�� ��Ÿ����Ʈ�ѷ��� �����Է��� ���� �� �ְ� ó����
        IPlayerState nextState = new PlayerIdle();
        playerController.pStateMachine.onChangeState.Invoke(nextState);
    }

    public void SkulSkillB()
    {
        // ���������ġ�� �����̵�, ��帮������ ���
        playerController.player.transform.position = skillAObj.transform.position;
        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;

        // �Է¹ް��ִ� �ִϸ��̼� bool���� ����� ���� ��Ÿ����Ʈ�ѷ��� �ٲ����ʾ� ������ ���¸� �ʱ�ȭ����
        // ���� ��Ÿ����Ʈ�ѷ��� �����Է��� ���� �� �ְ� ó����
        IPlayerState nextState = new PlayerIdle();
        playerController.pStateMachine.onChangeState.Invoke(nextState);

        // ������尡 �����ϸ� �ı�
        if (skillAObj != null)
        {
            Destroy(skillAObj);
        }
    }

}
