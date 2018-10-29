using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class M_PortalEffect : MonoBehaviour {

    public float RotateSpeed;

    void Start ()
    {

		
	}
	


	void Update ()
    {
        this.transform.Rotate(0, 0, RotateSpeed);
    }
}
