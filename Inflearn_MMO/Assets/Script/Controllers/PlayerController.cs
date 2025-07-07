using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 1. 위치 벡터
// 2. 방향 벡터
//      1.거리 (크기)
//      2.실제 방향
//struct MyVector
//{
//    public float x;
//    public float y;
//    public float z;

//    public float magnitude { get { return  Mathf.Sqrt(x*x + y*y + z*z); } }
//    //방향만 갖는 크기가 1인 벡터
//    public MyVector normalized { get { return new MyVector(x/magnitude, y/magnitude, z/magnitude); } }

//    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }

//    public static MyVector operator +(MyVector a, MyVector b)
//    {
//        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
//    }
//    public static MyVector operator -(MyVector a, MyVector b)
//    {
//        return new MyVector(a.x +- b.x, a.y - b.y, a.z - b.z);
//    }
//    public static MyVector operator *(MyVector a, float d)
//    {
//        return new MyVector(a.x * d, a.y * d, a.z * d);
//    }


//방향 벡터
// 1. 거리(크기) magnitude
// 2. 실제 방향  normalized 크기가 1인 방향 벡터

//절대 회전값
//transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

// +- delta
//transform.Rotate(new Vector3(0.0f, Time.deltaTime* _speed, 0.0f));

//Quaternion
//transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));


//transform.rotation

public class PlayerController : MonoBehaviour
{
    PlayerStat _stat;
    Vector3 _destPos;

    Texture2D attackCursor;
    Texture2D moveCursor;

    enum CursorType
    {
        None,
        Attack,
        Move,
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        attackCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Attack");
        moveCursor = Managers.Resource.Load<Texture2D>("Textures/Cursor/Cursor_Move");
        _stat = gameObject.GetComponent<PlayerStat>();
        //옵저버 패턴
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    

    public enum PlayerState
    {
        Idle,
        Die,
        Moving,
        Skill,      //atack, buff, 
    }

    PlayerState _state = PlayerState.Idle;

  
    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);

    }
    void UpdateMoving()
    {
        //방향과 크기를 갖는 벡터 추출
        Vector3 dir = _destPos - transform.position;
        //목적지에 도달한 경우 (float 두 값을 뺄셈을 하기 때문에 오차범위가 항상 존재하여 아주 작은 값을 이용)
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            //nma.CalculatePath()

            //moveDest = _speed*Time.deltaTime 값이 dir.magnitude를 넘어버려 목적지 부근에서 버벅임 발생
            //해결 : Clamp -> value가 min보다 작으면 min값을, max보다 크면 max값을 덮어줌.(min 과 max 사이값을 보장해줌)
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //크기가 1인 방향벡터의 방향으로 속도*시간 만큼 움직임
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.7f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.7f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //_destPos 방향을 바라봄
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }
        //애니메이션 처리
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);

    }
    void UpdateDie()
    {
        //HP 개념 추가 시 작성
    }
    
    void Update()
    {
        UpdateMouseCursor();
        switch(_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
        }


    }

    void UpdateMouseCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        //LayerMask mask = LayerMask.GetMask("Wall");

        RaycastHit hit;
        //wall
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if(_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(attackCursor, new Vector2(attackCursor.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }

            }
            else
            {
                if(_cursorType != CursorType.Move)
                {
                    Cursor.SetCursor(moveCursor, new Vector2(attackCursor.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Move; 
                }
                
            }
        }

    }
    // 키보드 이동
    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //        //transform.rotation = Quaternion.LookRotation(Vector3.forward); //월드 기준
    //        //transform.Translate로 할 시 커브를 그리며 이동

    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;
    //    }

    //    _moveToDest = false;
    //}
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    void OnMouseClicked(Define.MouseEvent mouseEvent)
    {
        if (_state == PlayerState.Die)
            return;

        //마우스 클릭 지점에 ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        //LayerMask mask = LayerMask.GetMask("Wall");
        
        RaycastHit hit;
        //wall
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))    
        {
            //클릭한 지점까지 이동
            _destPos = hit.point;
            _state = PlayerState.Moving;

            if(hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Clicked!!");
            }
            else
            {
                Debug.Log("Ground Clicked!!");
            }
        }
    }

}
