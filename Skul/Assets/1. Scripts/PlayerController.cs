using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본적인 플레이어컨트롤러 스크립트
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private Animator playerAni;

    // 대쉬 관련
    //private bool isGround = true;       // 바닥 판별
    private float dashForce = 10f;      // 대쉬 거리
    private bool canDash = true;        // 대쉬 가능 판별
    private bool isDash = false;        // 대쉬중
    private int dashCount = default;    // 대쉬 카운트
    private float dashTime = 0.3f;      // 대쉬 시간
    private float dashCooldown = 1f;    // 대쉬 쿨타임

    // 플레이어 관련
    private float speed = 5f;           // 속도
    //private float hp = default;         // hp
    //private float maxHp = 100;          // 최대 hp

    private bool isRight = true;        // 좌우 판별
    private float horizontal = default; // horizontal

    private bool isWalk = false;        // isWalk 판별
    private bool isJump = false;        // isJump 판별

    // 낙하 관련
    private float posY = default;       // y축
    private bool isFall = false;        // 낙하 판별
    private uint fallCount = default;   // 낙하 카운트
    private int jumpCount = 0;          // 점프 횟수
    private float jumpForce = 8f;       // 점프 힘

    private void Awake()
    {
        playerRig = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();
        playerRig.gravityScale = 1f;
        //hp = maxHp;
    }

    private void Update()
    {
        MoveControl();
        TurnPlayer();
    }

    private void FixedUpdate()
    {
        FallControl();
        
        // 대쉬 처리
        if (isDash)
        {
            return;
        }
        playerRig.velocity = new Vector2(horizontal * speed, playerRig.velocity.y);
    }

    // 움직임 관련 함수
    private void MoveControl()
    {
        // 대쉬중엔 이동제어 X
        if (isDash)
        {
            return;
        }

        // 애니메이터 연결을 위한 bool값 처리
        if (Input.GetAxisRaw("Horizontal") == default)
        {
            isWalk = false;
        }
        else
        {
            isWalk = true;
        }

        // 좌우 방향키 입력
        horizontal = Input.GetAxisRaw("Horizontal");
        playerAni.SetBool("isWalk", isWalk);

        // 점프키 입력 && 2번 점프
        if (Input.GetKeyDown(KeyCode.X) && jumpCount < 2)
        {
            isJump = true;
            jumpCount++;
            playerRig.velocity = transform.up * jumpForce;
        }
        playerAni.SetBool("isJump", isJump);

        // 대쉬키 입력
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine(Dash());
        }

        // 공격키 입력
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAni.SetTrigger("isAttack");
        }
    }

    // 플레이어 방향 전환 함수
    private void TurnPlayer()
    {
        if (isRight && horizontal < 0f || !isRight && horizontal > 0f)  // 반대키 입력 감지
        {
            Vector3 localScale = transform.localScale;
            isRight = !isRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // 낙하 관련 함수
    private void FallControl()
    {
        // 낙하 모션 체크
        if (fallCount == 0)
        {
            posY = transform.position.y;
        }

        fallCount++;
        if (fallCount >= 5)
        {
            fallCount = 0;
            if (posY - transform.position.y >= 0.05f)
            {
                isFall = true;
            }
        }
        playerAni.SetBool("isFall", isFall);
    }

    // 대쉬하는 코루틴
    private IEnumerator Dash()
    {
        // 대쉬 실행 여부 체크
        canDash = false;   

        // 대쉬 중 애니메이션 설정
        isDash = true;
        playerAni.SetBool("isDash", isDash);

        dashCount += 1;

        // 원래 중력값 저장 후 대쉬중에 무중력상태로 변환해서 플레이어가 떨어지지않게 설정 후 원상 복구
        float originalGravity = playerRig.gravityScale;
        playerRig.gravityScale = 0f;
        playerRig.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime); // dashTime만큼 대기
        playerRig.gravityScale = originalGravity;

        // 대쉬 종료 애니메이션 설정
        isDash = false;
        playerAni.SetBool("isDash", isDash);

        // 2단 대쉬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown); // dashCooldown만큼 대기

            // 대쉬 횟수, 가능 여부 초기화
            dashCount = 0;
            canDash = true;

            // 코루틴 종료
            yield break;
        }
        canDash = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            // 낙하 관련 초기화
            fallCount = 0;
            isFall = false;

            // 점프 관련 초기화
            jumpCount = 0;
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

        }
    }

}