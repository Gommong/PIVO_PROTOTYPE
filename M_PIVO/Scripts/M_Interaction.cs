using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Interaction : MonoBehaviour {

    [HideInInspector]
    public Vector3 MovePos;

    private RaycastHit UpTile;

    void Start () {
        MovePos = transform.position;
        Physics.Raycast(transform.position, Vector3.up, out UpTile, 5f);
    }
	
	void Update () {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(MovePos.x, transform.position.y, MovePos.z), 10);
        UpTile.transform.position = transform.position + Vector3.up * 2f;
    }
}
