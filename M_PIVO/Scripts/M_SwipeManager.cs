using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SwipeManager : MonoBehaviour {

    List<GameObject> SwipeObjects = new List<GameObject>();
    GameObject SwipeLine;

    Vector3 ClickPos1 = Vector3.zero;
    Vector3 ClickPos2 = Vector3.zero;

    bool MouseCheck = false;

    public float CenterScale, OtherScale;
    public float Object2Distance;



    void SwipeLineControl()
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
            MoveSwipeLine(ClickPosDistance);

            if (ClickPosDistance != 0)
            {
                ClickPos1 = ClickPos2;
            }
        }

        AutoMovingSwipeLine();
    }

    void MoveSwipeLine(float Distance)
    {
        SwipeLine.GetComponent<RectTransform>().localPosition += new Vector3(-Distance, 0, 0);
    }

    void CheckSwipeObject()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwipeObject").Length; i++)
        {
            SwipeObjects.Add(GameObject.Find("SwipeObject" + i.ToString()));
        }

        SwipeLine = GameObject.Find("SwipeObjectLine");
    }
    
    public int FindCenter()
    {
        int SwipeObjectNum = 0;
        float Distance = 1000;

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwipeObject").Length; i++)
        {            
            float SwipeObjectDistance = Mathf.Abs(500 - SwipeObjects[i].transform.position.x);
            if (Distance > SwipeObjectDistance)
            {
                Distance = SwipeObjectDistance;
                SwipeObjectNum = i;
            }
        }

        return SwipeObjectNum;
    }

    void InitializedSwipeObjectSize()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwipeObject").Length; i++)
            SwipeObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(280, 280);
    }

    void SwipeObjectSizeControl()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwipeObject").Length; i++)
        {
            if (FindCenter() == i)
            {
                SwipeObjects[i].GetComponent<RectTransform>().localScale = new Vector3(CenterScale, CenterScale, CenterScale);
            }
            else
            {
                SwipeObjects[i].GetComponent<RectTransform>().localScale = new Vector3(OtherScale, OtherScale, OtherScale);
            }
        }            
    }

    void AutoMovingSwipeLine()
    {
        float MoveSpeed = 10;

        if (!MouseCheck)
        {
            if (SwipeObjects[FindCenter()].transform.position.x < 520)
            {
                MoveSwipeLine(-MoveSpeed);
            }
            else if (SwipeObjects[FindCenter()].transform.position.x > 530)
            {
                MoveSwipeLine(MoveSpeed);
            }
        }
    }

    void SwipeObjectDistanceControl()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwipeObject").Length; i++)
        {
            SwipeObjects[i].transform.position = SwipeObjects[FindCenter()].transform.position;
            if (i < FindCenter())
            {
                float a = FindCenter() - i;
                SwipeObjects[i].transform.position = SwipeObjects[i].transform.position - new Vector3(Object2Distance*a, 0, 0);
            }
            else if (i > FindCenter())
            {
                float b = i - FindCenter();
                SwipeObjects[i].transform.position = SwipeObjects[i].transform.position + new Vector3(Object2Distance*b, 0, 0);
            }
        }
    }



    //EventFunction//

    
    
    
    void Start()
    {
        CheckSwipeObject();
        SwipeObjectDistanceControl();
        InitializedSwipeObjectSize();
    }

    void Update()
    {
        SwipeLineControl();        
        SwipeObjectSizeControl();        
    }
}