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

    // ����A, B�� ��Ʈ ���� ó���ϴ� �Լ�
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