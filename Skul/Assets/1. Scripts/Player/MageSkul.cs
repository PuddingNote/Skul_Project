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
        playerData = Resources.Load("MageSkul") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        Debug.Log("MageSkul");
    }

    void Start()
    {

    }

    public override void AttackA()
    {

    }

    public override void AttackB()
    {

    }

    public override void SkillA()
    {

    }

    public override void SkillB()
    {

    }

}