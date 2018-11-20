using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_GoalManager : MonoBehaviour {

    public GameObject ResultWidget;

    GameObject Corgi;

	void Start () {
        Corgi = GameObject.Find("Corgi");
	}
	
	void Update () {
        CheckDistance2Corgi();
    }

    void CheckDistance2Corgi()
    {
        float Dis;
        Dis = Vector3.Distance(Corgi.transform.position, transform.position);

        if (Dis < 3)
        {
            Corgi.GetComponent<M_Corgi>().ClearStage();
            ResultWidget.SetActive(true);
        }
    }
}
