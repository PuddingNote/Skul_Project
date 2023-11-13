using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : IPlayerState
{
    private PlayerController pController;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.IDLE;
        Debug.Log(pController.enumState);
    }

    public void StateUpdate()
    {
        
    }

    public void StateExit()
    {
        
    }
}
