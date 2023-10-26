using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skul : Player
{
    public PlayerData playerData;
    private PlayerController playerController;
    private RuntimeAnimatorController SkulHeadless;
    public System.Action onHeadBack;
    private GameObject skillAObj;

    void Start()
    {
        //playerController = gameObject.GetComponent<PlayerController>();
        //playerData = Resources.Load("SkulData") as PlayerData;
        //InitPlayerData(playerData);
        //playerController.player = (Player)(this as Player);

        // Action�� SkillA�� �Ἥ ��Ÿ����Ʈ�ѷ��� SkulHeadless�� �ٲ�� �ذ��� ���� ������ ��� ó���ϱ� ���� ���� ����
        //onHeadBack += () => { playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC; };

        // SkillA�� ���� ��� �ڽ��� �ذ��� ���� SkulHeadless�� ��Ÿ�Ӿִ���Ʈ�ѷ��� �����ϱ� ���� �ʱ�ȭ
        //SkulHeadless = Resources.Load("2. Animations/PlayerAni/SkulHeadless/SkulHeadless") as RuntimeAnimatorController;
        playerController = gameObject.GetComponent<PlayerController>();
        playerData = Resources.Load("SkulData") as PlayerData;
        InitPlayerData(playerData);
        playerController.player = (Player)(this as Player);
        Debug.Log("Skul");
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
        Debug.Log("use SkillA?");
        playerAni.SetBool("isSkillA", true);
        skillAObj = Instantiate(Resources.Load("0. Prefabs/SkulSkillAEffect") as GameObject);
        skillAObj.GetComponent<SkulSkillA>().Init(this);
        playerAni.runtimeAnimatorController = SkulHeadless;
    }

    public override void SkillB()
    {
        Debug.Log("use SkillB?");
        playerController.player.transform.position = skillAObj.transform.position;
        playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
        if (skillAObj != null)
        {
            Destroy(skillAObj);
        }
    }

}
