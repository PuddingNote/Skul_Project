using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상태 인터페이스
public interface State
{
    void Action();
}

// 이동 상태
public class Move : MonoBehaviour, State
{
    public void Action()
    {

    }
}

// 공격 상태
public class Attack : MonoBehaviour, State
{
    public void Action()
    {

    }
}