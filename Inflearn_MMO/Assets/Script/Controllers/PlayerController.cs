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

public class PlayerController : BaseController
{
    //���� ������ ����� ���
    //�ڽ��� �÷��� �ϴ� �÷��̾�����, �ٸ� �÷��̾����� �б��� �ʿ�
    PlayerStat _stat;

    bool _stopSkill = false;

    float _attackRange = 2;

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    public override void Init()
    {
        //������ ����
        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        //HP Bar üũ �� �߰�
        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);


        
    }

    //protected override void UpdateIdle()
    //{


    //}

    protected override void UpdateMoving()
    {
        if (State != Define.State.Moving)
            return;
        //���µ� ���Ͱ� �� �����Ÿ����� ������ ���� ���·� ����
        //Ÿ�� üũ
        if(_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distanceToLockTarget = (_destPos - transform.position).magnitude;

            if(distanceToLockTarget <=  _attackRange )
            {
                State = Define.State.Skill;
                return;
            }
        }

        //�̵�
        //����� ũ�⸦ ���� ���� ����
        Vector3 dir = _destPos - transform.position;
        dir.y = 0;          //���� ���� �ö󰡴� ���� ����
        //�������� ������ ��� (float �� ���� ������ �ϱ� ������ ���������� �׻� �����Ͽ� ���� ���� ���� �̿�)
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        { //Debug.DrawRay(transform.position + Vector3.up * 0.7f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.7f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                //���콺�� ������ �ִ� ��� Idle�� �ٲ��� �ʰ� ��
                if (Input.GetMouseButton(0) == false)
                    State = Define.State.Idle;
                return;

            }
            //moveDist = _speed * Time.deltaTime ���� dir.magnitude�� �Ѿ���� ������ �αٿ��� ������ �߻�
            //�ذ� : Clamp -> value�� min���� ������ min����, max���� ũ�� max���� ������.(min �� max ���̰��� ��������)
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            //_destPos ������ �ٶ�
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        } 
    }

    protected override void UpdateSkill()
    {
        //Ÿ���� �ٶ󺸵��� ��
        //1. Ÿ���� �ִ��� Ȯ��
        if(_lockTarget != null)
        {
            // Ÿ�� ���� ���� ��� �� Ÿ�� ���⺤�ͷ��� ȸ���� quat ����
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            // Ÿ���� �ٶ󺸵��� ȸ��(�ε巴�� �����̵��� Lerp ���)
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    #region UpdateDie()
    //protected override void UpdateDie()
    //{
    //    //HP ���� �߰� �� �ۼ�
    //}
    #endregion
    #region OnKeyboard()
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
    #endregion
    #region OnStatechanged()
    //protected override void OnStateChanged(Define.State state)
    //{
    //    base.OnStateChanged(state);
    //}
    #endregion

    public void OnHitEvent()
    {
        
        if (_lockTarget != null)
        {
            // TODO
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
        }
        if (_stopSkill)
        {
            State = Define.State.Idle;
        }
        else
        {
            State = Define.State.Skill;
        }
    }

    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (State)
        {
            //MouseEvent �߻� �� State�� Idle Ȥ�� Moving�� ��� OnMouseEvent_IdleRun ȣ��
            case Define.State.Idle:
                OnMouseEvent_IdleRun(mouseEvent);
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(mouseEvent);
                break;
            case Define.State.Skill:
                {
                    if (mouseEvent == Define.MouseEvent.PointterUp)
                        _stopSkill = true;
                }
                break;
        }

    }

    void OnMouseEvent_IdleRun(Define.MouseEvent mouseEvent) 
    {
        //���콺 Ŭ�� ������ ray
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        //LayerMask mask = LayerMask.GetMask("Wall");

        switch (mouseEvent)
        {
            case Define.MouseEvent.PointterDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        //������ ������ �������� �Ǵ�. ������ ��� lockTarget ����
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point; 
                }
                break;
            case Define.MouseEvent.PointterUp:
                {
                    _stopSkill = true;
                }
                break;
        }
    }

    public override void PlayAnimationForState(Define.State state)
    {
        Animator anim = GetComponent<Animator>();
        if (anim == null) return;

        switch (state)
        {
            case Define.State.Idle:
                anim.CrossFade("Idle", 0.15f);
                break;
            case Define.State.Moving:
                anim.CrossFade("OneHandedRun", 0.15f);
                break;
            case Define.State.Skill:
                anim.CrossFade("OneHandedAttack", 0.15f, -1, 0);
                break;
            case Define.State.GetHit:
                //GetHit �ִϸ��̼� �߰�
                break;
            case Define.State.Die:
                // Die �ִϸ��̼� �߰�
                break;
        }
    }
}
