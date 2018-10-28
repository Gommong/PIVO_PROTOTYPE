using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_CamTest : MonoBehaviour {

    Vector3 Pos;

    GameObject Corgi;

	void Start () {
        Pos = transform.position - Vector3.up * 3f;
        Corgi = GameObject.Find("Corgi");
    }
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, Corgi.transform.position + Pos, 1f);
    }
}
