using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action <Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    public void OnUpdate()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        //if (Input.anyKey == false)  //�ƹ� �Էµ� ���� ���
        //    return;

        // Ű�Է� ��
        if (Input.anyKey && KeyAction != null)       //Ű���� �Է��� ���� �ϴ� ���
            KeyAction.Invoke();

        //���콺 Ŭ�� ��
        if(MouseAction != null)
        {
            //���콺�� ������ ���� ��
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
                
            }
            //���콺�� ������ ��
            else
            {
                //�ѹ��̶� pressed�� �Ǿ��ٸ�
                if (_pressed)
                    //Ŭ������ ����
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
                
            }
        }
    }
}
