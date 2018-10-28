using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_CorgiTest : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        float a;
        if (Input.GetKey(KeyCode.Q))
            a = 1;
        else if (Input.GetKey(KeyCode.E))
            a = -1;
        else
            a = 0;

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), a, Input.GetAxisRaw("Vertical")) * Time.deltaTime * 4f;
	}
}
