using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;

    public static UIManager Instance
    {
        get
        {
            if (instance == null || instance == default)
            {
                return null;
            }
            return instance;
        }
    }

    // ½Ì±ÛÅæ ÆÐÅÏ Àû¿ë
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitUIManager();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void InitUIManager()
    {

    }

}
