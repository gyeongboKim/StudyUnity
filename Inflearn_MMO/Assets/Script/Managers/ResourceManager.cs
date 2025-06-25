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
        
        //original�̹� ������ �ٷ� ���
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if(prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //�Ź� instantiate ���� �ʰ� poolManager�� �����Ȱ��� Ȯ��
        //���� �� (Clone) ���ڿ� ����
        GameObject instantiatedObject = Object.Instantiate(prefab, parent);
        instantiatedObject.name = prefab.name;
        
        

        return instantiatedObject;
    }

    public void Destroy(GameObject objectToDestroy)
    {
        if (objectToDestroy == null)
            return;
        // ���� ������ �ʿ��Ѱ�� -> poolmanager�� ��Ź

        Object.Destroy(objectToDestroy);
    }
       
}
