using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons
    {
        PointButton,
    }
    enum Texts
    { 
        PointText,
        ScoreText,
    }
    

    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
    }
    

    //Util.FindChild와 마찬가지로 T에 대한 조건을 똑같이 넣어줄것.
    void Bind<T> (Type type) where T : UnityEngine.Object
    {
        string [] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }



    int _score = 0;
    public void OnButtonClicked()
    {
        
        ++_score;
        
    }
}
