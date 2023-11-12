using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;

    void OnEnable()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerData = Resources.Load("3.Scriptable Object/MageSkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        Debug.Log("MageSkul");
    }

    public override void AttackA()
    {
        AttackAandB();
    }

    public override void AttackB()
    {
        AttackAandB();
    }

    // 공격A, B의 히트 판정 처리하는 함수
    private void AttackAandB()
    {

    }

    public override void SkillA()
    {

    }

    public override void SkillB()
    {

    }

}