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
    public RuntimeAnimatorController BeforeChangeRuntimeC;  // SkulHeadless�� ����ϱ�����
    public List<Player> playerSkulList;         // �÷��̾ ����� �� �ִ� Skul�� List
    private SpriteRenderer playerSprite;

    private Player possibleSkul;

    public Player player;
    public int playerHp;
    public int playerMaxHp = 100;
    public int currentHp;
    public bool canDash = true;             // �뽬 ��밡�� üũ
    public bool isGetSkulSkillA = false;
    public bool isHit = false;              // �ǰ�üũ
    public bool isDead = false;             // ���üũ

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

        // �⺻ ������ ��Ÿ�Ӿִ���Ʈ�ѷ��� ���� -> ��ųA, B���� ��Ÿ�Ӿִ���Ʈ�ѷ��� �����ϴ� ����
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

    // �÷��̾� ���� ����
    public void ChangeState(PlayerState p_state)
    {
        if (dictionaryState[p_state] == null)
        {
            return;
        }

        pStateMachine.SetState(dictionaryState[p_state]);
    }

    // �÷��̾� ���� ����
    private void SelectState()
    {
        // �÷��̾ ���� ���¸� ����
        if (isDead == true)
        {
            return;
        }

        // �÷��̾� Hp�� 0���� �۰ų� ������ Dead
        if (playerHp <= 0)
        {
            // �÷��̾��� Hit���¸� üũ�� �������� ����
            isDead = true;
            playerHp = 0;
            ChangeState(PlayerState.DEAD);
        }

        // �÷��̾ �ǰݴ��ϸ� �ǰ�ó��
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

        // Skill ���
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

        // ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && swapCoolDown == 5f 
            && enumState != PlayerState.DASH)
        {
            ChangePlayer();
        }

        // �������ϰ� ��������
        if (player.playerRb.velocity.y < -1
        && ((enumState == PlayerState.IDLE || enumState == PlayerState.MOVE)
        || (enumState != PlayerState.JUMP && player.playerAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)))
        {
            ChangeState(PlayerState.JUMP);
        }
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
        ChangeState(PlayerState.IDLE);
        StartCoroutine(SwapCoolDown());
    }

    // ĳ���� Swap��Ÿ�� ���� �ڷ�ƾ �Լ�
    IEnumerator SwapCoolDown()
    {
        // ������Ÿ�� 5�� ����
        for (int i = 0; i < 50; i++)
        {
            float tick = 0.1f;
            swapCoolDown -= tick;
            yield return new WaitForSeconds(tick);
        }
        swapCoolDown = 5f;
    }

    // ��ųA ��Ÿ�� �ڷ�ƾ�Լ�
    IEnumerator SkillACoolDown()
    {
        float skillACool = player.skillACool;

        for (int i = 0; i < skillACool * 10; i++)
        {
            float tick = 0.1f;

            if (isGetSkulSkillA == true)
            {
                // Skull�� SkillA����� ������带 ���������� ���ʱ�ȭ
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

    // ��ųB ��ٿ� �ڷ�ƾ�Լ�
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

    // interface�� ��ӹ��� Ŭ������ MonoBehaviour�� ��ӹ��� �ʾƼ� �ڷ�ƾ�� ��� ��������� �Լ��� �ʿ��ؼ� ������µ�
    // �׳� �غ��� �ȴ� ?? �� �Ǵ°��� (PlayerDash Script)
    public void CoroutineDeligate(IEnumerator func)
    {
        StartCoroutine(func);
    }

    // ���Ϳ��� �ǰݴ������� �����ϴ� �ڷ�ƾ�Լ� (������������)
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
