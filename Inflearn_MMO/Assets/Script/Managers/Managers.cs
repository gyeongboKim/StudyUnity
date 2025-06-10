using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour 
{
    static Managers s_instance; //유일성이 보장된다
    static Managers Instance { get { Init(); return s_instance; } }  //유일한 매니저를 갖고온다. 프로퍼티 형식
    // Start is called before the first frame update
    //start 가 시작 된 경우 Init이 실행되고 혹은 start가 호출되기 이전에 Instance 생성 시 Init이 실행됨.

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        //초기화
        if(s_instance == null)
        {
            GameObject gameobject = GameObject.Find("@Managers");
            if (gameobject == null)
            {
                gameobject = new GameObject { name = "@Managers" };
                gameobject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(gameobject);
            s_instance = gameobject.GetComponent<Managers>();
        }
    }
}
