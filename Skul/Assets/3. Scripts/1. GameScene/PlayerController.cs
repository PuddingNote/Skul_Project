using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D playerRig = default;
    //private AudioSource playerAudio = default;
    //private Animator playerAni = default;
    //#region //대쉬변수
    //private bool isGround = true;

    //private float dashForce = 10f;
    //private bool canDash = true;
    //private bool isDash = false;
    //private int dashCount = default;
    //private float dashTime = 0.3f;
    //private float dashCooldown = 1f;
    //#endregion //대쉬변수
    //private int jumpCount = 0;
    //private float jumpForce = 8f;
    //private bool isRight = true;
    //private float horizontal = default;
    //private float speed = 5f;
    //private float hp = default;
    //private float maxHp = 100;
    //private bool isWalk = false;
    //private bool isJump = false;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    playerRig = gameObject.GetComponentMust<Rigidbody2D>();
    //    playerAudio = gameObject.GetComponentMust<AudioSource>();
    //    playerAni = gameObject.GetComponentMust<Animator>();
    //    playerRig.gravityScale = 1.5f;
    //    hp = maxHp;
    //} //Start

    //// Update is called once per frame
    //void Update()
    //{
    //    MoveControl();
    //    TurnPlayer();
    //} //Update

    //private void FixedUpdate()
    //{
    //    //{대쉬 처리
    //    if (isDash)
    //    {
    //        return;
    //    }
    //    playerRig.velocity = new Vector2(horizontal * speed, playerRig.velocity.y);
    //    //}대쉬 처리
    //} //FixedUpdate
    //private void MoveControl()
    //{
    //    if (isDash)
    //    {
    //        return;
    //    }

    //    //애니메이터 연결을 위한 bool값 처리
    //    if (Input.GetAxisRaw("Horizontal") == default)
    //    {
    //        isWalk = false;
    //    }
    //    else
    //    {
    //        isWalk = true;
    //    }
    //    //좌우 방향키 입력
    //    horizontal = Input.GetAxisRaw("Horizontal");
    //    playerAni.SetBool("isWalk", isWalk);

    //    //점프키 입력
    //    if (Input.GetKeyDown(KeyCode.C) && jumpCount < 2)
    //    {
    //        isJump = true;
    //        jumpCount++;
    //        playerRig.velocity = transform.up * jumpForce;
    //    }
    //    playerAni.SetBool("isJump", isJump);

    //    //대쉬키 입력
    //    if (Input.GetKeyDown(KeyCode.Z) && canDash)
    //    {
    //        StartCoroutine(Dash());
    //    }

    //    //공격키 입력
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {

    //    }
    //} //MoveControl

    ////플레이어 바라보는 방향 전환하는 함수
    //private void TurnPlayer()
    //{
    //    if (isRight && horizontal < 0f || !isRight && horizontal > 0f)
    //    {
    //        Vector3 localScale = transform.localScale;
    //        isRight = !isRight;
    //        localScale.x *= -1f;
    //        transform.localScale = localScale;
    //    }
    //} //TurnPlayer

    ////대쉬하는 코루틴
    //private IEnumerator Dash()
    //{
    //    canDash = false;
    //    isDash = true;
    //    playerAni.SetBool("isDash", isDash);
    //    dashCount += 1;
    //    float originalGravity = playerRig.gravityScale;
    //    playerRig.gravityScale = 0f;
    //    playerRig.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
    //    yield return new WaitForSeconds(dashTime);
    //    playerRig.gravityScale = originalGravity;
    //    isDash = false;
    //    playerAni.SetBool("isDash", isDash);
    //    //2단 대쉬
    //    if (dashCount >= 2)
    //    {
    //        yield return new WaitForSeconds(dashCooldown);
    //        dashCount = 0;
    //        canDash = true;
    //        yield break;
    //    }
    //    canDash = true;
    //} //Dash

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //} //OnTriggerEnter2D
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //        isJump = false;
    //        jumpCount = 0;
    //    }
    //} //OnCollisionEnter2D

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Ground")
    //    {
    //    }
    //} //OnCollisionExit2D

}