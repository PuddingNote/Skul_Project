using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : IMonsterState
{
    private MonsterController mController;
    private GameObject deadEffect;

    public void StateEnter(MonsterController _mController)
    {
        this.mController = _mController;
        mController.enumState = MonsterController.MonsterState.DEAD;

        // �������Ʈ Ȱ��ȭ
        deadEffect = GameObject.Instantiate(Resources.Load("0.Prefabs/DeadEffect")) as GameObject;
        deadEffect.transform.position = mController.monster.transform.position;
        deadEffect.SetActive(true);
        mController.monster.gameObject.SetActive(false);

        // UI�� ǥ�õ� ������ ���� ųī��Ʈ üũ
        GameManager.Instance.monsterRemainingSum -= 1;
        GameManager.Instance.killCount += 1;
    }

    public void StateUpdate()
    {

    }

    public void StateExit()
    {

    }
}
