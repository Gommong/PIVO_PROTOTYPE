using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SwipeManager : MonoBehaviour {

    List<GameObject> SwifeObjects = new List<GameObject>();

    public Vector2 CenterSize, SecondSize, LastSize;
    public Vector3 CenterPos, SecondPos, LastPos;

    int CenterSwifeObject;

	void Start () {
        CheckSwifeObject();
        CheckCenterSwifeObject();
        ChangeSizePos();
    }

    void Update()
    {
        
    }

    void ChangeSizePos()
    {
        SwifeObjects[CenterSwifeObject].GetComponent<RectTransform>().localPosition = CenterPos;
        SwifeObjects[CenterSwifeObject].GetComponent<RectTransform>().sizeDelta = CenterSize;
        
        SwifeObjects[CenterSwifeObject - 1].GetComponent<RectTransform>().localPosition = -SecondPos;
        SwifeObjects[CenterSwifeObject - 1].GetComponent<RectTransform>().sizeDelta = SecondSize;

        SwifeObjects[CenterSwifeObject + 1].GetComponent<RectTransform>().localPosition = SecondPos;
        SwifeObjects[CenterSwifeObject + 1].GetComponent<RectTransform>().sizeDelta = SecondSize;

        SwifeObjects[CenterSwifeObject - 2].GetComponent<RectTransform>().localPosition = -LastPos;
        SwifeObjects[CenterSwifeObject - 2].GetComponent<RectTransform>().sizeDelta = LastSize;

        SwifeObjects[CenterSwifeObject + 2].GetComponent<RectTransform>().localPosition = LastPos;
        SwifeObjects[CenterSwifeObject + 2].GetComponent<RectTransform>().sizeDelta = LastSize;
    }

    void CheckCenterSwifeObject()
    {
        CenterSwifeObject = 2;
    }

    void CheckSwifeObject()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("SwifeObject").Length; i++)
        {
            SwifeObjects.Add(GameObject.FindGameObjectsWithTag("SwifeObject")[i]);
        }
    }
}
