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
        
        //original이미 있으면 바로 사용
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //매번 instantiate 하지 않고 poolManager에 폴링된건지 확인
        //생성 후 (Clone) 문자열 제거
        GameObject instantiatedObject = Object.Instantiate(prefab, parent);
        instantiatedObject.name = prefab.name;
        
        

        return instantiatedObject;
    }

    public void Destroy(GameObject objectToDestroy)
    {
        if (objectToDestroy == null)
            return;
        // 만약 폴링이 필요한경우 -> poolmanager에 위탁

        Object.Destroy(objectToDestroy);
    }
       
}
