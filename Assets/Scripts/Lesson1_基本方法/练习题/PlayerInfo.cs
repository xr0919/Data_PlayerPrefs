using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public int num;
}
public class Player
{
    public string name;
    public int age;
    public int atk;
    public int def;
    public List<Item> eList;

    public void StoreInfo()
    {
        PlayerPrefs.SetString("name", this.name);
        PlayerPrefs.SetInt("age", this.age);
        PlayerPrefs.SetInt("atk", this.atk);
        PlayerPrefs.SetInt("def", this.def);

        PlayerPrefs.SetInt("itemNum", eList.Count);
        for (int i = 0; i < this.eList.Count; i++)
        {
            //存储每一个装备的信息
            PlayerPrefs.SetInt("itemId" + i, this.eList[i].id);
            PlayerPrefs.SetInt("itemNum" + i, this.eList[i].num);

        }
        PlayerPrefs.Save();

    }
    public void ReadInfo()
    {
        name = PlayerPrefs.GetString("name", "Unknown");
        age = PlayerPrefs.GetInt("age", 18);
        atk = PlayerPrefs.GetInt("atk", 10);
        def = PlayerPrefs.GetInt("def", 5);

        //初始化容器
        eList = new List<Item>();
        Item equip;
        int num = PlayerPrefs.GetInt("itemNum", 0);
        for (int i = 0; i < num; i++)
        {
            equip = new Item();
            equip.id = PlayerPrefs.GetInt("itemId" + i);
            equip.num = PlayerPrefs.GetInt("itemNum" + i);
            eList.Add(equip);

        }
    }
}

public class PlayerInfo : MonoBehaviour
{

    
    
    void Start()
    {
        /*PlayerInfo p = new PlayerInfo();
        p.ReadInfo();
        print(p.name);
        print(p.age);
        print(p.atk);

        p.name = "XR";
        p.age = 18;
        p.atk = 20;
        p.def = 20;
        print(p.name);
        print(p.age);
        print(p.atk);
        p.StoreInfo();*/

        Player p = new Player();
        p.ReadInfo();
        for (int i = 0; i < p.eList.Count; i++)
        {
            print("id: " + p.eList[i].id);
            print(" num: " + p.eList[i].num);
        }
        Item e = new Item();
        e.id = 1;
        e.num = 2;
        p.eList.Add(e);
        e = new Item();
        e.id = 2;
        e.num = 3;
        p.eList.Add(e);

        p.StoreInfo();
    }
    void Update()
    {

    }
}
