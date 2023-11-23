using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMageSkul : MonoBehaviour
{
    private GameObject enterKeyIcon;        // 자식오브젝트 변수
    private PlayerController player;        // 트리거에 감지된 플레이어 담을 변수
    private bool isPushKey = false;         // 플레이어 키입력 받는 조건 변수

    void Awake()
    {
        enterKeyIcon = gameObject.FindChildObj("EnterKeyIcon");
    }

    void Update()
    {
        if (isPushKey == true)
        {
            // F키 입력시 MageSkul 획득 조건 true
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetSkul(player);
                gameObject.SetActive(false);
            }
        }
    }

    // 플레이어가 MageSkul을 획득하는 함수
    private void GetSkul(PlayerController _player)
    {
        if (_player != null || _player != default)
        {
            // 플레이어가 MageSkul을 가지고있으면 리턴
            if (_player.GetComponent<MageSkul>() == true)
            {
                return;
            }

            // 현재 활성화되어있는 Skul스크립트를 비활성화
            _player.gameObject.GetComponent<Skul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
            if (GetComponent<MageSkul>() == null)
            {
                // MageSkul을 가지고 있지 않으면 스컬리스트에 추가
                _player.gameObject.GetComponent<PlayerController>().playerSkulList.Add(_player.gameObject.AddComponent<MageSkul>());
                return;
            }

            // MageSkul 스크립트를 활성화
            _player.gameObject.GetComponent<MageSkul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어가 범위에 들어오면 F키 아이콘 활성화 및 버튼입력 가능하게 isPushKey = true
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            enterKeyIcon.SetActive(true);
            isPushKey = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            player = other.gameObject.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 플레이어가 범위에서 벗어나면 F키 아이콘 비활성 및 버튼입력 불가능하게 isPushKey = false
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            enterKeyIcon.SetActive(false);
            isPushKey = false;
        }
    }
}
