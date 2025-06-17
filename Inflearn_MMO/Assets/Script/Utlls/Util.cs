using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util 
{
    public static GameObject FindChild(GameObject gameObject, string name = null, bool recursive = false) 
    {
        Transform transform = FindChild<Transform>(gameObject, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject gameObject, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        //�θ� ������Ʈ�� ����ִ� ��� null ����
        if (gameObject == null)
            return null;

        //���� �ڽĸ� ã�� ���
        if(recursive == false)
        {
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                Transform transform = gameObject.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null)
                        return component;
                }
            }
            
        }//��������� ã�� ����
        else
        {
            foreach (T component in gameObject.GetComponentsInChildren<T>())
            {
                //�̸��� �Է����� ���� ��쿡�� T Ÿ���� ã���� ����
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
     
}
