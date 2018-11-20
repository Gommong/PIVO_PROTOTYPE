using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SceneManager : MonoBehaviour {

    public enum SceneName { ToSelect, ToPlay, ToTitle};

    public SceneName Scene;

    public void SceneControl()
    {
        if (Scene == SceneName.ToSelect)
        {
            //Application.LoadLevel("M_StageSelect");
            Application.LoadLevel("M_Title");
        }
        else if (Scene == SceneName.ToPlay)
        {
            Application.LoadLevel("M_Stage");
        }
        else if (Scene == SceneName.ToTitle)
        {
            Application.LoadLevel("M_Title");
        }
    }
}
