using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        IDLE = 0,
        MOVE,
        JUMP,
        DASH,
        ATTACK,
        DEAD
    };

    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public bool isGround = true;
    public bool canDash = true;
    public PlayerState enumState = PlayerState.IDLE;            // �÷��̾� ������� üũ ����

    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dicState = new Dictionary<PlayerState, IPlayerState>();

    void Start()
    {
        playerHp = playerMaxHp;
        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();

        dicState.Add(PlayerState.IDLE, idle);
        dicState.Add(PlayerState.MOVE, move);
        dicState.Add(PlayerState.JUMP, jump);
        dicState.Add(PlayerState.DASH, dash);
        dicState.Add(PlayerState.ATTACK, attack);
        pStateMachine = new PStateMachine(idle, this);
    }


    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGround == true)
        {
            pStateMachine.SetState(dicState[PlayerState.MOVE]);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            pStateMachine.SetState(dicState[PlayerState.JUMP]);
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash == true)
        {
            pStateMachine.SetState(dicState[PlayerState.DASH]);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            pStateMachine.SetState(dicState[PlayerState.ATTACK]);
        }

        if (Input.anyKey == false && isGround == true)
        {
            pStateMachine.SetState(dicState[PlayerState.IDLE]);
        }

        pStateMachine.DoUpdate();
    }

    // interface�� ��ӹ��� Ŭ������ MonoBehaviour�� ��� ���� ���ؼ� �ڷ�ƾ�� ��� ��������� �Լ�
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}
