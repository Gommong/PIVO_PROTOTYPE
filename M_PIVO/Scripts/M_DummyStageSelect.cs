using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class M_DummyStageSelect : MonoBehaviour {

    public GameObject a; //변수명머라하지 선행안깬거..
    public GameObject CostUnlockStage;  //코스트없는거..

    public Text RequiredGemText;
    public Text ConsumeBiscuitText;

    public int RequiredGem = 20; //오 되네
    public int ConsumedBiscuit = 10;

    private int TotalBiscuit;
    private int TotalGem;

    public bool IsEntered = false;


    void Start ()
    {
        CostUpdate();
        CallTotalCost();
	}
	
	void Update ()
    {
        CallTotalCost();
        EnterProcess();

    }

    void CostUpdate() //스테이지 들어가는 조건 UI에 값넣어주는건데 한번만 돌면 돼서 흠
    {
        RequiredGemText.text = RequiredGem.ToString(); 
        ConsumeBiscuitText.text = ConsumedBiscuit.ToString();
    }


    void CallTotalCost()  //현재 얼마나 갖고 있는지 갱신하기
    {
        TotalBiscuit = GameObject.Find("DummyStageSelectManager").GetComponent<M_CostManager>().TotalBiscuit; 
        TotalGem = GameObject.Find("DummyStageSelectManager").GetComponent<M_CostManager>().TotalGem; 

    }

    void ReturnTotalCost()  //여기서 변한 값 CostManager에 돌려주기
    {
        GameObject.Find("DummyStageSelectManager").GetComponent<M_CostManager>().TotalBiscuit = TotalBiscuit;
        GameObject.Find("DummyStageSelectManager").GetComponent<M_CostManager>().TotalGem = TotalGem;
    }

    public void AreYouReady() //개그입니다
    {
        Debug.Log("가진 비스킷수:" +  TotalBiscuit);
        Debug.Log("가진 보석수:" + TotalGem);

        if (TotalGem >= RequiredGem && TotalBiscuit >= ConsumedBiscuit && IsEntered ==false) //지금갖고있는  보석이 필요 보석량보다 많으면 and 가진 비스킷이 충분하면
            //이거 선행 스테이지 클리어  조건도  추가해야
        {
            Debug.Log("입짱!@!@!@!");
            TotalBiscuit -= ConsumedBiscuit; //이거 CostManager에 다시 돌려줘야 하는데 음
            ReturnTotalCost(); //그래서 함수 만듬
            IsEntered = true;

        }

        else if (TotalGem <RequiredGem) //보석이 부족하면
        {
            Debug.Log("보석을 더 모아서 돌아와라 애송이");
        }

        else  if (TotalBiscuit  < ConsumedBiscuit) //지불할 비스킷이 없으면
        {
            Debug.Log("비스킷이 부족하군 애송이");
        }
        
    }
   

    void EnterProcess()
    {
        if(IsEntered)
        {
            CostUnlockStage.gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            //이거  자식 있으면 없애야
            for (int i = 0; i <= 2; i++)
            {
                CostUnlockStage.transform.GetChild(i).gameObject.SetActive(false);
            }
            

        }
    }

}
