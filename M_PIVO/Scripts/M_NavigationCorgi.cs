using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_NavigationCorgi : MonoBehaviour {

    [HideInInspector]
    public Vector3 MovePos;

    [HideInInspector]
    public Vector3 NextMove;

    [HideInInspector]
    public int CurWayPointNum;

    public float MoveSpeed;

    GameObject Corgi;
    M_Corgi CorgiScript;

    bool bNavigation;
    bool IsCorgiMove;
    bool CanGo = true;    

	void Start () {
        Corgi = GameObject.FindGameObjectWithTag("Player");
        CorgiScript = Corgi.GetComponent<M_Corgi>();
        bNavigation = true;
        IsCorgiMove = false;
        MovePos = transform.position;
        NextMove = transform.position;
    }
	
	void Update () {
        NavigationMove();
    }

    void NavigationMove()
    {
        if (bNavigation)
        {
            Navigation();            
        }

        if (transform.position != NextMove && transform.position != MovePos)
        {
            transform.position = NextMove;
            CorgiScript.MovePosArray.Add(NextMove);
        }
        else if (transform.position != MovePos)
        {
            bNavigation = true;
        }

        if (!CanGo)
        {
            RaycastHit hit;

            Physics.Raycast(Corgi.transform.position, Vector3.down, out hit, 5f);            
            MovePos = hit.transform.position + new Vector3(0, 2.5f, 0);
            NextMove = hit.transform.position + new Vector3(0, 2.5f, 0);
            transform.position = hit.transform.position + new Vector3(0, 2.5f, 0);
            CorgiScript.LastMovePos = hit.transform.position + new Vector3(0, 2.5f, 0);
            Corgi.transform.position = hit.transform.position + new Vector3(0, 2.5f, 0);

            CanGo = true;
            CorgiScript.MovePosArray.Clear();
            CorgiScript.ArrayNum = 0;
        }
    }

    void Navigation()
    {
        bNavigation = false;
        float Distance = 999f;
        RaycastHit hit = new RaycastHit { };
        Vector3 CompareVector = transform.position - new Vector3(0, -2.5f, 0);

        Vector3 VLeft = Vector3.zero,
            VRight = Vector3.zero,
            VForward = Vector3.zero,
            VBackward = Vector3.zero;

        for (int Direction = 0; Direction < 4; Direction++)
        {
            if (Direction == 0 && Physics.Raycast(transform.position + new Vector3(-2, 10, 0), Vector3.down, out hit))
            {
                if (hit.collider.tag == "Tile")
                    CompareVector = hit.collider.transform.position;
            }
            if (Direction == 1 && Physics.Raycast(transform.position + new Vector3(2, 10, 0), Vector3.down, out hit))
            {
                if (hit.collider.tag == "Tile")
                    CompareVector = hit.collider.transform.position;
            }
            if (Direction == 2 && Physics.Raycast(transform.position + new Vector3(0, 10, 2), Vector3.down, out hit))
            {
                if (hit.collider.tag == "Tile")
                    CompareVector = hit.collider.transform.position;
            }
            if (Direction == 3 && Physics.Raycast(transform.position + new Vector3(0, 10, -2), Vector3.down, out hit))
            {
                if (hit.collider.tag == "Tile")
                    CompareVector = hit.collider.transform.position;
            }

            for (int i = 0; i < CorgiScript.MovePosArray.Count; i++)
            {
                if (new Vector3(CompareVector.x, CompareVector.y + 2.5f, CompareVector.z) == CorgiScript.MovePosArray[i])
                    CompareVector = new Vector3(999, 999, 999);

                if (CompareVector.y + 2.5f - transform.position.y > 3f)
                    CompareVector = new Vector3(999, 999, 999);
            }
            
            if (Distance > Vector3.Distance(MovePos, CompareVector))
            {
                Distance = Vector3.Distance(MovePos, CompareVector);
                NextMove = new Vector3(CompareVector.x, CompareVector.y + 2.5f, CompareVector.z);
            }

            if (Direction == 3 && NextMove.y - transform.position.y > 3f || NextMove.y > 900f)
            {
                CanGo = false;
            }
        }
    }

    public void NavigationInit(RaycastHit hit)
    {
        MovePos = hit.transform.position + new Vector3(0, 2.5f, 0);
        CorgiScript.LastMovePos = hit.transform.position + new Vector3(0, 2.5f, 0);
        CorgiScript.MovePosArray.Clear();
        transform.position = Corgi.transform.position;
        NextMove = transform.position;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f))
        {
            CorgiScript.MovePosArray.Add(hit.transform.position);
        }
    }
}
