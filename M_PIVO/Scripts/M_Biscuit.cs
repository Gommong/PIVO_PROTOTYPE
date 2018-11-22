using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Biscuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("비스킷돼라아");
    }


    private void OnTriggerEnter(Collider other)
    {
       // if(other.tag  == "Player")
       // {
            Debug.Log("비스킷 뇸뇸");
       // }
    }
}
