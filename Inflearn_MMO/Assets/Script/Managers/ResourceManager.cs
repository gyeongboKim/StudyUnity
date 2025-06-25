using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);
            GameObject pooledObject = Managers.Pool.GetOriginal(name);

            if (pooledObject != null)
                return pooledObject as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        
        
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //�Ź� instantiate ���� �ʰ� poolManager�� �����Ȱ��� Ȯ��
        // ó�� ������ ��� Ǯ ������ pop�ɰ���
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        //���� �� (Clone) ���ڿ� ����
        GameObject instantiatedObject = Object.Instantiate(original, parent);
        instantiatedObject.name = original.name;

        return instantiatedObject;
    }

    public void Destroy(GameObject objectToDestroy)
    {
        if (objectToDestroy == null)
            return;
        // ���� ���� ����ΰ��  -> poolmanager�� ��Ź(Pool�� push, SetActive �� false�� ���·� @Pool_Root�� ~~_Root�� �Ʒ��� ��)
        Poolable poolable = objectToDestroy.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(objectToDestroy);
    }
       
}
