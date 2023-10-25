using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulSkillA : MonoBehaviour
{
    public Skul parentObj;
    private float speed = 7f;
    private float range = 15f;
    public float direction;
    private float originalGravity;
    private bool isHit = false;
    private Vector3 startVector;
    private Rigidbody2D skillA_Rb;
    private Animator skillA_Ani;

    void Start()
    {
        skillA_Rb = gameObject.GetComponent<Rigidbody2D>();
        skillA_Ani = gameObject.GetComponent<Animator>();

        // �߷����� ��Ʈ�ϰų� ��Ÿ����� �̵������� �߷������� ����
        originalGravity = skillA_Rb.gravityScale;
        skillA_Rb.gravityScale = 0f;
    }

    void Update()
    {
        OverRange();
        DetectTaget();
        gameObject.transform.Translate(new Vector2(direction, 0).normalized * speed * Time.deltaTime);
    }

    // ���ư��� ���� Ÿ���� �����ϸ� Hitó�� �ϴ� �Լ�
    private void DetectTaget()
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

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.zero, 0f, LayerMask.GetMask(tagetObj));
        if (hit.collider != null)
        {
            if (tagetObj == GData.ENEMY_LAYER_MASK)
            {
                MonsterController target = hit.collider.gameObject?.GetComponent<MonsterController>();
                target.monster.hp -= Random.Range(20, 25);
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
    private IEnumerator DestroySkul()
    {
        if (gameObject == null)
        {
            yield break;
        }
        speed = 0;
        skillA_Rb.gravityScale = originalGravity;
        skillA_Ani.StartPlayback();
        yield return new WaitForSeconds(4f);
        parentObj.onHeadBack?.Invoke();
        Destroy(gameObject);
        Debug.Log("�ذ� �ı���");
    }

}