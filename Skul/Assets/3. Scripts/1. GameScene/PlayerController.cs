using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody2D playerRig = default;
    //private AudioSource playerAudio = default;
    //private Animator playerAni = default;
    //#region //�뽬����
    //private bool isGround = true;

    //private float dashForce = 10f;
    //private bool canDash = true;
    //private bool isDash = false;
    //private int dashCount = default;
    //private float dashTime = 0.3f;
    //private float dashCooldown = 1f;
    //#endregion //�뽬����
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
    //    //{�뽬 ó��
    //    if (isDash)
    //    {
    //        return;
    //    }
    //    playerRig.velocity = new Vector2(horizontal * speed, playerRig.velocity.y);
    //    //}�뽬 ó��
    //} //FixedUpdate
    //private void MoveControl()
    //{
    //    if (isDash)
    //    {
    //        return;
    //    }

    //    //�ִϸ����� ������ ���� bool�� ó��
    //    if (Input.GetAxisRaw("Horizontal") == default)
    //    {
    //        isWalk = false;
    //    }
    //    else
    //    {
    //        isWalk = true;
    //    }
    //    //�¿� ����Ű �Է�
    //    horizontal = Input.GetAxisRaw("Horizontal");
    //    playerAni.SetBool("isWalk", isWalk);

    //    //����Ű �Է�
    //    if (Input.GetKeyDown(KeyCode.C) && jumpCount < 2)
    //    {
    //        isJump = true;
    //        jumpCount++;
    //        playerRig.velocity = transform.up * jumpForce;
    //    }
    //    playerAni.SetBool("isJump", isJump);

    //    //�뽬Ű �Է�
    //    if (Input.GetKeyDown(KeyCode.Z) && canDash)
    //    {
    //        StartCoroutine(Dash());
    //    }

    //    //����Ű �Է�
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {

    //    }
    //} //MoveControl

    ////�÷��̾� �ٶ󺸴� ���� ��ȯ�ϴ� �Լ�
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

    ////�뽬�ϴ� �ڷ�ƾ
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
    //    //2�� �뽬
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