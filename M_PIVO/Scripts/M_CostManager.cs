using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_CostManager : MonoBehaviour {

    //외부에서 사용할 변수이지만, 인스펙터뷰에서 관리하도록 하지 않는다면
    //[HideInInspector]를 이용해서 인스펙터뷰에서는 보이지 않도록 해주세요.
    [HideInInspector]
    public int TotalBiscuit; //스테이지 선택창에서 갖고있는 총 비스킷

    [HideInInspector]
    public int TotalGem; //보석

    [SerializeField]
    private Text BiscuitCountUI;

    [SerializeField]
    private Text GemCountUI;
    //다른 스크립트에서 사용하지 않을 변수라면 private로 해두시는게 좋습니다.
    //그래야 어떤 변수가 외부에서 사용되는지 한 눈에 파악할 수 있으니까요
    //만약 다른 스크립트에서 사용하지 않는 변수인데 인스펙터로 관리하고 싶다면
    //[SerializeField]로 만들어서 사용하시는게 좋습니다.


    void Start()
    {
        InitializeCost();
    }
    //Start와 Update는 최대한 간결하게 해야지 보기에 편합니다.
    void Update ()
    {
        CostUITextRefresh();
        CheatControl();
    }
    

    



    void InitializeCost()//초기화가 Start에 들어있던거를 하나로 묶어서 함수로 만들었습니다.
    {
        TotalBiscuit = 0; //걍 초기값 안넣어주면 오류날거같아서
        TotalGem = 0;
    }

    void CheatControl()//치트는 나중에 관리하기 편하도록 하나의 함수로 묶어두었습니다.
    {
        if (Input.GetKeyDown("x"))  //x키누르면 비스킷치트
        {
            TotalBiscuit += 5;
        }
        else if (Input.GetKeyDown("c"))  //c키누르면 보석치트
        {
            TotalGem += 5;
        }
    }

    //Update에서 사용하는 함수는 특별한 상황이 아니라면 Start에서 불러줄 필요가 없습니다.
    void CostUITextRefresh()
    {
        BiscuitCountUI.text = TotalBiscuit.ToString(); //ui에 보유량 갱신
        GemCountUI.text = TotalGem.ToString();
    }

}
