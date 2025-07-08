using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuarterView;

    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 5.0f, -5.0f);

    //inspector���� ����
    [SerializeField]
    GameObject _player = null;  

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (_mode == Define.CameraMode.QuarterView)
        {
            RaycastHit hit;
            //Raycast �� ���� �ε��� ��� 
            if(Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Block")))
            {
                //ī�޶� ĳ���Ͱ� ���̰� Ȯ��. 
                //dist �� ���� �ε��� ���� ��ǥ�� �÷��̾� ��ǥ ������ ���� * 0.8
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _delta.normalized * dist;
                
            }
            else
            {
                //�Ϲ����� ���   
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
            
        }
    }

    //���ͺ並 �ڵ�󿡼� �����ϰ� ���� �� ���
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuarterView; 
        _delta = delta;
    }
}
