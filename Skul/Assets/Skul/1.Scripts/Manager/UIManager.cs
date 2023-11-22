using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //private GameObject loadingObj = default;
    //public GameObject mainUiObj = default;
    //public GameObject resultObj = default;
    //public GameObject showStageObj = default;
    //public GameObject optionObj = default;
    //public GameObject minimap = default;
    //public Sprite daedScreenShot;
    //public Sprite mainSkul;
    //public Sprite subSkul;
    //public Sprite mainSkillA;
    //public Sprite mainSkillB;
    //public Sprite subSkillA;
    //public Sprite subSkillB;
    //public int playerHp;
    //public int playerMaxHp;
    //public float swapCoolDown;

    // 싱글톤 패턴 적용
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
        //mainUiObj = gameObject.FindChildObj(GData.MAIN_UI_OBJ_NAME);
        //loadingObj = gameObject.FindChildObj(GData.LOADDING_OBJ_NAME);
        //resultObj = gameObject.FindChildObj(GData.RESULT_UI_OBJ_NAME);
        //optionObj = gameObject.FindChildObj(GData.EXIT_UI_OBJ_NAME);
        //showStageObj = mainUiObj.FindChildObj(GData.SHOWSTAGE_UI_OBJ_NAME);
        //minimap = mainUiObj.FindChildObj(GData.MINIMAP_OBJ_NAME);
        //daedScreenShot = default;
        //mainSkul = default;
        //subSkul = default;
        //mainSkillA = default;
        //mainSkillB = default;
        //subSkillA = default;
        //subSkillB = default;
        //swapCoolDown = default;
        //skillACoolDown = default;
        //skillBCoolDown = default;
        //maxSkillACool = default;
        //maxSkillBCool = default;
    }

    //// Stage입장시 정보UI 보여주는 함수
    //public void ShowStageName(string mainName, string subName)
    //{
    //    TMP_Text mainStageName = showStageObj.FindChildObj("MainName").GetComponentMust<TMP_Text>();
    //    TMP_Text subStageName = showStageObj.FindChildObj("SubName").GetComponentMust<TMP_Text>();
    //    mainStageName.text = mainName;
    //    subStageName.text = subName;
    //    StartCoroutine(StageName());
    //}

    //IEnumerator StageName()
    //{
    //    showStageObj.SetActive(true);
    //    yield return new WaitForSeconds(4.5f);
    //    showStageObj.SetActive(false);
    //}

    //// 로딩창 출력 후 해당씬으로 이동
    //public void ShowLoading(bool activeImage)
    //{
    //    loadingObj.SetActive(activeImage);
    //}
}
