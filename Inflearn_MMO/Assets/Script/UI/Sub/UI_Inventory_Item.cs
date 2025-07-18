using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Inventory_Item : UI_Base
{
    enum GameObjects
    { 
        ItemIcon,
        ItemNameTMP,
    }

    string _itemName;

    // Init()�� ���ε� �� �̺�Ʈ ��ϸ� ����ϰ� ��.
    public override void Init()
    {

        Bind<GameObject>(typeof(GameObjects)); 

        // ������ Ŭ�� �̺�Ʈ�� ���� �޼���� ���ε�
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent(OnItemClicked, Define.UIEvent.Click);

        // ���콺 �����Ͱ� ������ ������ ���� �������� �� ȣ��Ǵ� �̺�Ʈ ���ε�
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent(OnPointerEnterItemIcon, Define.UIEvent.Enter);

        // ���콺 �����Ͱ� ������ �����ܿ��� ����� �� ȣ��Ǵ� �̺�Ʈ ���ε�
        Get<GameObject>((int)GameObjects.ItemIcon).BindEvent(OnPointerExitItemIcon, Define.UIEvent.Exit);

        //GetText((int)GameObjects.ItemNameTMP).text = "Bind"; //GetText�� ��� �Ұ� : Bind�� GameObject�� �߱� ����
    }

    public void SetInfo(string name)
    {
        _itemName = name;

        if (Get<GameObject>((int)GameObjects.ItemNameTMP) != null)
        {
            Get<GameObject>((int)GameObjects.ItemNameTMP).GetComponent<TextMeshProUGUI>().text = _itemName;
        }
        else
        {
            Debug.LogWarning("ItemNameTMP�� ���� ���ε����� ����. Init()�� ���� ȣ��Ǿ����� Ȯ��.");
        }
    }

    private void OnItemClicked(PointerEventData eventData)
    {
        Debug.Log($"������ Ŭ��! �̸�: {_itemName}");
        // ���⿡ ������ ���, ���� â ���� ���� ���� �ֱ�
        // ��: Managers.Inventory.UseItem(_itemName);
    }

    // ���콺 �����Ͱ� ������ ������ ���� �������� �� ȣ��� �޼���
    private void OnPointerEnterItemIcon(PointerEventData eventData)
    {
        Debug.Log($"������ ������ ���� ���콺 ����: {_itemName}");
        // ���� Ȱ��ȭ, ���̶���Ʈ ȿ�� �� �ֱ�
        // ��: Managers.UI.ShowItemTooltip(this, _itemName); // 'this'�� �Ѱ� ��ġ�� ������ ���� �ִ�.
    }

    // ���콺 �����Ͱ� ������ �����ܿ��� ����� �� ȣ��� �޼���
    private void OnPointerExitItemIcon(PointerEventData eventData)
    {
        Debug.Log($"������ �����ܿ��� ���콺 ��Ż: {_itemName}");
        // ���� ��Ȱ��ȭ, ���̶���Ʈ ȿ�� ���� �� �ֱ�
        // ��: Managers.UI.HideItemTooltip();
    }
}
