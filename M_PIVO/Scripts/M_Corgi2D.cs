using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Corgi2D : MonoBehaviour {

    public List<GameObject> CollisionList;
    M_Corgi CorgiScript;

	void Start () {
        CorgiScript = transform.parent.GetComponent<M_Corgi>();
    }

    private void Update()
    {
        if (CollisionList != null)
        {
            if (CollisionList.Count == 0)
                CorgiScript.IsMove = true;
            else
                CorgiScript.IsMove = false;
        }
        else
            CorgiScript.IsMove = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        CollisionList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < CollisionList.Count; i++)
        {
            if (CollisionList[i] == collision.gameObject)
                CollisionList.RemoveAt(i);
        }        
    }
}
