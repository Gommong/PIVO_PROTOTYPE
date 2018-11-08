using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ChangeManager : MonoBehaviour {

    public enum WorldStateEnum { Is3D, Change, Is2D };

    public GameObject UI2D, UI3D;

    [HideInInspector]
    public WorldStateEnum WorldState;

    GameObject Corgi, Corgi3D, Corgi2D;
    M_ChangeBox ChBox;
    M_Camera Cam;
    M_NavigationCorgi Navigation;

    [HideInInspector]
    public bool ChoiceRange;

    void Start () {
        Corgi = GameObject.FindGameObjectWithTag("Player");
        Corgi3D = Corgi.transform.Find("3D").gameObject;
        Corgi2D = Corgi.transform.Find("2D").gameObject;
        ChBox = GameObject.Find("ChangeViewRect").GetComponent<M_ChangeBox>();
        Cam = GameObject.Find("CameraGroup").GetComponent<M_Camera>();
        Navigation = GameObject.FindGameObjectWithTag("GameController").GetComponent<M_NavigationCorgi>();
        UI3D = GameObject.Find("ChangeBack");
        UI2D = GameObject.Find("Change");
        UI2D.SetActive(false);
        UI3D.SetActive(false);
    }

    void Update() {
    }

    public void IsChanging()
    {
        if (WorldState == WorldStateEnum.Change)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    ChoiceRange = true;
            //}

            if (Input.GetMouseButtonUp(0) && ChoiceRange == true && ChBox.IsSuccess)
            {//2D로 바뀜
                WorldState = WorldStateEnum.Is2D;
                Corgi3D.SetActive(false);
                Corgi2D.SetActive(true);
                ChoiceRange = false;
                ChBox.ChangeBoxOn();
                ChBox.ChangeBoxOff();
                Cam.ChangeCam(true);
                UI3D.SetActive(false);
                UI2D.SetActive(true);
            }
            else if(Input.GetMouseButtonUp(0) && ChoiceRange == true && !ChBox.IsSuccess)
            {//3D로 다시 돌아감
                WorldState = WorldStateEnum.Is3D;
                ChBox.ChangeBoxOff();
                ChoiceRange = false;
                UI3D.SetActive(false);
            }            
        }
    }

    public void ChangeControl()//버튼 누르면
    {
        if (WorldState == WorldStateEnum.Is3D)//3D일때
        {
            WorldState = WorldStateEnum.Change;
            StartCoroutine(ChBox.SetIncreaseSizeXY());
            Corgi.GetComponent<M_Corgi>().MovePos = Corgi.transform.position;
            Navigation.MovePos = Corgi.transform.position;
            UI3D.SetActive(true);
        }
        else if (WorldState == WorldStateEnum.Is2D)//2D일때
        {
            WorldState = WorldStateEnum.Is3D;
            Corgi.GetComponent<M_Corgi>().Is3DInit();
            Corgi3D.SetActive(true);
            Corgi2D.SetActive(false);
            Cam.ChangeCam(false);
            ChBox.CombackWorld();            
            UI2D.SetActive(false);
        }
    }
}
