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
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI  { get { return Instance._ui; } }

    void Start()
    {
        Init();
    }

    
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        //�ʱ�ȭ
        if(s_instance == null)
        {
            GameObject managersRootObject = GameObject.Find("@Managers");
            if (managersRootObject == null)
            {
                managersRootObject = new GameObject { name = "@Managers" };
                managersRootObject.AddComponent<Managers>();
            }

            DontDestroyOnLoad(managersRootObject);
            s_instance = managersRootObject.GetComponent<Managers>();
        }
    }
}
