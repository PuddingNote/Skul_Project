using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkulSkillA : MonoBehaviour
{
    private Rigidbody2D skillA_Rb;
    private Animator skillA_Ani;
    private Skul parentObj;             // 부모오브젝트를 찾기위한 변수

    private Vector3 startVector;        // 스킬 시작위치
    private float speed = 7f;           // 스킬 속도
    private float range = 15f;          // 스킬 사거리
    private float direction;            // 스킬 이동 방향
    private float originalGravity;      // 날아가는도중 중력영향을 안받게하기위한 변수
    private bool isHit = false;         // 스킬이 Hit했는지

    void Awake()
    {
        skillA_Rb = gameObject.GetComponent<Rigidbody2D>();
        skillA_Ani = gameObject.GetComponent<Animator>();

        originalGravity = skillA_Rb.gravityScale;   // 원래 중력값 저장
        skillA_Rb.gravityScale = 0f;                // 중력 제거
    }

    void Update()
    {
        OverRange();
        DetectTarget();
        gameObject.transform.Translate(new Vector2(direction, 0).normalized * speed * Time.deltaTime);
    }

    // 날아가는 도중 타겟을 감지하면 Hit처리 하는 함수
    private void DetectTarget()
    {
        // Hit상태가되면 LayerMask대상을 플레이어로 전환해 플레이어가 스컬헤드를 습득할 수 있도록 처리함
        string tagetObj;

        if (isHit == true)
        {
            tagetObj = GData.PLAYER_LAYER_MASK;
        }
        else
        {
            tagetObj = GData.ENEMY_LAYER_MASK;
        }

        // 해골이 땅에 닿았을경우
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
                int skillDamage = 20;
                MonsterController target = hit.collider.gameObject?.GetComponent<MonsterController>();
                if (target != null)
                {
                    target.monster.hp -= skillDamage;
                    isHit = true;
                    GameManager.Instance.totalDamage += skillDamage;
                    float direction = hit.collider.transform.position.x - transform.position.x > 0 ? -1.5f : 1.5f;
                    skillA_Rb.AddForce(new Vector2(direction, 3f), ForceMode2D.Impulse);
                    Debug.Log($"스킬A공격 = {target.monster.hp}/{target.monster.maxHp}");
                }
                GameObject hitEffect = Instantiate(Resources.Load("0.Prefabs/HitEffect") as GameObject);
                hitEffect.transform.position = hit.collider.transform.position;
            }
            if (tagetObj == GData.PLAYER_LAYER_MASK)
            {
                PlayerController playerController = hit.collider.gameObject?.GetComponent<PlayerController>();
                if (playerController.player._name == "Skul")
                {
                    playerController.player.playerAni.runtimeAnimatorController = playerController.BeforeChangeRuntimeC;

                    // 해골습득시 런타임애니메이션컨트롤러가 변경되므로 상태 초기화
                    IPlayerState nextState = new PlayerIdle();
                    playerController.pStateMachine.onChangeState?.Invoke(nextState);

                    // 머리 습득시 SkillA 쿨 초기화
                    playerController.isGetSkulSkillA = true;
                }
                Destroy(gameObject);
                return;
            }
            StartCoroutine(DestroySkul());
        }
    }

    // 사거리를 벗어나면 멈추고 중력영향 받게하는 함수
    private void OverRange()
    {
        // 스킬 시작위치, 현재위치 거리 계산
        float distance = Vector3.Distance(startVector, transform.position);
        if (distance >= range)
        {
            isHit = true;
            StartCoroutine(DestroySkul());
        }
    }

    // 자신의 부모를 입력받는 함수 (Skul)
    public void Init(Skul parent)
    {
        parentObj = parent;
        startVector = parentObj.gameObject.GetComponent<PlayerController>().transform.position;
        gameObject.transform.position = startVector;
        direction = parentObj.gameObject.GetComponent<PlayerController>().transform.localScale.x;
    }

    // Hit되거나 최대 사거리에 도달했을 경우 4초뒤에 파괴하는 코루틴 함수
    IEnumerator DestroySkul()
    {
        if (gameObject == null)
        {
            yield break;
        }
        speed = 0;
        skillA_Rb.gravityScale = originalGravity;   // 중력값 복구
        skillA_Ani.StartPlayback();                 // 애니메이션 역재생
        yield return new WaitForSeconds(4f);

        parentObj.onHeadBack?.Invoke();

        Destroy(gameObject);
        Debug.Log("해골 파괴");
    }

}