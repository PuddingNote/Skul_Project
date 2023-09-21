using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearchRay : MonoBehaviour
{
    MonsterController monsterController;
    [HideInInspector]
    public Collider2D hit;

    void Start()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();
    }

    void Update()
    {
        TagetCheckRay();
    }

    // 주변에 플레이어를 확인하는 함수
    public void TagetCheckRay()
    {
        hit = Physics2D.OverlapBox(monsterController.monster.transform.position,
        new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY), 0, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
    }

    // 디버그용 : 플레이어 확인 범위를 시각화
    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    if (monsterController != null)
    //    {
    //        Gizmos.DrawWireCube(monsterController.monster.transform.position,
    //        new Vector2(monsterController.monster.sightRangeX, monsterController.monster.sightRangeY));
    //    }
    //}

}