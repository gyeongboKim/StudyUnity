using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : UI_Base
{
    //UI ��� �̸��� �����ϱ� ���� ��������.
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
        //�������� ������� ��ҵ��� ���ε�
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        //'ScoreText' �� ����� TextMeshProUGUI ������Ʈ�� ������ �ؽ�Ʈ ������ ����
        GetText((int)Texts.ScoreText).text = "Bind Text";
    }
    

    
    



    int _score = 0;


    public void OnButtonClicked()
    {
        ++_score;    
    }
}
