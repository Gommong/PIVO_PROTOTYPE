using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ButtonControl : MonoBehaviour {

    public GameObject Pause;

	void Start () {
		
	}

    public void PauseButton()
    {
        Pause.SetActive(true);
    }

    public void PauseCancel()
    {
        Pause.SetActive(false);
    }

    public void PauseOK()
    {
        Application.LoadLevel("M_Title");
    }
}
