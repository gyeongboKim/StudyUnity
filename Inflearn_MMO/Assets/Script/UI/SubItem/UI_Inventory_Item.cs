using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Inventory_Item : UI_Base
{
    enum GameObjects
    { 
        ItemIcon,
        ItemNameTMP,
    }

    string _name;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.ItemNameTMP).GetComponent<TextMeshProUGUI>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭!{_name}"); });

        //GetText((int)GameObjects.ItemNameTMP).text = "Bind"; //GetText로 사용 불가 : Bind를 GameObject로 했기 때문
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
