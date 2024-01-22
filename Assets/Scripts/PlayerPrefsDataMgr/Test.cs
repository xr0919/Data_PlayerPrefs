using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest
{
    public int age;
    public string name;
    public float height;
    public bool sex;

    //反射存储LIst和dic类
    public List<int> list;
    public Dictionary<int, string> dic;
    //反射自定义类的存储
    public ItemTest itemTest;
    public List<ItemTest> itemList;
    public Dictionary<int, ItemTest> itemDic;

}
//测试用
/*
public class PlayerTest
{
    public int age = 10;
    public string name = "Unknown";
    public float height = 177.5f;
    public bool sex = true;

    //反射存储LIst和dic类
    public List<int> list = new List<int>() { 1, 2, 3, 4 };
    public Dictionary<int, string> dic = new Dictionary<int, string>() {
        { 1,"一" },{2,"二" } 
    };
    //反射自定义类的存储
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
    //要写个无参构造 因为不写有参构造会把他顶掉
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
        //0.删除
        //PlayerPrefs.DeleteAll();
        //1.读取数据
        PlayerTest p = PlayerPrefsDataMgr.Instance.LoadData(typeof(PlayerTest), "P1") as PlayerTest;
        //2.游戏逻辑中回去修改玩家数据
        p.age = 18;
        p.name = "XR";
        p.sex = true;
        p.itemList.Add(new ItemTest(1, 99));
        p.itemDic.Add(3, new ItemTest(3, 1));
        //3.游戏数据存储
        PlayerPrefsDataMgr.Instance.SaveData(p, "P1");


        //测试用
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
