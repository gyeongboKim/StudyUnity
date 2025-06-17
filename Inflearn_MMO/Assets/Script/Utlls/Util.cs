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
        //부모 오브젝트가 비어있는 경우 null 리턴
        if (gameObject == null)
            return null;

        //직속 자식만 찾는 경우
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
            
        }//재귀적으로 찾는 버전
        else
        {
            foreach (T component in gameObject.GetComponentsInChildren<T>())
            {
                //이름을 입력하지 않은 경우에도 T 타입을 찾으면 리턴
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
     
}
