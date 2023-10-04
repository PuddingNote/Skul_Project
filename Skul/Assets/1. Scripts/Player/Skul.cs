using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    private Animator skulAni;

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerData = Resources.Load("SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        skulAni = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public override void AttackA()
    {
        Debug.Log("���ð���A ����?");
        Vector2 attackArea = new Vector2(1.5f, 1.5f);

        // BoxcastAll�� Hitó��
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, attackArea, 0f, Vector2.zero, 0f, LayerMask.GetMask(GData.ENEMY_LAYER_MASK));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == GData.ENEMY_LAYER_MASK)
            {
                Monster monster = hit.collider.gameObject.GetComponent<Monster>();
                monster.hp -= playerData.AttackDamage;
                Debug.Log($"{hit.collider.name}={monster.hp}/{monster.maxHp}");
            }
        }
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