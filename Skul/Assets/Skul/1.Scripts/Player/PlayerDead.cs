using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IPlayerState
{
    private PlayerController pController;
    private Texture2D screenTex;
    private Rect area;
    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DEAD;

        GameObject deadEffect = GameObject.Instantiate(Resources.Load("�״�����ƮPrefab���") as GameObject);
        deadEffect.transform.position = pController.player.transform.position;
        deadEffect.SetActive(true);
        pController.CoroutineDeligate(TakeScreenShotRoutine());
    }

    public void StateUpdate()
    {
        
    }

    public void StateExit()
    {
        
    }

    private IEnumerator TakeScreenShotRoutine()
    {
        // ���ο���
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForEndOfFrame();

        // ������ ��ũ������ Ʋ ũ�� ���� screenTex 600x600
        screenTex = new Texture2D(600, 600, TextureFormat.RGB24, false);

        // �÷��̾��� �������� ���������ǿ��� ��ũ������������ ��ȯ
        var pos = pController.player.transform.position;
        var playerPos = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y, 0));

        // Rect�� ���� readpixel �Ķ������ �ҽ��� ���� ���� ���� ����
        area = new Rect(playerPos.x - 300f, playerPos.y - 300f, playerPos.x + 300f, playerPos.y + 300f);
        screenTex.ReadPixels(area, 0, 0);
        screenTex.LoadImage(screenTex.EncodeToPNG());
        Rect rect = new Rect(0, 0, screenTex.width, screenTex.height);
        //UIManager.Instance.daedScreenShot = Sprite.Create(screenTex, rect, Vector2.one * 0.5f);
        //UIManager.Instance.resultObj.SetActive(true);
        Time.timeScale = 1f;
        pController.gameObject.SetActive(false);
    }
}
