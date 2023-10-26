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

    public enum PlayerSkul
    {
        SKUL,
        MAGE
    }

    public PlayerSkul currentSkul = PlayerSkul.SKUL;
    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public bool isGround = true;
    public bool canDash = true;
    public PlayerGroundCheck isGroundRay;
    public PlayerState enumState = PlayerState.IDLE;            // 플레이어 현재상태 체크 변수

    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dictionaryState = new Dictionary<PlayerState, IPlayerState>();
    private SpriteRenderer playerSprite;
    public RuntimeAnimatorController BeforeChangeRuntimeC;
    public Animator playerAni;

    void Start()
    {
        gameObject.AddComponent<Skul>();
        //BeforeChangeRuntimeC = player.playerAni.runtimeAnimatorController;
        //InitPlayer();
        playerHp = playerMaxHp;
        isGroundRay = gameObject.GetComponent<PlayerGroundCheck>();
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
        //if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGroundRay.hit.collider != null)
        //{
        //    if (enumState != PlayerState.DASH)
        //    {
        //        if (player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        //        {
        //            ChangeState(PlayerState.MOVE);
        //        }
        //    }
        //}

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGroundRay.hit.collider != null && enumState != PlayerState.DASH)
        {
            // 현재 상태가 Attack상태라면 애니메이션이 끝나고 Move시작
            //if (enumState == PlayerState.ATTACK)
            //{
            //    if (player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            //    {
            //        return;
            //    }
            //}
            //else
            {
                ChangeState(PlayerState.MOVE);
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
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

        if (Input.anyKey == false && isGroundRay.hit.collider != null && enumState != PlayerState.DASH)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePlayer();
        }
        pStateMachine.DoUpdate();
    }

    //private void InitPlayer()
    //{
    //    GameObject childObj = Resources.Load("Prefabs/Skul") as GameObject;
    //    playerSprite.sprite = childObj.GetComponent<SpriteRenderer>().sprite;
    //    playerHp = playerMaxHp;
    //}

    // 플레이어 스컬교체하는 함수
    private void ChangePlayer()
    {
        GetComponent<Skul>().enabled = !(GetComponent<Skul>().enabled);
        if (GetComponent<MageSkul>() == null)
        {
            gameObject.AddComponent<MageSkul>();
            return;
        }
        GetComponent<MageSkul>().enabled = !(GetComponent<Skul>().enabled);
    }

    // interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
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
