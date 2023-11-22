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

        GameObject deadEffect = GameObject.Instantiate(Resources.Load("죽는이펙트Prefab경로") as GameObject);
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
        // 슬로우모션
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForEndOfFrame();

        // 저장할 스크린샷의 틀 크기 지정 screenTex 600x600
        screenTex = new Texture2D(600, 600, TextureFormat.RGB24, false);

        // 플레이어의 포지션을 월드포지션에서 스크린포지션으로 변환
        var pos = pController.player.transform.position;
        var playerPos = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y, 0));

        // Rect를 만들어서 readpixel 파라메터의 소스로 쓰기 위해 공간 지정
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
