using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum WorldObject
    { 
        Unknown,
        Player,
        Monster,
    }


    public enum Layer
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,

    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,        //���콺 ��ư�� ������ �ִ� ���� ���������� �߻� 
        PointterDown, //���콺 ��ư�� ������ ����(1ȸ)
        PointterUp,   //���콺 ��ư�� ���� ����(1ȸ)  
        Click,        //���콺 ��ư�� �����ٰ� ���� �ð� ���� ���� ����(1ȸ, Dwon/Up ����)  
    }
    public enum CameraMode
    { 
        QuarterView,
    }



    public enum State
    {
        Idle,
        Moving,
        Skill,      //atack, buff, 
        GetHit,
        Die,
    }
}
