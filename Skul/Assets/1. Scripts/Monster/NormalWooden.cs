using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWooden : Monster
{
    private MonsterController monsterController;
    public MonsterData monsterData;
    private Animator normalWoodenAni;

    private RaycastHit2D hit;
    private float direction;            // ����
    private Vector3 attackdirection;    // ���ݹ���

    void Awake()
    {
        monsterController = gameObject.GetComponentMust<MonsterController>();   
        monsterData = Resources.Load("NormalWooden") as MonsterData;    // NormalWooden ���ҽ��� �ε��ؼ� MonsterData�� ��ȯ
        InitMonsterData(monsterData);                                   // ���� ������ �ʱ�ȭ

        // ������Ʈ�ѷ����� ����Ÿ�� ������ ���ٰ����ϰ� �ϱ� ���� ó��
        // ������Ʈ�ѷ��� ����Ÿ�� ������ �ڽ��� ���ͷ� ĳ�����Ͽ� ����
        monsterController.monster = (Monster)(this as Monster);
        normalWoodenAni = gameObject.GetComponent<Animator>();
    }

    // �����ϴ� �Լ�(������ �ִϸ��̼� �̺�Ʈ�� ó����)
    public override void AttackA()
    {
        Vector2 attackArea = new Vector2(0.5f, 1.5f);               // ���� ����
        direction = transform.localScale.x;                         // ���� ���� Ȯ��
        attackdirection = new Vector3(direction, 0f).normalized;    // ���� ���� ����

        // Boxcast�� �ǰ�ó��
        hit = Physics2D.BoxCast(transform.position, attackArea, 0f, attackdirection, 1f, LayerMask.GetMask(GData.PLAYER_LAYER_MASK));
        if (hit.collider != null)
        {
            // �÷��̾� ü�� ����
            PlayerController target = hit.collider.gameObject.GetComponentMust<PlayerController>();
            int minDamage = monsterController.monster.minDamage;
            int maxDamage = monsterController.monster.maxDamage;
            target.hp -= Random.Range(minDamage, maxDamage);
            Debug.Log($"�븻��� ����! �÷��̾� hp = {target.hp}");
        }
    }

    // ����׿� : ���� ���� ǥ��
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (attackdirection * 1f), new Vector2(0.5f, 1.5f));
    }

    // ���ݾִϸ��̼��� ����Ǹ� �ڷ�ƾ ����
    // ���� ���� ���� �� ��� ���·� ��ȯ�ϴ� �Լ�
    public void ExitAttack()
    {
        StartCoroutine(AttackDelay());
    }

    // ���ݵ����� ���ϴ� �ڷ�ƾ �Լ�
    private IEnumerator AttackDelay()
    {
        // ���ݵ����� �߿��� idle��� ó��
        normalWoodenAni.SetBool("isAttackA", false);
        normalWoodenAni.SetBool("isIdle", true);
        yield return new WaitForSeconds(2f);

        normalWoodenAni.SetBool("isIdle", false);

        // 2���� ���� ���°� ������ �ƴ϶�� �ڷ�ƾ ���� => �ڷ�ƾ������ ���°� ������ ��� �ؿ� ���ݸ���� ����ϱ� ���� ����ó��
        if (monsterController.enumState != MonsterController.MonsterState.ATTACK)
        {
            Debug.Log($"2���� ����{monsterController.enumState}");
            yield break;
        }
        normalWoodenAni.SetBool("isAttackA", true);
    }

}