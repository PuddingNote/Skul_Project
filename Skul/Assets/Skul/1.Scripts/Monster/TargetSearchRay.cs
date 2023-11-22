using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearchRay : MonoBehaviour
{
    MonsterController monsterController;

    [HideInInspector] public Collider2D hit;

    void Start()
    {
        monsterController = gameObject.GetComponent<MonsterController>();
    }

    void Update()
    {
        TagetCheckRay();
    }

    // �ֺ��� �÷��̾ Ȯ���ϴ� �Լ�
    public void TagetCheckRay()
    {
        hit = Physics2D.OverlapBox(monsterController.monster.transform.position,
        new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
    }

    // ����׿� : �÷��̾� Ȯ�� ���� ǥ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (monsterController != null)
        {
            Gizmos.DrawWireCube(monsterController.monster.transform.position, 
                new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY));
        }
    }

}