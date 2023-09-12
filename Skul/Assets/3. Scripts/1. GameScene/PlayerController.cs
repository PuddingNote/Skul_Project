using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private Animator playerAni;

    private bool isGround = true;
    private float dashForce = 10f;
    private bool canDash = true;
    private bool isDash = false;
    private int dashCount = default;
    private float dashTime = 0.3f;
    private float dashCooldown = 1f;


    private float speed = 5f;
    private float hp = default;
    private float maxHp = 100;

    private bool isRight = true;
    private float horizontal = default;

    private bool isWalk = false;
    private bool isJump = false;

    private float posY = default;

    private bool isFall = false;
    private uint fallCount = default;

    private int jumpCount = 0;
    private float jumpForce = 8f;

    private void Awake()
    {
        playerRig = gameObject.GetComponent<Rigidbody2D>();
        playerAni = gameObject.GetComponent<Animator>();
        //playerRig.gravityScale = 1.0f;
        //hp = maxHp;
    }

    private void Update()
    {
        MoveControl();
        TurnPlayer();
    }

    private void FixedUpdate()
    {
        // ���� ��� üũ
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

        // �뽬 ó��
        if (isDash)
        {
            return;
        }
        playerRig.velocity = new Vector2(horizontal * speed, playerRig.velocity.y);
    }

    private void MoveControl()
    {
        if (isDash)
        {
            return;
        }

        // �ִϸ����� ������ ���� bool�� ó��
        if (Input.GetAxisRaw("Horizontal") == default)
        {
            isWalk = false;
        }
        else
        {
            isWalk = true;
        }

        // �¿� ����Ű �Է�
        horizontal = Input.GetAxisRaw("Horizontal");
        playerAni.SetBool("isWalk", isWalk);

        // ����Ű �Է�
        if (Input.GetKeyDown(KeyCode.X) && jumpCount < 2)
        {
            isJump = true;
            jumpCount++;
            playerRig.velocity = transform.up * jumpForce;
        }
        playerAni.SetBool("isJump", isJump);

        // �뽬Ű �Է�
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {

        }

        // ����Ű �Է�
        if (Input.GetKeyDown(KeyCode.C))
        {

        }
    }

    // �÷��̾� ���� ��ȯ �Լ�
    private void TurnPlayer()
    {
        if (isRight && horizontal < 0f || !isRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isRight = !isRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

}