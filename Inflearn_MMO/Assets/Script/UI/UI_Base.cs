using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    //UI 오브젝트를 저장하는 딕셔너리
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    //핵심 바인딩 및 검색 로직으로 
    //열거형의 이름을 기반으로 특정 타입 T 의 UI 요소를 바인딩함.
    //where 제약 조건으로 T가 Unity 오브젝트임을 보장함.
    public void Bind<T>(Type type) where T : UnityEngine.Object
    {
        //열거형 타입에 정의된 모든 이름을 가져옴 
        string[] names = Enum.GetNames(type);

        //찾을 UnityEngine.Object 인스턴스를 담을 배열 생성 후 그 배열과 타입을 키로 하여 딕셔너리에 추가
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            //Util 클래스에 있는 FindChild 함수를 사용하여 현재 이름과 일치하는 자식 GameObject를 찾고 해당 타입의 컴포넌트를 가져옴
            //recursive 가 true인 경우 자식 내에서 재귀적으로 검색
            //만약 T 가 GameObject인 경우 오버로드한 FindChild 이용
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            //인스턴스를 찾지 못한 경우 로그
            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    public TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    public Button GetButton(int idx) { return Get<Button>(idx); }
    public Image GetImage(int idx) { return Get<Image>(idx); }

    //열거형 인덱스를 사용하여 특정 타입의 바인딩된 UI요소 검색
    public T Get<T>(int idx) where T : UnityEngine.Object
    {
        //딕셔너리에서 지저된 타입 T에대한 오브젝트 배열을 가져오기
        UnityEngine.Object[] tryGetObjects = null;
        if (_objects.TryGetValue(typeof(T), out tryGetObjects) == false)
            return null;

        //주어진 인덱스의 오브젝트를 T 타입으로 캐스팅하여 반환
        return tryGetObjects[idx] as T;
    }
}
