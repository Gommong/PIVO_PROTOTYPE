using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ChangeBox : MonoBehaviour {

    [SerializeField]
    private Vector3 m_increaseMaxSize;

    [SerializeField]
    private float m_increaseSizePerXY;

    // 프레임당 상자가 커지는 실제 수치
    private Vector3 m_increaseSizeValueXY;
    private Vector3 StartPosition;        
    private float m_increaseSizeValueZ;

    GameObject m_outWallGroup;
    GameObject ViewEffect;
    GameObject Corgi;
    List<GameObject> others;

    Collider collider;
    M_ChangeManager ChangeScript;

    [HideInInspector]
    public bool IsSuccess = true;

    GameObject[] Tile, InteractionTile, Interaction;

    public void SetColliderEnable(bool value) { collider.enabled = value; }

    void Start () {
        collider = GetComponent<BoxCollider>();
        ViewEffect = transform.Find("ChangeViewEffect").gameObject;
        m_outWallGroup = transform.parent.Find("OutWallGroup").gameObject;
        ChangeScript = GameObject.Find("ChangeManager").GetComponent<M_ChangeManager>();
        Corgi = GameObject.Find("Corgi");
        others = new List<GameObject>();

        InitChangeViewRect();

        Tile = GameObject.FindGameObjectsWithTag("Tile");
        InteractionTile = GameObject.FindGameObjectsWithTag("InteractionTile");
    }
	
	void Update () {
        SetIncreaseSizeZ();
    }

    void SetIncreaseSizeZ()//Z범위 조절
    {
        if (ChangeScript.WorldState == M_ChangeManager.WorldStateEnum.Change)
        {
            if (Input.GetMouseButton(0))
            {
                IsSuccess = true;
                if (Input.mousePosition.x < 500f)//마우스 중앙 500,250
                {
                    m_increaseSizeValueZ = (500f - Input.mousePosition.x) * m_increaseMaxSize.z * 0.002f;
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, m_increaseSizeValueZ);
                    transform.position = StartPosition + new Vector3(0, 0, m_increaseSizeValueZ * 0.5f);
                    ViewEffect.transform.position = StartPosition + new Vector3(0, 0, m_increaseSizeValueZ);
                }
                else
                {
                    m_increaseSizeValueZ = (500f - Input.mousePosition.x) * m_increaseMaxSize.z * -0.002f;
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, m_increaseSizeValueZ);
                    transform.position = StartPosition + new Vector3(0, 0, m_increaseSizeValueZ * -0.5f);
                    ViewEffect.transform.position = StartPosition - new Vector3(0, 0, m_increaseSizeValueZ);
                }

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    if (hit.collider.gameObject.layer == 11)
                        IsSuccess = false;
                }
            }
        }
    }

    void InitChangeViewRect()//X,Y증가
    {
        // x, y 프레임당 증가수치를 구한다.
        // 총 증가수치 * 퍼센트
        float increaseSizeValueX = m_increaseMaxSize.x * m_increaseSizePerXY * 0.01f;
        float increaseSizeValueY = m_increaseMaxSize.y * m_increaseSizePerXY * 0.01f;

        m_increaseSizeValueXY = new Vector3(increaseSizeValueX, increaseSizeValueY, 0f);

        // 상자의 충돌체크 끄기
        SetColliderEnable(false);
        // 외벽 비활성화
        m_outWallGroup.SetActive(false);
    }

    public void ChangeBoxOff()//초기화
    {
        ViewEffect.SetActive(false);
        SetColliderEnable(false);
        gameObject.SetActive(false);
        for (int i = 0; i < others.Count; i++)
        {
            others[i].GetComponentInChildren<MeshRenderer>().material.SetFloat("_Choice", 0);
        }
        others.Clear();
    }

    public void ChangeBoxOn()
    {
        bool Check;
        for (int i = 0; i < Tile.Length; i++)
        {            
            Check = false;
            for (int j = 0; j < others.Count; j++)
            {
                if (others[j] == Tile[i])
                {
                    Check = true;
                    break;
                }
            }

            if (!Check)
            {
                Tile[i].SetActive(false);
            }
        }
    }

    public void CombackWorld()
    {
        for (int i = 0; i < Tile.Length; i++)
        {
            Tile[i].SetActive(true);
        }
    }

    public IEnumerator SetIncreaseSizeXY()
    {
        // 이펙트 켜기
        ViewEffect.SetActive(true);

        Vector3 startScale = Vector3.zero;
        startScale.z = 0.01f;

        transform.localScale = startScale;

        transform.position = Corgi.transform.position;
        StartPosition = transform.position;

        transform.LookAt(transform.position + new Vector3(0, 0, -100f));

        // 상자의 충돌체크 켜기
        SetColliderEnable(true);
        // 상자 활성화
        gameObject.SetActive(true);

        while (true)
        {
            // 상자의 x, y 크기를 키움
            transform.localScale += m_increaseSizeValueXY * Time.deltaTime * 0.8f;
            yield return new WaitForSeconds(0.01f);
            
            // 최대 크기만큼 커졌을 경우 반복문 종료
            if (transform.localScale.x >= m_increaseMaxSize.x)
                break;
        }        
        yield return null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {            
            others.Add(other.gameObject);
            MeshRenderer MeshR = other.GetComponentInChildren<MeshRenderer>();
            MeshR.material.SetFloat("_Choice", 2);
        }
    }

    void OnTriggerExit(Collider other)
    {
        MeshRenderer MeshR = other.GetComponentInChildren<MeshRenderer>();
            MeshR.material.SetFloat("_Choice", 0);
        
        for (int i = 0; i < others.Count; i++)
        {
            if (others[i] == other)
            {                    
                others.RemoveAt(i);
            }
        }
    }
}
