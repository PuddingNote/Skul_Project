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

    private SpriteRenderer spriteRenderer;       // 색상값 조정을 위해 임시 추가

    void OnEnable()
    {
        // Action에 SkillA를 써서 런타임컨트롤러가 SkulHeadless로 바뀌고 해골을 줍지 못했을 경우 처리하기 위한 내용 저장
        onHeadBack += () =>
        {
            // 현재 플레이어가 Skul이 아니면 리턴
            if (playerController.player._name != "SkulData")
            {
                return;
            }
            playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
        };

        // SkillA를 썼을 경우 자신의 해골을 날려 SkulHeadless로 런타임애니컨트롤러를 변경하기 위한 초기화
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

    // AttackA, B의 히트 판정 처리하는 함수
    private void AttackAandB()
    {
        Vector2 attackArea = new Vector2(1.5f, 1.5f);

        // BoxcastAll로 Hit처리
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

        // 입력받고있던 애니메이션 bool값이 변경된 헤드리스 런타임컨트롤러로 바뀌지않아 강제로 상태를 초기화시켜
        // 헤드리스 런타임컨트롤러가 유저입력을 받을 수 있게 처리함
        IPlayerState nextState = new PlayerIdle();
        playerController.pStateMachine.onChangeState.Invoke(nextState);
    }

    public void SkulSkillB()
    {
        // 스컬헤드위치로 순간이동, 헤드리스상태 벗어남
        playerController.player.transform.position = skillAObj.transform.position;
        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;

        // 입력받고있던 애니메이션 bool값이 변경된 스컬 런타임컨트롤러로 바뀌지않아 강제로 상태를 초기화시켜
        // 스컬 런타임컨트롤러가 유저입력을 받을 수 있게 처리함
        IPlayerState nextState = new PlayerIdle();
        playerController.pStateMachine.onChangeState.Invoke(nextState);

        // 스컬헤드가 존재하면 파괴
        if (skillAObj != null)
        {
            Destroy(skillAObj);
        }
    }

}
