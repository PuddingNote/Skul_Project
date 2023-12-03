using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public RaycastHit2D hit;            // Ground üũ ����
    private PlayerController playerController;
    private float rayLength;

    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    // �÷��̾��� ��ġ�� Ground�� �ִ��� ���߿� �ִ��� Ȯ���ϴ� BoxCast
    void Update()
    {
        rayLength = playerController.player.groundCheckLength;

        hit = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.2f), 
            0f, Vector2.down, rayLength, LayerMask.GetMask(GData.GROUND_LAYER_MASK));
    }
}
