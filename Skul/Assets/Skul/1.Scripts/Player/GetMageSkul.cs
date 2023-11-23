using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMageSkul : MonoBehaviour
{
    private GameObject enterKeyIcon;        // �ڽĿ�����Ʈ ����
    private PlayerController player;        // Ʈ���ſ� ������ �÷��̾� ���� ����
    private bool isPushKey = false;         // �÷��̾� Ű�Է� �޴� ���� ����

    void Awake()
    {
        enterKeyIcon = gameObject.FindChildObj("EnterKeyIcon");
    }

    void Update()
    {
        if (isPushKey == true)
        {
            // FŰ �Է½� MageSkul ȹ�� ���� true
            if (Input.GetKeyDown(KeyCode.F))
            {
                GetSkul(player);
                gameObject.SetActive(false);
            }
        }
    }

    // �÷��̾ MageSkul�� ȹ���ϴ� �Լ�
    private void GetSkul(PlayerController _player)
    {
        if (_player != null || _player != default)
        {
            // �÷��̾ MageSkul�� ������������ ����
            if (_player.GetComponent<MageSkul>() == true)
            {
                return;
            }

            // ���� Ȱ��ȭ�Ǿ��ִ� Skul��ũ��Ʈ�� ��Ȱ��ȭ
            _player.gameObject.GetComponent<Skul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
            if (GetComponent<MageSkul>() == null)
            {
                // MageSkul�� ������ ���� ������ ���ø���Ʈ�� �߰�
                _player.gameObject.GetComponent<PlayerController>().playerSkulList.Add(_player.gameObject.AddComponent<MageSkul>());
                return;
            }

            // MageSkul ��ũ��Ʈ�� Ȱ��ȭ
            _player.gameObject.GetComponent<MageSkul>().enabled = !(_player.gameObject.GetComponent<Skul>().enabled);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾ ������ ������ FŰ ������ Ȱ��ȭ �� ��ư�Է� �����ϰ� isPushKey = true
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
        // �÷��̾ �������� ����� FŰ ������ ��Ȱ�� �� ��ư�Է� �Ұ����ϰ� isPushKey = false
        if (other.tag == GData.PLAYER_LAYER_MASK)
        {
            enterKeyIcon.SetActive(false);
            isPushKey = false;
        }
    }
}
