using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene SceneType)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(SceneType));
    }
    string GetSceneName(Define.Scene SceneType)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), SceneType);
        return name;
    }
}
