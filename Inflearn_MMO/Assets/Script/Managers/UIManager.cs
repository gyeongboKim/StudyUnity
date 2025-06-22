using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if(root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject targetGameObject, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(targetGameObject);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    //name = prefab ������ �̸�, T�� �̸� �״�� ����ϵ�(��κ��� ��� name�� T�� �̸��� ������) �ɼ����� name�� �߰��ϵ���
    public T ShowPopupUI <T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject popupGameObject = Managers.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(popupGameObject);
        _popupStack.Push(popup);

        popupGameObject.transform.SetParent(Root.transform);

        return popup;
    
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject sceneUIObject = Managers.Resource.Instantiate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(sceneUIObject);
        _sceneUI = sceneUI;

        sceneUIObject.transform.SetParent(Root.transform);

        return sceneUI;

    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;
        GameObject subItemObject = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            subItemObject.transform.SetParent(parent);
        
        return Util.GetOrAddComponent<T>(subItemObject);
    }



    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;
        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        while( _popupStack.Count > 0 ) 
            ClosePopupUI(); 
    }
}


        
