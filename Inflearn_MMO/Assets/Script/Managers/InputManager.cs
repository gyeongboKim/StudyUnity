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
        //if (Input.anyKey == false)  //아무 입력도 없는 경우
        //    return;

        // 키입력 시
        if (Input.anyKey && KeyAction != null)       //키보드 입력이 존재 하는 경우
            KeyAction.Invoke();

        //마우스 클릭 시
        if(MouseAction != null)
        {
            //마우스를 누르고 있을 때
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
                
            }
            //마우스가 떼졌을 때
            else
            {
                //한번이라도 pressed가 되었다면
                if (_pressed)
                    //클릭으로 간주
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
                
            }
        }
    }
}
