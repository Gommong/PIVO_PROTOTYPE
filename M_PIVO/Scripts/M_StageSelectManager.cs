using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_StageSelectManager : MonoBehaviour {

    List<GameObject> SwipeObjects = new List<GameObject>();

    private bool[] CanPlay;
    private bool[] IsClear;
    private int StageLength;
    private float[,] StageData;
    private int ToCheatNum = 1;

    public GameObject StageSelectButton;

    public GameObject CostUnlockStage;  //코스트없는거..

    public Text RequiredGemText;
    public Text ConsumeBiscuitText;

    private string StageManager = "StageManager";
    private int RequiredGem = 20; //오 되네
    private int ConsumedBiscuit = 10;

    private int TotalBiscuit;
    private int TotalGem;
    
    private bool IsEntered = false;


    void Start ()
    {
        InitializeData();
        CostUIInitialize();
        InitializeStageSelectManager();
    }
	
	void Update ()
    {
        UpdateTotalCost();
        StageEnableControl();
        ButtonControl();
        IsClearCheat();
    }

    //Update라는 용어는 말그대로 Update에서 사용되도록 하는 함수이므로 Update라는 이름을 한 번부르는 함수에서 사용하지 말아주세요
    void CostUIInitialize() //스테이지 들어가는 조건 UI에 값넣어주는건데 한번만 돌면 돼서 흠
    {
        RequiredGemText.text = RequiredGem.ToString(); 
        ConsumeBiscuitText.text = ConsumedBiscuit.ToString();
    }

    void UpdateTotalCost()  //현재 얼마나 갖고 있는지 갱신하기
    {
        TotalBiscuit = GameObject.Find(StageManager).GetComponent<M_CostManager>().TotalBiscuit; 
        TotalGem = GameObject.Find(StageManager).GetComponent<M_CostManager>().TotalGem; 
    }

    void ReturnTotalCost()  //여기서 변한 값 CostManager에 돌려주기
    {
        GameObject.Find(StageManager).GetComponent<M_CostManager>().TotalBiscuit = TotalBiscuit;
        GameObject.Find(StageManager).GetComponent<M_CostManager>().TotalGem = TotalGem;
    }

    void ChangeEnableStage(GameObject SWObject, bool bState)
    {
        if (bState)
        {
            SWObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            //이거  자식 있으면 없애야
            //for (int i = 0; i <= 2; i++)
            //{
            //    SWObject.transform.GetChild(i).gameObject.SetActive(false);
            //}
        }
        else
        {
            SWObject.GetComponent<Image>().color = new Color32(130, 130, 130, 255);
        }
    }

    void CheckSwipeObject(int Length)
    {
        for (int i = 0; i < Length; i++)
        {
            SwipeObjects.Add(GameObject.Find("SwipeObject" + i.ToString()));
        }
    }

    bool CheckStageOpen(float a, float b, float c)
    {
        bool ReturnValue = false;

        if (IsClear[(int)a] == true)
        {
            if (b <= TotalGem)
            {
                if (c <= TotalBiscuit)
                    ReturnValue = true;
            }
        }
        else
            ReturnValue = false;
        
        return ReturnValue;
    }

    void ButtonControl()
    {
        int CenterNum = GameObject.Find("SwipeManager").GetComponent<M_SwipeManager>().FindCenter();

        if (CanPlay[CenterNum])
            StageSelectButton.SetActive(true);
        else
            StageSelectButton.SetActive(false);
    }

    void IsClearCheat()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            IsClear[ToCheatNum] = true;
            ToCheatNum++;
        }
    }

    void StageEnableControl()
    {
        for (int i = 0; i < StageLength; i++)
        {
            if (CheckStageOpen(StageData[i, 1], StageData[i, 2], StageData[i, 3]))
            {
                ChangeEnableStage(SwipeObjects[i], true);
                CanPlay[i] = true;
            }
        }
    }

    void InitializeStageSelectManager()
    {
        for (int i = 0; i < StageLength; i++)
        {
            ChangeEnableStage(SwipeObjects[i], false);
        }
    }

    void InitializeData()
    {
        StageLength = GameObject.FindGameObjectsWithTag("SwipeObject").Length;
        
        //배열 초기화
        CheckSwipeObject(StageLength);
        IsClear = new bool[StageLength];
        CanPlay = new bool[StageLength];
        StageData = new float[StageLength, 4];

        IsClear[0] = true;

        StageData[0, 0] = 1; //스테이지 번호
        StageData[0, 1] = 0; //해당 스테이지가 클리어되어야함
        StageData[0, 2] = 0; //필요한 보석 갯수
        StageData[0, 3] = 0; //필요한 비스킷 갯수

        //데이터 테이블을 만들기 귀찮음으로 반복문으로 만듬
        for (int i = 0; i < StageLength; i++)
        {
            StageData[i, 0] = 1 + i;
            StageData[i, 1] = i;
            StageData[i, 2] = 0 + i * 5;
            StageData[i, 3] = 0 + i * 5;
            CanPlay[i] = false;
        }
    }
}
