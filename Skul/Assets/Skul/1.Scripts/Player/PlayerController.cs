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

    public PStateMachine pStateMachine { get; private set; }
    private Dictionary<PlayerState, IPlayerState> dictionaryState = new Dictionary<PlayerState, IPlayerState>();
    public RuntimeAnimatorController BeforeChangeRuntimeC;  // SkulHeadless를 사용하기위해
    public List<Player> playerSkulList;         // 플레이어가 사용할 수 있는 Skul의 List
    private SpriteRenderer playerSprite;

    private Player possibleSkul;

    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public int currentHp;
    public bool canDash = true;             // 대쉬 사용가능 체크
    public bool isGetSkulSkillA = false;
    public bool isHit = false;              // 피격체크
    public bool isDead = false;             // 사망체크

    public float swapCoolDown = 5f;
    public float skillACoolDown;
    public float skillBCoolDown;

    void Start()
    {
        playerHp = playerMaxHp;
        playerSkulList = new List<Player>();
        playerSprite = gameObject.GetComponent<SpriteRenderer>();

        possibleSkul = gameObject.AddComponent<Skul>();
        playerSkulList.Add(possibleSkul);

        // 기본 스컬의 런타임애니컨트롤러를 저장 -> 스킬A, B사용시 런타임애니컨트롤러를 변경하는 로직
        BeforeChangeRuntimeC = Resources.Load("2.Animations/PlayerAni/Skul/Skul") as RuntimeAnimatorController;

        isGroundRay = gameObject.GetComponent<PlayerGroundCheck>();

        currentHp = playerHp;
        skillACoolDown = player.skillACool;
        skillBCoolDown = player.skillBCool;

        IPlayerState idle = new PlayerIdle();
        IPlayerState move = new PlayerMove();
        IPlayerState jump = new PlayerJump();
        IPlayerState dash = new PlayerDash();
        IPlayerState attack = new PlayerAttack();
        IPlayerState skillA = new PlayerSkillA();
        IPlayerState skillB = new PlayerSkillB();
        IPlayerState dead = new PlayerDead();

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
            // 플레이어의 Hit상태를 체크해 무적상태 구현
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
            && (enumState == PlayerState.IDLE 
            || (enumState != PlayerState.MOVE
            && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
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
        && (enumState == PlayerState.MOVE 
        || (enumState != PlayerState.IDLE 
        && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
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

        // 점프안하고 떨어질때
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

    // 캐릭터 Swap쿨타임 적용 코루틴 함수
    IEnumerator SwapCoolDown()
    {
        // 스왑쿨타임 5초 설정
        for (int i = 0; i < 50; i++)
        {
            float tick = 0.1f;
            swapCoolDown -= tick;
            yield return new WaitForSeconds(tick);
        }
        swapCoolDown = 5f;
    }

    // 스킬A 쿨타임 코루틴함수
    IEnumerator SkillACoolDown()
    {
        float skillACool = player.skillACool;

        for (int i = 0; i < skillACool * 10; i++)
        {
            float tick = 0.1f;

            if (isGetSkulSkillA == true)
            {
                // Skull의 SkillA사용후 스컬헤드를 습득했을시 쿨초기화
                skillACoolDown = player.skillACool;
                isGetSkulSkillA = false;
                yield break;
            }
            if (skillACool != player.skillACool)
            {
                skillACoolDown = player.skillACool;
                yield break;
            }
            skillACoolDown -= tick;
            yield return new WaitForSeconds(tick);
        }
        skillACoolDown = player.skillACool;
    }

    // 스킬B 쿨다운 코루틴함수
    IEnumerator SkillBCoolDown()
    {
        float skillBCool = player.skillBCool;

        for (int i = 0; i < skillBCool * 10; i++)
        {
            float tick = 0.1f;
            if (skillBCool != player.skillBCool)
            {
                skillBCoolDown = player.skillBCool;
                yield break;
            }
            skillBCoolDown -= tick;
            yield return new WaitForSeconds(tick);
        }
        skillBCoolDown = player.skillBCool;
    }

    // interface를 상속받은 클래스는 MonoBehaviour를 상속받지 않아서 코루틴을 대신 실행시켜줄 함수가 필요해서 만들었는데
    // 그냥 해보니 된다 ?? 왜 되는거지 (PlayerDash Script)
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    // 몬스터에게 피격당했을때 실행하는 코루틴함수 (무적상태적용)
    IEnumerator HitPlayer()
    {
        int hitTime = 0;
        Color original = playerSprite.color;

        while (hitTime < 10)
        {
            if (hitTime % 2 == 0)
            {
                playerSprite.color = new Color(original.r, original.g, original.b, 0.3f);
            }
            else
            {
                playerSprite.color = new Color(original.r, original.g, original.b, 1f);
            }
            yield return new WaitForSeconds(0.2f);

            hitTime++;
        }

        playerSprite.color = original;
        isHit = false;

        yield return null;
    }

}
