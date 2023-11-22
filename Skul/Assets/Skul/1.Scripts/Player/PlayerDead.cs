using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : IPlayerState
{
    private PlayerController pController;

    public void StateEnter(PlayerController _pController)
    {
        this.pController = _pController;
        pController.enumState = PlayerController.PlayerState.DEAD;

        GameObject deadEffect = GameObject.Instantiate(Resources.Load("0.Prefabs/DeadEffect") as GameObject);
        deadEffect.transform.position = pController.player.transform.position;
        deadEffect.SetActive(true);
        //Debug.Log(pController.enumState);

        pController.gameObject.SetActive(false);
    }

    public void StateUpdate()
    {
        
    }

    public void StateExit()
    {
        
    }

}
