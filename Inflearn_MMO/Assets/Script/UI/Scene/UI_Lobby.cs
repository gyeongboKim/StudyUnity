using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Lobby : UI_Scene
{
    enum Buttons
    {
        StartNewGameButton,        //새 게임 버튼 (
        LoadGameButton,       //불러오기 버튼 (팝업 UI 켜짐)
        MemorialButton,    //기록 버튼(지금 까지 깬 기록들)
        AchievementButton,  //업적 버튼
        OptionButton,
        ExitGameButton,
    }
    
    enum Texts
    {
        TitleText,
        VersionText,
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.StartNewGameButton).gameObject.BindEvent(OnStartButtonClicked);
        GetButton((int)Buttons.LoadGameButton).gameObject.BindEvent(OnLoadButtonCliked);
    }


    private void OnStartButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.CharacterScene);
    }


    private void OnLoadButtonCliked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.GameScene);
    }
}
