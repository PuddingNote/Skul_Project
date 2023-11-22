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
        SKILLA,
        SKILLB,
        DEAD
    };

    public PlayerGroundCheck isGroundRay;
    public PlayerState enumState = PlayerState.IDLE;
    //private PStateMachine _pStateMachine;

    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dictionaryState = new Dictionary<PlayerState, IPlayerState>();
    public RuntimeAnimatorController BeforeChangeRuntimeC;
    public List<Player> playerSkulList;         // 플레이어가 사용할 수 있는 Skul의 List
    private SpriteRenderer playerSprite;

    private Player possibleSkul;

    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public int currentHp;
    public bool canDash = true;         // 대쉬 사용가능 체크
    public bool isGetSkulSkillA = false;
    public bool isHit = false;          // 피격체크
    public bool isDead = false;         // 사망체크

    public float swapCoolDown = 5f;
    public float skillACoolDown;
    public float skillBCoolDown;

    void Start()
    {
        possibleSkul = gameObject.AddComponent<Skul>();
        playerSkulList = new List<Player>();
        playerSkulList.Add(possibleSkul);
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        // 기본 스컬의 런타임애니컨트롤러를 저장 -> 스킬A, B사용시 런타임애니컨트롤러를 변경하는 로직
        BeforeChangeRuntimeC = Resources.Load("2.Animations/PlayerAni/Skul/Skul") as RuntimeAnimatorController;

        isGroundRay = gameObject.GetComponent<PlayerGroundCheck>();
        playerHp = playerMaxHp;
        currentHp = playerHp;

        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();
        IPlayerState dead = new PlayerDead();
        IPlayerState skillA = new PlayerSkillA();
        IPlayerState skillB = new PlayerSkillB();

        dictionaryState.Add(PlayerState.IDLE, idle);
        dictionaryState.Add(PlayerState.MOVE, move);
        dictionaryState.Add(PlayerState.JUMP, jump);
        dictionaryState.Add(PlayerState.DASH, dash);
        dictionaryState.Add(PlayerState.ATTACK, attack);
        dictionaryState.Add(PlayerState.SKILLA, skillA);
        dictionaryState.Add(PlayerState.SKILLB, skillB);
        dictionaryState.Add(PlayerState.DEAD, dead);

        pStateMachine = new PStateMachine(idle, this);
    }

    void Update()
    {
        SelectState();
        pStateMachine.DoUpdate();
    }

    // 플레이어 상태 변경
    public void ChangeState(PlayerState p_state)
    {
        if (dictionaryState[p_state] == null)
        {
            return;
        }

        pStateMachine.SetState(dictionaryState[p_state]);
    }

    // 플레이어 상태 선택
    private void SelectState()
    {
        // 플레이어가 죽은 상태면 리턴
        if (isDead == true)
        {
            return;
        }

        // 플레이어 Hp가 0보다 작거나 같으면 Dead
        if (playerHp <= 0)
        {
            // 플레이어의 Hit상태를 bool값으로 체크해 무적상태 구현
            isDead = true;
            playerHp = 0;
            ChangeState(PlayerState.DEAD);
        }

        // 플레이어가 피격당하면 피격처리
        if (playerHp < currentHp && isHit == false && isDead == false)
        {
            isHit = true;
            StartCoroutine(HitPlayer());
            currentHp = playerHp;
        }

        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && isGroundRay.hit.collider != null
            && enumState != PlayerState.DASH 
            && (enumState == PlayerState.IDLE || (enumState != PlayerState.MOVE && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
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

        if (Input.GetKeyDown(KeyCode.C) && enumState != PlayerState.DASH)
        {
            ChangeState(PlayerState.ATTACK);
        }

        if (isGroundRay.hit.collider != null
        && (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.LeftArrow) == false)
        && enumState != PlayerState.DASH
        && (enumState == PlayerState.MOVE || (enumState != PlayerState.IDLE && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            ChangeState(PlayerState.IDLE);
        }

        // Skill 사용
        if (Input.GetKeyDown(KeyCode.A)
        && skillACoolDown == player.skillACool
        && enumState != PlayerState.DASH)
        {
            ChangeState(PlayerState.SKILLA);
            StartCoroutine(SkillACoolDown());
        }

        if (Input.GetKeyDown(KeyCode.S)
        && skillBCoolDown == player.skillBCool
        && enumState != PlayerState.DASH)
        {
            if (player.playerAni.runtimeAnimatorController.name == "Skul")
            {
                return;
            }
            ChangeState(PlayerState.SKILLB);
            StartCoroutine(SkillBCoolDown());
        }

        // 스컬 변경
        if (Input.GetKeyDown(KeyCode.Space) && swapCoolDown == 5f 
            && enumState != PlayerState.DASH)
        {
            ChangePlayer();
        }

        // Jump상태가 아닐때 Velocity.y값이 -1보다 작으면 낙하시작 (점프안하고 떨어질때)
        if (player.playerRb.velocity.y < -1
        && ((enumState == PlayerState.IDLE || enumState == PlayerState.MOVE)
        || (enumState != PlayerState.JUMP && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            ChangeState(PlayerState.JUMP);
        }
    }

    // 플레이어 스컬교체하는 함수
    private void ChangePlayer()
    {
        // 스컬을 1개만 가지고있으면 리턴
        if (playerSkulList.Count < 2)
        {
            return;
        }

        // 스컬리스트의 활성화 상태를 반전시킴
        for (int i = 0; i < playerSkulList.Count; i++)
        {
            playerSkulList[i].enabled = !playerSkulList[i].enabled;
        }
        ChangeState(PlayerState.IDLE);
        StartCoroutine(SwapCoolDown());
    }

    // 캐릭터 Swap쿨다운 적용 코루틴 함수
    IEnumerator SwapCoolDown()
    {
        // 스왑쿨다운 5초 설정
        for (int i = 0; i < 50; i++)
        {
            float tick = 0.1f;
            swapCoolDown -= tick;
            yield return new WaitForSeconds(tick);
        }
        swapCoolDown = 5f;
    }

    // 스킬A 쿨다운 코루틴함수
    private IEnumerator SkillACoolDown()
    {
        float skillACool = player.skillACool;
        for (int i = 0; i < skillACool * 10; i++)
        {
            var tick = 0.1f;
            if (isGetSkulSkillA == true)
            {
                // Skull의 SkillA사용후 스컬헤드를 습득했을시 쿨초기화
                skillACoolDown = player.skillACool;
                isGetSkulSkillA = false;
                //UIManager.Instance.skillACoolDown = skillACoolDown;
                yield break;
            }
            if (skillACool != player.skillACool)
            {
                skillACoolDown = player.skillACool;
                //UIManager.Instance.skillACoolDown = skillACoolDown;
                yield break;
            }
            skillACoolDown -= tick;
            //UIManager.Instance.skillACoolDown = skillACoolDown;
            yield return new WaitForSeconds(tick);
        }
        skillACoolDown = player.skillACool;
    }

    // 스킬B 쿨다운 코루틴함수
    private IEnumerator SkillBCoolDown()
    {
        float skillBCool = player.skillBCool;
        for (int i = 0; i < skillBCool * 10; i++)
        {
            var tick = 0.1f;
            if (skillBCool != player.skillBCool)
            {
                skillBCoolDown = player.skillBCool;
                //UIManager.Instance.skillBCoolDown = skillBCoolDown;
                yield break;
            }
            skillBCoolDown -= tick;
            //UIManager.Instance.skillBCoolDown = skillBCoolDown;
            yield return new WaitForSeconds(tick);
        }
        skillBCoolDown = player.skillBCool;
    }

    // interface를 상속받은 클래스는 MonoBehaviour를 상속 받지 못해서 코루틴을 대신 실행시켜줄 함수
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    // 몬스터에게 피격당할 시 실행하는 코루틴함수
    IEnumerator HitPlayer()
    {
        // 스프라이트 컬러의 알파값을 바꿔 깜빡거리게 구현
        Color original = playerSprite.color;
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 0.3f);
        yield return new WaitForSeconds(0.2f);
        playerSprite.color = new Color(255f, 255f, 255f, 1f);
        playerSprite.color = original;
        isHit = false;
    } //HitPlayer

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    isGround = true;
        //}
    }
}
