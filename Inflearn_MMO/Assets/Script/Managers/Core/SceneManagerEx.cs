using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    //���� �ʿ� : FindObjectOfType ��ȿ����
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    
    //[�ɼ�] �������� ���� �� �ε��� �ʿ��� ��� ��� (LoadScene���� LoadingScene�� ������ ���� ��)
    public void LoadSceneImmediately(Define.Scene sceneType)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(sceneType));
    }

    // �񵿱� �ε��� �����ϰ� �ε� ������ �̵��ϴ� ����
    public void LoadScene(Define.Scene sceneType)
    {
        Managers.Clear();

        Managers.Data.NextSceneName = GetSceneName(sceneType);

        SceneManager.LoadScene(GetSceneName(Define.Scene.LoadingScene));
    }

    // Define.Scene string ��ȯ
    string GetSceneName(Define.Scene sceneType)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), sceneType);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
