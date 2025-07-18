using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    //UI ������Ʈ�� �����ϴ� ��ųʸ�
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    public abstract void Init();

    private void Start()
    {
        Init();
    }

    //�ٽ� ���ε� �� �˻� �������� 
    //�������� �̸��� ������� Ư�� Ÿ�� T �� UI ��Ҹ� ���ε���.
    //where ���� �������� T�� Unity ������Ʈ���� ������.
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        //������ Ÿ�Կ� ���ǵ� ��� �̸��� ������ 
        string[] names = Enum.GetNames(type);

        //ã�� UnityEngine.Object �ν��Ͻ��� ���� �迭 ���� �� �� �迭�� Ÿ���� Ű�� �Ͽ� ��ųʸ��� �߰�
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            //Util Ŭ������ �ִ� FindChild �Լ��� ����Ͽ� ���� �̸��� ��ġ�ϴ� �ڽ� GameObject�� ã�� �ش� Ÿ���� ������Ʈ�� ������
            //recursive �� true�� ��� �ڽ� ������ ��������� �˻�
            //���� T �� GameObject�� ��� �����ε��� FindChild �̿�
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            //�ν��Ͻ��� ã�� ���� ��� �α�
            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    //������ �ε����� ����Ͽ� Ư�� Ÿ���� ���ε��� UI��� �˻�
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        //��ųʸ����� ������ Ÿ�� T������ ������Ʈ �迭�� ��������
        UnityEngine.Object[] tryGetObjects = null;
        if (_objects.TryGetValue(typeof(T), out tryGetObjects) == false)
            return null;

        //�־��� �ε����� ������Ʈ�� T Ÿ������ ĳ�����Ͽ� ��ȯ
        return tryGetObjects[idx] as T;
    }

    //���� ����ϴ� ���Ŀ� ���ؼ� Get�Լ�
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx);  }
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject gameObject, Action<PointerEventData> action, Define.UIEvent uiEventType = Define.UIEvent.Click)
    {
        UI_EventHandler evtHandler = Util.GetOrAddComponent<UI_EventHandler>(gameObject);

        switch (uiEventType)
        {
            case Define.UIEvent.Enter:
                evtHandler.OnEnterHandler -= action;
                evtHandler.OnEnterHandler += action;
                break;
            case Define.UIEvent.Exit:
                evtHandler.OnExitHandler -= action;
                evtHandler.OnExitHandler += action;
                break;
            case Define.UIEvent.Click:
                evtHandler.OnClickHandler -= action;
                evtHandler.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evtHandler.OnDragHandler -= action;
                evtHandler.OnDragHandler += action;
                break;
        }
    }
}
