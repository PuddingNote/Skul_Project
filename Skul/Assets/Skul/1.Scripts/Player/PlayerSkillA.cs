using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillA : IPlayerState
{
    private PlayerController pController;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.SKILLA;
        pController.player.playerAni.SetBool("isSkillA", true);
    }

    public void StateFixedUpdate()
    {
        
    }

    public void StateUpdate()
    {
        
    }

    public void StateExit()
    {
        pController.player.playerAni.SetBool("isSkillA", false);
    }

}
