using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest
{
    public int age;
    public string name;
    public float height;
    public bool sex;

    //����洢LIst��dic��
    public List<int> list;
    public Dictionary<int, string> dic;
    //�����Զ�����Ĵ洢
    public ItemTest itemTest;
    public List<ItemTest> itemList;
    public Dictionary<int, ItemTest> itemDic;

}
//������
/*
public class PlayerTest
{
    public int age = 10;
    public string name = "Unknown";
    public float height = 177.5f;
    public bool sex = true;

    //����洢LIst��dic��
    public List<int> list = new List<int>() { 1, 2, 3, 4 };
    public Dictionary<int, string> dic = new Dictionary<int, string>() {
        { 1,"һ" },{2,"��" } 
    };
    //�����Զ�����Ĵ洢
    public ItemTest itemTest = new ItemTest(5,99);
    public List<ItemTest> itemList = new List<ItemTest>() { new ItemTest(1,10),new ItemTest(2,20), };
    public Dictionary<int,ItemTest> itemDic = new Dictionary<int, ItemTest>() {
        {3,new ItemTest(3,33) },
        {4,new ItemTest(4,44) },
    };

}*/
public class ItemTest
{
    public int id;
    public int num;
    //Ҫд���޲ι��� ��Ϊ��д�вι�����������
    public ItemTest() { }
    public ItemTest(int id, int num)
    {
        this.id = id;
        this.num = num;
    }   
}

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //0.ɾ��
        //PlayerPrefs.DeleteAll();
        //1.��ȡ����
        PlayerTest p = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerTest), "P1") as PlayerTest;
        //2.��Ϸ�߼��л�ȥ�޸��������
        p.age = 18;
        p.name = "XR";
        p.sex = true;
        p.itemList.Add(new ItemTest(1, 99));
        p.itemDic.Add(3, new ItemTest(3, 1));
        //3.��Ϸ���ݴ洢
        PlayerPrefsDataMgr.Instance.SaveData(p, "P1");


        //������
        /*
        PlayerTest p = new PlayerTest();
        //
        PlayerPrefsDataMgr.Instance.SaveData(p, "P1");
        PlayerTest p2 = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerTest), "P1") as PlayerTest;
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
