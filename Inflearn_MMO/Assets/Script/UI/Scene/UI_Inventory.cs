using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }
        void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        //�κ��丮 �ʱ�ȭ
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        //���� �κ��丮 ������ �����ؼ� �Ѱ��ֱ�
        for (int i = 0; i < 11; i++)
        {

            //GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Inventory_Item");    <-ver1
            GameObject item = Managers.UI.MakeSubItem<UI_Inventory_Item>(parent: gridPanel.transform).gameObject;
            UI_Inventory_Item inventoryItem = item.GetOrAddComponent<UI_Inventory_Item>();
            inventoryItem.SetInfo($"Bow{i}");
        }
    }
}

