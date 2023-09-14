using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriterenderer;

    public Transform player;
    public float speed;
    public Vector2 home;

    public float attackCoolTime;
    public float attackDelay;

    public Transform boxpos;
    public Vector2 boxSize;


    public void Awake()
    {
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        home = transform.position;
        speed = 3f;
        attackCoolTime = 4f;
    }

    private void Update()
    {
        if (attackDelay >= 0)
        {
            attackDelay -= Time.deltaTime;
        }

    }

    public void DirectionEnemy(float target, float baseobj)
    {
        if (target < baseobj)
        {
            animator.SetFloat("Direction", -1);
            spriterenderer.flipX = true;
        }
        else
        {
            animator.SetFloat("Direction", 1);
            spriterenderer.flipX = false;
        }
    }

    // Attack 애니메이션에서 참조
    public void Attack()
    {
        if (animator.GetFloat("Direction") == -1)
        {
            if (boxpos.localPosition.x > 0)
            {
                boxpos.localPosition = new Vector2(boxpos.localPosition.x * -1, boxpos.localPosition.y);
            }
        }
        else
        {
            if (boxpos.localPosition.x < 0)
            {
                boxpos.localPosition = new Vector2(Mathf.Abs(boxpos.localPosition.x), boxpos.localPosition.y);
            }
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(boxpos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("Damage");
            }
        }
    }

}
