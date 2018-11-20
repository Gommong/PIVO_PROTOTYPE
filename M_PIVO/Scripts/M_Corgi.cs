using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Corgi : MonoBehaviour {

    [HideInInspector]
    public Vector3 MovePos, LastMovePos;

    [HideInInspector]
    public List<Vector3> MovePosArray;

    [HideInInspector]
    public bool IsMove = false;

    [HideInInspector]
    public int ArrayNum;

    [HideInInspector]
    public GameObject Corgi3D, Corgi2D;
    
    public float MoveSpeed;
    private float MoveDefault;

    public float UpSpeed, BeforeY;
    private int IsUp;//1_올라가기 //0_내려가기 //-1_끄기

    Animator AnimState;
    M_ChangeManager ChangeManager;
    M_Camera CameraScript;

	void Start () {
        MoveDefault = MoveSpeed;
        MovePos = transform.position;
        Corgi3D = transform.Find("3D").gameObject;
        Corgi2D = transform.Find("2D").gameObject;

        AnimState = GetComponentInChildren<Animator>();
        ChangeManager = GameObject.Find("ChangeManager").GetComponent<M_ChangeManager>();
        CameraScript = GameObject.Find("CameraGroup").GetComponent<M_Camera>();
    }

    void Update()
    {
        Move();
    }

    public void Ray3D()
    {
        if (MovePosArray.Count > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f) && MoveSpeed != 0)
            {
                MoveSpeed = 0;
                BeforeY = transform.position.y;
                AnimState.SetBool("IsClimb", true);
                IsUp = 1;
            }
            else if (!Physics.Raycast(transform.position + transform.forward - Vector3.down * 2f, Vector3.down, 4f))
            {
                if (!Physics.Raycast(transform.position - Vector3.down * 2f, Vector3.down, 4f) && MoveSpeed != 0)
                {
                    MoveSpeed = 0;
                    BeforeY = transform.position.y;
                    AnimState.SetBool("IsFalling", true);
                    IsUp = 0;
                }
            }
        }

        if (MoveSpeed == 0 && IsUp == 1)
        {
            transform.position += Vector3.up * Time.deltaTime * UpSpeed;

            if (transform.position.y >= BeforeY + 2f)
            {
                transform.position = new Vector3(transform.position.x, BeforeY + 2, transform.position.z);
                BeforeY = transform.position.y;
                MoveSpeed = MoveDefault;
                AnimState.SetBool("IsClimb", false);
            }
        }
        else if (MoveSpeed == 0 && IsUp == 0)
        {
            transform.position += Vector3.down * Time.deltaTime * UpSpeed * 4f;
            
            if (transform.position.y <= BeforeY - 2f)
            {
                transform.position = new Vector3(transform.position.x, BeforeY - 2f, transform.position.z);
                MoveSpeed = MoveDefault;
                AnimState.SetBool("IsFalling", false);
            }
        }
    }

    void Ray2D()
    {
        Vector3 RayPos = Vector3.up * 5f + Vector3.right * Corgi2D.transform.localScale.x * 0.5f;
        
        if (Physics2D.Raycast(Corgi2D.transform.position + RayPos, Vector2.down))
        {
            IsUp = 1;
            RaycastHit2D hit = Physics2D.Raycast(Corgi2D.transform.position + RayPos, Vector2.down);

            if (IsMove == true)
            {
                if (hit.transform.position.y + 2f > transform.position.y && hit.transform.position.y < transform.position.y)
                {
                    MoveSpeed = 0;
                    IsUp = 1;
                }
                else if (transform.position.y - hit.transform.position.y > 2.7f)
                {
                    Vector3 DownRay = Corgi2D.transform.position - Vector3.right * Corgi2D.transform.localScale.x * 1.2f - Vector3.up * 1.2f;
                    if (!Physics2D.Raycast(DownRay, Vector3.down, 0.5f))
                    {
                        MoveSpeed = 0;
                        IsUp = 0;
                    }
                }
                else if (transform.position.y - hit.transform.position.y >= 2.5f)
                {
                    MoveSpeed = MoveDefault;
                    transform.position = new Vector3(transform.position.x, hit.transform.position.y + 2.5f, transform.position.z);
                }
                else if (hit.transform.position.y > transform.position.y)
                {
                    MoveSpeed = 0;
                    IsUp = -1;
                }

                if (MoveSpeed == 0 && IsUp == 1)
                {
                    transform.position += Vector3.up * Time.deltaTime * UpSpeed;
                }
                else if (MoveSpeed == 0 && IsUp == 0)
                {
                    transform.position += Vector3.down * Time.deltaTime * UpSpeed * 4f;
                }
            }
            else
            {
                MoveSpeed = MoveDefault;
                if (!Physics2D.Raycast(transform.position + new Vector3(Corgi2D.transform.localScale.x * 1.2f, -3f, 0f), Vector3.down))
                {
                    MoveSpeed = 0;
                }
            }
        }
        else
        {            
            MoveSpeed = 0;
            IsUp = -1;
        }
    }

    public void Is3DInit()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);

        transform.position = hit.transform.position + Vector3.up * 2.5f;
        MoveSpeed = MoveDefault;
        MovePosArray.Clear();
        ArrayNum = 0;
        GameObject Navigation = GameObject.Find("NavigationCorgi");
        Navigation.transform.position = transform.position;

        M_NavigationCorgi NavigationScript = Navigation.GetComponent<M_NavigationCorgi>();
        NavigationScript.NextMove = transform.position;
        NavigationScript.MovePos = transform.position;
        LastMovePos = transform.position;
    }

    public void Move()
    {
        if (ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is3D)
        {
            Ray3D();
            if (MovePosArray.Count > 0)
            {                
                Vector3 CorgiMovePos = new Vector3(MovePosArray[ArrayNum].x, transform.position.y, MovePosArray[ArrayNum].z);
                transform.position = Vector3.MoveTowards(transform.position, CorgiMovePos, MoveSpeed);
                AnimState.SetBool("IsChange", false);

                if (transform.position == CorgiMovePos && ArrayNum < MovePosArray.Count)
                {
                    ArrayNum++;
                }
                if (ArrayNum == MovePosArray.Count)
                {
                    ArrayNum = 0;
                    MovePosArray.Clear();
                }
            }

            if (new Vector3(LastMovePos.x, transform.position.y, LastMovePos.z) != transform.position)
            {
                AnimState.SetBool("IsMoving", true);
                transform.LookAt(new Vector3(MovePosArray[ArrayNum].x, transform.position.y, MovePosArray[ArrayNum].z));
            }
            else
            {
                AnimState.SetBool("IsMoving", false);
            }
        }
        else if (ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Change)
        {
            AnimState.SetBool("IsChange", true);
        }
        else if (ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is2D)
        {
            Ray2D();
            transform.rotation = Quaternion.Euler(0, 2.5f, 0);

            Vector3 Destiny = new Vector3(MovePos.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, Destiny, MoveSpeed * 1.5f);

            if (transform.position.x < MovePos.x)
                Corgi2D.transform.localScale = new Vector3(1, 1, 1);
            else if (transform.position.x > MovePos.x)
                Corgi2D.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void ClearStage()
    {
        if (ChangeManager.WorldState == M_ChangeManager.WorldStateEnum.Is3D)
        {
            Is3DInit();
            CameraScript.LevelSequence();
        }
        else
        {
            MovePos = transform.position;
        }
    }

}
