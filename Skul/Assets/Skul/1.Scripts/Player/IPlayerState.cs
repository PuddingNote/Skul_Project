using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void StateEnter(PlayerController pController);
    void StateUpdate();
    void StateExit();

}