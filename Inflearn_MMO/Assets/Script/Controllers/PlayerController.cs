using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 1. ��ġ ����
// 2. ���� ����
//      1.�Ÿ� (ũ��)
//      2.���� ����
//struct MyVector
//{
//    public float x;
//    public float y;
//    public float z;

//    public float magnitude { get { return  Mathf.Sqrt(x*x + y*y + z*z); } }
//    //���⸸ ���� ũ�Ⱑ 1�� ����
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


//���� ����
// 1. �Ÿ�(ũ��) magnitude
// 2. ���� ����  normalized ũ�Ⱑ 1�� ���� ����

//���� ȸ����
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
        //������ ����
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
        //����� ũ�⸦ ���� ���� ����
        Vector3 dir = _destPos - transform.position;
        //�������� ������ ��� (float �� ���� ������ �ϱ� ������ ���������� �׻� �����Ͽ� ���� ���� ���� �̿�)
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            //nma.CalculatePath()

            //moveDest = _speed*Time.deltaTime ���� dir.magnitude�� �Ѿ���� ������ �αٿ��� ������ �߻�
            //�ذ� : Clamp -> value�� min���� ������ min����, max���� ũ�� max���� ������.(min �� max ���̰��� ��������)
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            //ũ�Ⱑ 1�� ���⺤���� �������� �ӵ�*�ð� ��ŭ ������
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position + Vector3.up * 0.7f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.7f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            //_destPos ������ �ٶ�
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }
        //�ִϸ��̼� ó��
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);

    }
    void UpdateDie()
    {
        //HP ���� �߰� �� �ۼ�
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
    // Ű���� �̵�
    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //        //transform.rotation = Quaternion.LookRotation(Vector3.forward); //���� ����
    //        //transform.Translate�� �� �� Ŀ�긦 �׸��� �̵�

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

        //���콺 Ŭ�� ������ ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        //LayerMask mask = LayerMask.GetMask("Wall");
        
        RaycastHit hit;
        //wall
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))    
        {
            //Ŭ���� �������� �̵�
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
