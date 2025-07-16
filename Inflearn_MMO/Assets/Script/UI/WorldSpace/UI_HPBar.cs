using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {

        Transform parent = transform.parent;
        Collider parentCollider = parent.GetComponent<Collider>();


        // �ݶ��̴��� �߾��� (���� ��ǥ)
        Vector3 colliderCenter = parentCollider.bounds.center;

        // �ݶ��̴��� y�� ���� ũ�� (�߾ӿ��� �ֻ�ܱ����� �Ÿ�)
        float halfHeight = parentCollider.bounds.extents.y;
        // ���� �� ���� ���� ������ ���� ���������� ���� �����ϵ��� 
        float verticalOffset = 0.3f;

        //UI�� ��ġ ���� (�θ� ������Ʈ�� �Ӹ� ���� ���Բ�) : �ݶ��̴� �߾� Y + �ݶ��̴� ������ ���� + �߰� ������          y������ �� ��ü���� �ݶ��̴� �ֻ���� ���Ƽ� HP �ٰ� ���� �߰��� ������ ��ó�� ���� �� ����. 
        transform.position = new Vector3(colliderCenter.x, colliderCenter.y + halfHeight + verticalOffset, colliderCenter.z);

        //transform.LookAt(Camera.main.transform); XXXX
        //������ �׳� ī�޶� �ٶ󺸵��� �����ϸ� �ſ� ������ �Ųٷε�. �׷��⿡ ī�޶� ���� ������ ������ ��

        //( ������ ) ������ ī�޶� �ٶ󺸵��� �� (��Ȯ���� ī�޶� ���� ������ �ٶ󺸵��� ��)
        transform.rotation = Camera.main.transform.rotation;

        
        // ���� HP���� MaxHP�� ����
        // �� �� �ϳ��� float���� ĳ����
        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}

