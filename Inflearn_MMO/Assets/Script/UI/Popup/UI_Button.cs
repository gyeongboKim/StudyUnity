using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
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
    enum Images
    { 
        ItemIcon,
    }


    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        //�������� ������� ��ҵ��� ���ε�
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //'ScoreText' �� ����� TextMeshProUGUI ������Ʈ�� ������ �ؽ�Ʈ ������ ����

        //Extension �޼��� Ȱ��
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject targetGameObject = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(targetGameObject, ((PointerEventData data) => { targetGameObject.transform.position = data.position; }), Define.UIEvent.Drag);

    }






    int _score = 0;


    public void OnButtonClicked(PointerEventData data)
    {
        ++_score;
        GetText((int)Texts.ScoreText).text = $"Score : {_score}";
    }
}
