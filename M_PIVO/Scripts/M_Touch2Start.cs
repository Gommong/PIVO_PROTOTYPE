using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M_Touch2Start : MonoBehaviour {

    [SerializeField]
    private float Speed;

    private float LerpValue;
    private bool LerpValueControl;

    private Color TextColor;

	void Start () {
    }
	
	void Update () {

        if (LerpValueControl)
            LerpValue -= Time.deltaTime * Speed;
        else
            LerpValue += Time.deltaTime * Speed;

        if (LerpValue > 1)
            LerpValueControl = true;
        else if (LerpValue < 0)
            LerpValueControl = false;
        
        TextColor = Color.Lerp(Color.white, Color.black, LerpValue);
        GetComponent<Text>().color = TextColor;
    }
}
