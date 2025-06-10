using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour 
{
    static Managers s_instance; //���ϼ��� ����ȴ�
    static Managers Instance { get { Init(); return s_instance; } }  //������ �Ŵ����� ����´�. ������Ƽ ����
    // Start is called before the first frame update
    //start �� ���� �� ��� Init�� ����ǰ� Ȥ�� start�� ȣ��Ǳ� ������ Instance ���� �� Init�� �����.

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
        //�ʱ�ȭ
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
