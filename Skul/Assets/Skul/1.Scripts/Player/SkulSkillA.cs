using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulSkillA : MonoBehaviour
{
    private Rigidbody2D skillA_Rb;
    private Animator skillA_Ani;
    private Skul parentObj;

    private Vector3 startVector;        // ��ų ������ġ
    private float speed = 7f;           // ��ų �ӵ�
    private float range = 15f;          // ��ų ��Ÿ�
    private float direction;             // ��ų �̵� ����
    private float originalGravity;      // ���ư��µ��� �߷¿����� �ȹް��ϱ����� ����
    private bool isHit = false;         // ��ų�� Hit�ߴ���

    void Awake()
    {
        skillA_Rb = gameObject.GetComponent<Rigidbody2D>();
        skillA_Ani = gameObject.GetComponent<Animator>();

        originalGravity = skillA_Rb.gravityScale;   // ���� �߷°� ����
        skillA_Rb.gravityScale = 0f;                // �߷� ����
    }

    void Update()
    {
        OverRange();
        DetectTarget();
        gameObject.transform.Translate(new Vector2(direction, 0).normalized * speed * Time.deltaTime);
    }

    // ���ư��� ���� Ÿ���� �����ϸ� Hitó�� �ϴ� �Լ�
    private void DetectTarget()
    {
        // Hit���°��Ǹ� CircleCast �ߵ� ���ϰ� ����
        string tagetObj;

        if (isHit == true)
        {
            tagetObj = GData.PLAYER_LAYER_MASK;
        }
        else
        {
            tagetObj = GData.ENEMY_LAYER_MASK;
        }

        // �ذ��� ���� ��������
        RaycastHit2D hitGround = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
        if (hitGround.collider != null)
        {
            isHit = true;
            StartCoroutine(DestroySkul());
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(tagetObj));
        if (hit.collider != null)
        {
            if (tagetObj == GData.ENEMY_LAYER_MASK)
            {
                MonsterController target = hit.collider.gameObject?.GetComponent<MonsterController>();
                target.monster.hp -= 20;
                Debug.Log($"��ųA���� = {target.monster.hp}/{target.monster.maxHp}");
                isHit = true;
            }
            if (tagetObj == GData.PLAYER_LAYER_MASK)
            {
                PlayerController playerController = hit.collider.gameObject?.GetComponent<PlayerController>();
                playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;
                Destroy(gameObject);
                return;
            }
            StartCoroutine(DestroySkul());
        }
    }

    // ��Ÿ��� ����� ���߰� �߷¿��� �ް��ϴ� �Լ�
    private void OverRange()
    {
        // ��ų ������ġ, ������ġ �Ÿ� ���
        float distance = Vector3.Distance(startVector, transform.position);
        if (distance >= range)
        {
            isHit = true;
            StartCoroutine(DestroySkul());
        }
    }

    // �ڽ��� �θ� �Է¹޴� �Լ�
    public void Init(Skul parent)
    {
        parentObj = parent;
        startVector = parentObj.gameObject.GetComponent<PlayerController>().transform.position;
        gameObject.transform.position = startVector;
        direction = parentObj.gameObject.GetComponent<PlayerController>().transform.localScale.x;
    }

    // Hit�ǰų� �ִ� ��Ÿ��� �������� ��� 4�ʵڿ� �ı��ϴ� �ڷ�ƾ �Լ�
    IEnumerator DestroySkul()
    {
        if (gameObject == null)
        {
            yield break;
        }
        speed = 0;
        skillA_Rb.gravityScale = originalGravity;   // �߷°� ����
        skillA_Ani.StartPlayback();                 // �ִϸ��̼� �����
        yield return new WaitForSeconds(4f);
        parentObj.onHeadBack?.Invoke();
        Destroy(gameObject);
        Debug.Log("�ذ� �ı�");
    }

}