using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.LobbyScene;
        Debug.Log($"SceneType �ʱ�ȭ �Ϸ� : {SceneType}");

        Managers.UI.ShowSceneUI<UI_Lobby>();

        //UI ��ư�� �̺�Ʈ ����
        //Managers.UI.
        
    }

    private void Update()
    {
        //�� ��ȯ �ǽ�
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadSceneImmediately(Define.Scene.GameScene);
        }
    }

    public override void Clear()
    {
        Debug.Log("LobbyScene Clear!");
        //TODO
    }
    

}
