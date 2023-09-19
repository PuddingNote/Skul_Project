using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻���� �÷��̾���Ʈ�ѷ� ��ũ��Ʈ
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private Animator playerAni;

    // �뽬 ����
    //private bool isGround = true;       // �ٴ� �Ǻ�
    private float dashForce = 10f;      // �뽬 �Ÿ�
    private bool canDash = true;        // �뽬 ���� �Ǻ�
    private bool isDash = false;        // �뽬��
    private int dashCount = default;    // �뽬 ī��Ʈ
    private float dashTime = 0.3f;      // �뽬 �ð�
    private float dashCooldown = 1f;    // �뽬 ��Ÿ��

    // �÷��̾� ����
    private float speed = 5f;           // �ӵ�
    //private float hp = default;         // hp
    //private float maxHp = 100;          // �ִ� hp

    private bool isRight = true;        // �¿� �Ǻ�
    private float horizontal = default; // horizontal

    private bool isWalk = false;        // isWalk �Ǻ�
    private bool isJump = false;        // isJump �Ǻ�

    // ���� ����
    private float posY = default;       // y��
    private bool isFall = false;        // ���� �Ǻ�
    private uint fallCount = default;   // ���� ī��Ʈ
    private int jumpCount = 0;          // ���� Ƚ��
    private float jumpForce = 8f;       // ���� ��

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
        
        // �뽬 ó��
        if (isDash)
        {
            return;
        }
        playerRig.velocity = new Vector2(horizontal * speed, playerRig.velocity.y);
    }

    // ������ ���� �Լ�
    private void MoveControl()
    {
        // �뽬�߿� �̵����� X
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

        // ����Ű �Է� && 2�� ����
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
            StartCoroutine(Dash());
        }

        // ����Ű �Է�
        if (Input.GetKeyDown(KeyCode.C))
        {
            playerAni.SetTrigger("isAttack");
        }
    }

    // �÷��̾� ���� ��ȯ �Լ�
    private void TurnPlayer()
    {
        if (isRight && horizontal < 0f || !isRight && horizontal > 0f)  // �ݴ�Ű �Է� ����
        {
            Vector3 localScale = transform.localScale;
            isRight = !isRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // ���� ���� �Լ�
    private void FallControl()
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
    }

    // �뽬�ϴ� �ڷ�ƾ
    private IEnumerator Dash()
    {
        // �뽬 ���� ���� üũ
        canDash = false;   

        // �뽬 �� �ִϸ��̼� ����
        isDash = true;
        playerAni.SetBool("isDash", isDash);

        dashCount += 1;

        // ���� �߷°� ���� �� �뽬�߿� ���߷»��·� ��ȯ�ؼ� �÷��̾ ���������ʰ� ���� �� ���� ����
        float originalGravity = playerRig.gravityScale;
        playerRig.gravityScale = 0f;
        playerRig.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        yield return new WaitForSeconds(dashTime); // dashTime��ŭ ���
        playerRig.gravityScale = originalGravity;

        // �뽬 ���� �ִϸ��̼� ����
        isDash = false;
        playerAni.SetBool("isDash", isDash);

        // 2�� �뽬
        if (dashCount >= 2)
        {
            yield return new WaitForSeconds(dashCooldown); // dashCooldown��ŭ ���

            // �뽬 Ƚ��, ���� ���� �ʱ�ȭ
            dashCount = 0;
            canDash = true;

            // �ڷ�ƾ ����
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
            // ���� ���� �ʱ�ȭ
            fallCount = 0;
            isFall = false;

            // ���� ���� �ʱ�ȭ
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