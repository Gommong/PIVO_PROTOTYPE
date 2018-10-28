using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Camera : MonoBehaviour {

    GameObject Corgi, NavigationCorgi, Interaction;
    Animator Anim;
    M_NavigationCorgi NavigationScript;    
    M_ChangeManager ChangeManager;    
    M_WayPoint WayPoint, LevelManager;

    private Vector3 CamPos;

	void Start () {
        Corgi = GameObject.FindGameObjectWithTag("Player");
        NavigationCorgi = GameObject.FindGameObjectWithTag("GameController");
        ChangeManager = GameObject.Find("ChangeManager").GetComponent<M_ChangeManager>();
        LevelManager = GameObject.Find("LevelManager").GetComponent<M_WayPoint>();
        NavigationScript = NavigationCorgi.GetComponent<M_NavigationCorgi>();
        Anim = GetComponentInChildren<Animator>();

        CamPos = transform.position;
    }
	
	void FixedUpdate () {
        Follow();
        Touch();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.WayPointNum++;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel("M_Stage");
        }
	}

    public void ChangeCam(bool A)
    {
        if (A)
            Anim.SetTrigger("Change2D");
        else
            Anim.SetTrigger("Change3D");
    }

    void Follow()
    {
        if (ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is3D)
            transform.position = Vector3.Lerp(transform.position, Corgi.transform.position + CamPos, 0.05f);
    }

    void Touch()
    {
        if (Input.GetMouseButton(0) && ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is3D)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if (hit.collider.gameObject.layer == 9)
                    Interaction = hit.transform.gameObject;

                if (Interaction != null)
                {                    
                    if (Physics.Raycast(ray.origin, ray.direction, out hit, 1000f, 1 << LayerMask.NameToLayer("InteractionTile")))
                    {
                        Interaction.GetComponent<M_Interaction>().MovePos = hit.transform.position;
                    }
                }
                else
                {
                    if (hit.collider.tag == "Tile")
                    {
                        WayPoint = hit.collider.gameObject.GetComponentInParent<M_WayPoint>();

                        if (WayPoint.WayPointNum <= LevelManager.WayPointNum)
                        {
                            NavigationScript.NavigationInit(hit);
                        }
                    }
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if (Interaction != null)
                Interaction = null;
        }

        if (Input.GetMouseButton(0) && ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is2D)
        {
            M_Corgi CorgiScript = Corgi.GetComponent<M_Corgi>();
            Vector3 CorgiPos = Corgi.transform.position;

            Vector3 Destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float X;
            if (Destiny.x > 0)
                X = CorgiPos.x + Time.deltaTime * 15f;
            else
                X = CorgiPos.x - Time.deltaTime * 15f;

            CorgiScript.MovePos = new Vector3(X, CorgiPos.y, CorgiPos.z);
            Debug.Log(X);
            //누른 곳으로 이동하기
            //CorgiScript.MovePos = new Vector3(Destiny.x, CorgiPos.y, CorgiPos.z);

        }
    }
}
