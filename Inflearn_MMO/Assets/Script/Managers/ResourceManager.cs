using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject instantiatedObject = Object.Instantiate(prefab, parent);

        //생성 시 (Clone) 문자열 제거
        int index = instantiatedObject.name.IndexOf("(Clone)");
        if (index > 0)
            instantiatedObject.name = instantiatedObject.name.Substring(0, index);

        return instantiatedObject;
    }

    public void Destroy(GameObject objectToDestroy)
    {
        if (objectToDestroy == null)
            return;
        Object.Destroy(objectToDestroy);
    }
       
}
