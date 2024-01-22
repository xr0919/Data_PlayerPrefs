using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRe
{
    public int id;
    public int num;
    public ItemRe()
    {

    }
    public ItemRe(int id, int num)
    {
        this.id = id;
        this.num = num;
    }
}

public class PlayerRe
{
    public string name;
    public int age;
    public int atk;
    public int def;
    private string keyName;
    public List<ItemRe> items = new List<ItemRe>();
    public PlayerRe()
    {

    }


    public void Save()
    {
        PlayerPrefs.SetString(keyName + "_Rname", name);
        PlayerPrefs.SetInt(keyName+"_Rage", age);
        PlayerPrefs.SetInt(keyName + "_Ratk", atk);
        PlayerPrefs.SetInt(keyName + "_Rdef", def);

        for (int i = 0; i < items.Count; i++)
        {
            PlayerPrefs.SetInt("itemId" + i, items[i].id);
            PlayerPrefs.SetInt("itemNum" + i, items[i].num);

        }

        PlayerPrefs.Save();

    }
    public void Load(string keyName)
    {
        this.keyName = keyName;
        name = PlayerPrefs.GetString(keyName+"_Rname", "Unknown");
        age = PlayerPrefs.GetInt(keyName+"_Rage", 10);
        atk = PlayerPrefs.GetInt(keyName+"_Ratk", 10);
        def = PlayerPrefs.GetInt(keyName+"_Rdef", 10);

        for (int i = 0; i < items.Count; i++)
        {
            
            ItemRe item = new ItemRe(PlayerPrefs.GetInt("itemId"), PlayerPrefs.GetInt("itemNum"));
            items.Add(item);
            Console.WriteLine(item.id + " " + item.num);
        }
    }
    public void Dele()
    {
        PlayerPrefs.DeleteAll();
    }
}

public class Lesson2_E_review : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1.
        PlayerRe p1 = new PlayerRe();
        p1.Load("Player1");//可存多个玩家数据
        print(p1.name);

        ItemRe i1 = new ItemRe();
        i1.id = 1;
        i1.num = 111;
        p1.items.Add(i1);
        ItemRe i2 = new ItemRe();
        i2.id = 1;
        i2.num = 111;
        p1.items.Add(i2);
        /*p1.name = "XR";
        p1.age = 18;
        p1.atk = 18;
        p1.def = 18;*/
        p1.Save();
        //p1.Dele();
    }

    // Update is called once per frame
    void Update()
    {


    }
}
