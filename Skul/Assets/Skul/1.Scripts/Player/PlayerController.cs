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
    public PlayerGroundCheck isGroundRay;
    public PlayerState enumState = PlayerState.IDLE;
    //private PStateMachine _pStateMachine;

    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dictionaryState = new Dictionary<PlayerState, IPlayerState>();
    //private SpriteRenderer playerSprite;
    public RuntimeAnimatorController BeforeChangeRuntimeC;
    public Animator playerAni;
    public List<Player> playerSkulList;         // �÷��̾ ����� �� �ִ� Skul�� List
    private Player possibleSkul;
    private float swapCoolDown = 0f;

    void Start()
    {
        // �⺻ ������ ��Ÿ�Ӿִ���Ʈ�ѷ��� ���� -> ��ųA, B���� ��Ÿ�Ӿִ���Ʈ�ѷ��� �����ϴ� ����
        possibleSkul = gameObject.AddComponent<Skul>();
        playerSkulList = new List<Player>();
        playerSkulList.Add(possibleSkul);

        //gameObject.AddComponent<Skul>();
        BeforeChangeRuntimeC = player.playerAni.runtimeAnimatorController;
        isGroundRay = gameObject.GetComponent<PlayerGroundCheck>();
        playerHp = playerMaxHp;

        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();

        dictionaryState.Add(PlayerState.IDLE, idle);
        dictionaryState.Add(PlayerState.MOVE, move);
        dictionaryState.Add(PlayerState.JUMP, jump);
        dictionaryState.Add(PlayerState.DASH, dash);
        dictionaryState.Add(PlayerState.ATTACK, attack);
        pStateMachine = new PStateMachine(idle, this);
    }

    // �÷��̾� ���� ����
    public void ChangeState(PlayerState p_state)
    {
        if (dictionaryState[p_state] == null)
        {
            return;
        }

        pStateMachine.SetState(dictionaryState[p_state]);
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGroundRay.hit.collider != null
            && enumState != PlayerState.DASH && enumState != PlayerState.ATTACK && enumState != PlayerState.JUMP)
        {
            ChangeState(PlayerState.MOVE);
        }

        if (Input.GetKeyDown(KeyCode.X) && !Input.GetKey(KeyCode.DownArrow))
        {
            ChangeState(PlayerState.JUMP);
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash == true)
        {
            ChangeState(PlayerState.DASH);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeState(PlayerState.ATTACK);
        }

        if (Input.anyKey == false && isGroundRay.hit.collider != null
            && enumState != PlayerState.DASH && enumState != PlayerState.ATTACK)
        {
            ChangeState(PlayerState.IDLE);
        }

        // Skill
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.playerAni.SetBool("isSkillA", true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.SkillB();
        }

        // ChangePlayer
        if (Input.GetKeyDown(KeyCode.Space) && swapCoolDown == 0f)
        {
            ChangePlayer();
        }

        pStateMachine.DoUpdate();
    }

    // �÷��̾� ���ñ�ü�ϴ� �Լ�
    private void ChangePlayer()
    {
        // ������ 1���� ������������ ����
        if (playerSkulList.Count < 2)
        {
            return;
        }

        // ���ø���Ʈ�� Ȱ��ȭ ���¸� ������Ŵ
        for (int i = 0; i < playerSkulList.Count; i++)
        {
            playerSkulList[i].enabled = !playerSkulList[i].enabled;
        }
        StartCoroutine(SwapCoolDown());

        //GetComponent<Skul>().enabled = !(GetComponent<Skul>().enabled);
        //if (GetComponent<MageSkul>() == null)
        //{
        //    gameObject.AddComponent<MageSkul>();
        //    return;
        //}
        //GetComponent<MageSkul>().enabled = !(GetComponent<Skul>().enabled);
    }

    // ĳ���� Swap��ٿ� ���� �ڷ�ƾ �Լ�
    IEnumerator SwapCoolDown()
    {
        // ������ٿ� 5�� ����
        for (int i = 0; i < 50; i++)
        {
            float tick = 0.1f;
            swapCoolDown += tick;
            yield return new WaitForSeconds(tick);
        }
        swapCoolDown = 0f;
    }

    // interface�� ��ӹ��� Ŭ������ MonoBehaviour�� ��� ���� ���ؼ� �ڷ�ƾ�� ��� ��������� �Լ�
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    isGround = true;
        //}
    }
}
