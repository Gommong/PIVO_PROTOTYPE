using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SwipeManager : MonoBehaviour {

    List<GameObject> SwifeObjects = new List<GameObject>();
    GameObject SwifeLine;

    public Vector2 CenterSize, SecondSize, LastSize;
    public Vector3 CenterPos, SecondPos, LastPos;

    Vector3 ClickPos1 = Vector3.zero;
    Vector3 ClickPos2 = Vector3.zero;

    int CenterSwifeObjectNum = 2;
    int CompareDistance = 400;
    bool MouseCheck = false;

    void Start () {
        CheckSwifeObject();
        ChangeSizePos();
    }

    void Update()
    {
        CenterSwifeObjectControl();
        CheckCenterSwifeObject();
    }

    void CenterSwifeObjectControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickPos1 = Input.mousePosition;
            MouseCheck = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MouseCheck = false;
        }

        if (MouseCheck)
        {
            ClickPos2 = Input.mousePosition;
            float ClickPosDistance = ClickPos1.x - ClickPos2.x;
            MoveSwifeLine(ClickPosDistance);

            if (ClickPosDistance != 0)
            {
                ClickPos1 = ClickPos2;
            }
        }
    }

    void MoveSwifeLine(float Distance)
    {
        SwifeLine.GetComponent<RectTransform>().localPosition += new Vector3(-Distance, 0, 0);
    }

    void ChangeSizePos()
    {
        SwifeObjects[CenterSwifeObjectNum].GetComponent<RectTransform>().sizeDelta = CenterSize;
        CenterPos = SwifeObjects[CenterSwifeObjectNum].GetComponent<RectTransform>().localPosition;
        
        for (int i = 0; i < SwifeObjects.Count; i++)
        {
            if (i != CenterSwifeObjectNum)
            {
                SwifeObjects[i].GetComponent<RectTransform>().sizeDelta = SecondSize;
            }
        }

        for (int i = CenterSwifeObjectNum - 1; i > 0 - 1; i--)
        {
            Vector3 Pos = SwifeObjects[i + 1].GetComponent<RectTransform>().localPosition;
            if (i + 1 == CenterSwifeObjectNum)
            {
                SwifeObjects[i].GetComponent<RectTransform>().localPosition = Pos - SecondPos;
            }
            else
            {
                SwifeObjects[i].GetComponent<RectTransform>().localPosition = Pos - LastPos;
            }
        }

        for (int i = CenterSwifeObjectNum + 1; i < SwifeObjects.Count; i++)
        {
            Vector3 Pos = SwifeObjects[i - 1].GetComponent<RectTransform>().localPosition;
            if (i - 1 == CenterSwifeObjectNum)
            {
                SwifeObjects[i].GetComponent<RectTransform>().localPosition = Pos + SecondPos;
            }
            else
            {
                SwifeObjects[i].GetComponent<RectTransform>().localPosition = Pos + LastPos;
            }
        }
    }

    void CheckCenterSwifeObject()
    {
        if (SwifeLine.GetComponent<RectTransform>().localPosition.x > CompareDistance)
        {
            CompareDistance += 400;
            CenterSwifeObjectNum--;
            ChangeSizePos();
        }
        else if (SwifeLine.GetComponent<RectTransform>().localPosition.x < CompareDistance - 800)
        {
            CompareDistance -= 400;
            CenterSwifeObjectNum++;
            ChangeSizePos();
        }
    }

    void CheckSwifeObject()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwifeObject").Length; i++)
        {
            SwifeObjects.Add(GameObject.Find("SwifeObject" + i.ToString()));
        }

        SwifeLine = GameObject.Find("SwifeObjectLine");
    }
}
