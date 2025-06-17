using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UI_Base
{
    //UI 요소 이름을 정리하기 위한 열거형들.
    enum Buttons
    {
        PointButton,
    }
    enum Texts
    { 
        PointText,
        ScoreText,
    }
    enum GameObjects
    { 
        TestObject,
    }

    private void Start()
    {
        //열거형을 기반으로 요소들을 바인딩
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        //'ScoreText' 와 연결된 TextMeshProUGUI 컴포넌트를 가져와 텍스트 내용을 설정
        GetText((int)Texts.ScoreText).text = "Bind Text";
    }
    

    
    



    int _score = 0;


    public void OnButtonClicked()
    {
        ++_score;    
    }
}
