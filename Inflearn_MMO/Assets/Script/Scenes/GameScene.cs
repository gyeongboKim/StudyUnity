using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    #region CoroutineTest
    //class Test
    //{
    //    public int id = 0;
    //}
    ////������ �����ɸ��ų� ������ �Լ�
    ////���� ��ã��
    ////1������ �ȿ� �ȵǴ� ��� �Ͻ� ���� �� ���� �����ӿ� ����
    ////1. �Լ��� ���¸� ����/���� ����
    ////  ��û �����ɸ��� �۾��� ��� ���ų�
    ////  ���ϴ� Ÿ�ֿ̹� �Լ��� ��� Stop/�����ϴ� ���
    ////2. return -> ���ϴ� Ÿ������ ���� (class �� ����)
    //class CoroutineTest : IEnumerable
    //{
    //    public System.Collections.IEnumerator GetEnumerator()
    //    {
    //        for(int i = 0; i < 1000000; i ++)
    //        {
    //            if(i % 10000 == 0)
    //                yield return null;
    //        }
    //        //yield return new Test() { id = 1 };
    //        //yield return null;
    //        ////yield break;  //�ƿ� ���� ����
    //        //yield return new Test() { id = 2 };
    //        //yield return new Test() { id = 3 };
    //        //yield return new Test() { id = 4 };

    //    }

    //    void GenerateItem()
    //    {
    //        //����
    //        //1. �������� �����
    //        //2. DB�� �����Ѵ�
    //        //1���� 2���� ������ �Ǵ°��� ��ٸ��� �ʰ� ���� ������ ����Ǹ� ���ÿ����� ������ ���¿� DB������ �������� ���� �ʰԵ�
    //        //������ ���簡 �Ͼ�ų�, �������� ���� ���·� ���ư��ְų� �ϴ� ���� �߻�
    //    }
    //    //�ð� ���� ��� �߾ӿ��� �������� ������ �� �ִ� Ÿ�� �ý��� �ʿ�.
    //    //4�ʸ� ���� ���� �Ź� üũ�ϰ� ���ϴ� ����� �Ը� Ŀ���� ��� ū ���ҽ� ���� ��.
    //    float deltaTime = 0;
    //    void ExplodeAfter4Seconds()
    //    {
    //        deltaTime += Time.deltaTime;
    //        if(deltaTime >= 4)
    //        {
    //            //����
    //        }
    //    }
    //}
    #endregion

    Coroutine co;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.GameScene;

        //Managers.UI.ShowPopupUI<UI_Button>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);


        //Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/ChestMonster");
        //Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Beholder");
        //Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/Slime");
        //Managers.Game.Spawn(Define.WorldObject.Monster, "Monsters/TurtleShell");

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(5);
    }

    //IEnumerator CoStopExplode(float seconds)
    //{
    //    Debug.Log("Defusing...");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Defuse Complited!!");
    //    if(co != null)
    //    {
    //        StopCoroutine(co);
    //        co = null;
    //    }
    //}

    //IEnumerator CoExplodeAfterSeconds(float seconds)
    //{
    //    Debug.Log("Tic...Tic....");
    //    yield return new WaitForSeconds(seconds);
    //    Debug.Log("Boooooom!!!!");
    //    co = null;
    //}

    public override void Clear()
    {

    }



     
}
