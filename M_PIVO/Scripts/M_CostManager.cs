using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_CostManager : MonoBehaviour {

    public int TotalBiscuit; //스테이지 선택창에서 갖고있는 총 비스킷
    public int TotalGem; // " 보석

    public Text BiscuitCountUI;
    public Text GemCountUI;


    void Start()
    {
        TotalBiscuit = 0; //걍 초기값 안넣어주면 오류날거같아서
        TotalGem = 0;

        CostUITextRefresh();

    }

    
  

	void Update ()
    {

        CostUITextRefresh();

        if (Input.GetKeyDown("x"))  //x키누르면 비스킷치트
        {
            BiscuitCheat();

        }

        if (Input.GetKeyDown("c"))  //c키누르면 보석치트
        {
            GemCheat();

        }


    }


    void CostUITextRefresh()
    {
        BiscuitCountUI.text = TotalBiscuit.ToString(); //ui에 보유량 갱신
        GemCountUI.text = TotalGem.ToString(); 
    }

    void BiscuitCheat() //치트쨩!!!
    {
        TotalBiscuit += 5;
    }

    void GemCheat() //치트쨩!!!
    {
        TotalGem += 5;
    }

}
