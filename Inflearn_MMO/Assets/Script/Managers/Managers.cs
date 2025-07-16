using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour 
{
    static Managers s_instance; //���ϼ��� ����ȴ�
    static Managers Instance { get {  Init(); return s_instance; } }  //������ �Ŵ����� ����´�. ������Ƽ ����
                                                                      //start �� ���� �� ��� Init�� ����ǰ� Ȥ�� start�� ȣ��Ǳ� ������ Instance ���� �� Init�� �����.
    #region Contetns
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game  { get { return Instance._game; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI  { get { return Instance._ui; } }
    #endregion

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

            s_instance._data.Init();    //data�� ��� clear�� ���ʿ�
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear(); 

        //���� Clear �Լ��� �߿��� Pool�� �����ϰų� �� �� �ֱ⿡ �������� ��ġ
        Pool.Clear();
    }
}
