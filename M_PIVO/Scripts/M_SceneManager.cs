using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_SceneManager : MonoBehaviour {

    public enum SceneName { Title, StageSelect, StagePlay};

    public SceneName Scene;

    public void SceneControl()
    {
        if (Scene == SceneName.Title)
        {
            //Application.LoadLevel("M_StageSelect");
            Application.LoadLevel("M_Stage");
        }
        else if (Scene == SceneName.StageSelect)
        {
            Application.LoadLevel("M_Stage");
        }
        else if (Scene == SceneName.StagePlay)
        {
            Application.LoadLevel("M_Title");
        }
    }
}
