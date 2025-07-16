using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 2;


    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);

    }

    

    protected override void UpdateIdle()
    {
     
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if(distanceToPlayer <= _scanRange)
        {
            _lockTarget = player;
            State = Define.State.Moving;

            return;
        }
        
    }
    protected override void UpdateMoving()
    {
        NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
        //�����̴ٰ� Ÿ���� �� �����Ÿ����� ������ ���� �õ�
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distanceToLockTarget = (_destPos - transform.position).magnitude;

            if (distanceToLockTarget <= _attackRange)
            {   
                //nma�� �̿��Ͽ� ��ã�� 
                nma.SetDestination(transform.position);
                
                State = Define.State.Skill;
                return;
            }
        }

        //�̵�
        //����� ũ�⸦ ���� ���� ����
        Vector3 dir = _destPos - transform.position;
        //�������� ������ ��� (float �� ���� ������ �ϱ� ������ ���������� �׻� �����Ͽ� ���� ���� ���� �̿�)
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            //nma�� �̿��Ͽ� ��ã�� 
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;
 
            //_destPos ������ �ٶ�
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            //transform.LookAt(_destPos);
        }
    }
    protected override void UpdateSkill()
    {
        //Ÿ���� �ٶ󺸵��� ��
        //1. Ÿ���� �ִ��� Ȯ��
        if (_lockTarget != null)
        {
            // Ÿ�� ���� ���� ��� �� Ÿ�� ���⺤�ͷ��� ȸ���� quat ����
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            // Ÿ���� �ٶ󺸵��� ȸ��(�ε巴�� �����̵��� Lerp ���)
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    public void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat);
            
        }
        else
        {
            State = Define.State.Idle;
        }
    }

    //hit�� scan �и�. (�´� ������ �ٷ� �ǰ� ���, ������ ������ ������ scan�ϱ� ���ؼ�)
    public void OnScanEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            if (targetStat.Hp > 0)
            {
                float distanceToLockTarget = (_lockTarget.transform.position - transform.position).magnitude;
                if (distanceToLockTarget <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }

    }

    public override void PlayAnimationForState(Define.State state)
    {
        Animator anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogWarning($"Animator component missing on '{gameObject.name}'.");
            return;
        }

        switch (state)
        {
            case Define.State.Idle:
                anim.CrossFade("Idle", 0.15f);
                break;
            case Define.State.Moving:
                anim.CrossFade("Run", 0.15f);
                break;
            case Define.State.Skill:
                anim.CrossFade("Attack01", 0.15f, -1, 0);
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
