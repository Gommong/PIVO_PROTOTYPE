using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_StageSelectManager : MonoBehaviour {

    List<GameObject> SwipeObjects = new List<GameObject>();

    private bool[] CanPlay;//플레이 가능한지를 확인
    private bool[] IsClear;//해당 스테이지가 클리어 됐는지 확인
    private int StageLength;
    private float[,] StageData;
    private int ToCheatNum = 1;
    private string StageManager = "StageManager";

    public GameObject StageSelectButton;
    public GameObject UnlockUI, LockUI;
    
    private int RequiredGem = 20; //오 되네
    private int ConsumedBiscuit = 10;

    private int TotalBiscuit;
    private int TotalGem;
    


    void Start ()
    {
        InitializeData();
        InitializeStageSelectManager();
        StageEnableControl();
        StageUIControl();
    }
	
	void Update ()
    {
        UpdateTotalCost();
        ButtonControl();
        IsClearCheat();
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

    void ChangeEnableStage(GameObject SWObject, bool bState)//스테이지를 활성화할 때 색을 바꿔주는 함수이다.
    {
        if (bState)
        {
            SWObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else
        {
            SWObject.GetComponent<Image>().color = new Color32(130, 130, 130, 255);
        }
    }

    void CheckSwipeObject(int Length)//선택할 수 있는 스테이지 갯수가 몇개인지 확인한다.
    {
        for (int i = 0; i < Length; i++)
        {
            SwipeObjects.Add(GameObject.Find("SwipeObject" + i.ToString()));
        }
    }

    bool CheckStageOpen(float a, float b, float c)//3가지 조건이 만족되면 true를 아니라면 false를 리턴한다.
    {
        bool ReturnValue = false;

        if (IsClear[(int)a] == true)
        {
            //if (b <= TotalGem)
            //{
                if (c <= TotalBiscuit)
                    ReturnValue = true;
            //}
        }
        else
            ReturnValue = false;
        
        return ReturnValue;
    }

    void ButtonControl()//스테이지 입장하기 버튼의 활성화/비활성화를 관리한다.
    {
        int CenterNum = GameObject.Find("SwipeManager").GetComponent<M_SwipeManager>().FindCenter();
        //FindCenter함수는 중앙에 있는 스테이지의 배열번호를 리턴한다.

        if (CanPlay[CenterNum])
            StageSelectButton.SetActive(true);
        else
            StageSelectButton.SetActive(false);
    }

    void IsClearCheat()//Z키를 누르면 클리어한 스테이지가 하나씩 올라간다.
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TotalBiscuit += 5;
            IsClear[ToCheatNum] = true;
            ToCheatNum++;

            StageEnableControl();
            DestroyUI();
            StageUIControl();
        }
    }

    void StageEnableControl()//입장 가능한 스테이지의 색를 활성화 시켜준다.
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

    void DestroyUI()//자식으로 있는 UI 삭제
    {
        for (int i = 0; i < StageLength; i++)
        {
            Destroy(SwipeObjects[i].transform.GetChild(0).gameObject);
        }
    }

    void StageUIControl()
    {
        for (int i = 0; i < StageLength; i++)
        {
            GameObject UIObject;
            if (CanPlay[i])
            {
                UIObject = Instantiate(UnlockUI, Vector3.zero, Quaternion.identity);
            }
            else
            {
                UIObject = Instantiate(LockUI, Vector3.zero, Quaternion.identity);
            }

            UIObject.transform.parent = SwipeObjects[i].transform;
            UIObject.transform.localPosition = new Vector3(0, 10, 0);
            UIObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            int StageNumber = i + 1;
            UIObject.transform.GetChild(1).GetComponent<Text>().text = StageNumber.ToString();
        }
    }

    void InitializeStageSelectManager()//시작할 때, 모든 스테이지를 비활성화 상태로 만든다.
    {
        for (int i = 0; i < StageLength; i++)
        {
            ChangeEnableStage(SwipeObjects[i], false);
        }
    }

    void InitializeData()//데이터값을 초기화한다.
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
