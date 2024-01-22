using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1_PlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ///pLayerprefs是什么
        //是Unity提供的可以用于存储读取玩家数据的公共类

        ///存储相关
        //PlayerPrefs的数据存储 类似于键值对
        //键 string
        //值 int float string
        PlayerPrefs.SetInt("myAge", 18);
        PlayerPrefs.SetFloat("myHeight", 177.5f);
        PlayerPrefs.SetString("myName", "XR");

        //直接调用Set相关方法 只会把数据存到 内存 中
        //当游戏结束是 Unity会自动把数据存到硬盘中
        //如果游戏不是正常结束的 数据不会存到硬盘中
        //只要调用该方法 就会马上存储到硬盘中
        PlayerPrefs.Save();

        //PlayerPrefs有局限性 只能存三种类型的数据
        //如果你想要存储别的类型的数据 只能降低精度 或者上升精度来进行存储
        bool sex = true;
        PlayerPrefs.SetInt("SEX", sex ? 1 : 0);
        //如果不同或同一类型用键名进行存储，会进行覆盖
        PlayerPrefs.SetFloat("myAge", 20.2f);


        ///读取相关
        //优先从内存找，即使没有马上存储save在本地
        //int
        int age = PlayerPrefs.GetInt("myAge");
        print(age); //0 找不到
        age = PlayerPrefs.GetInt("myAge", 100);//找不到对应的值 就返回第二个值默认值
        //float
        float height = PlayerPrefs.GetFloat("myHeight", 1000f);//第二个参数默认值 在没有数据的时候，可以进行基础数据的初始化
        print(height);
        //string
        string name = PlayerPrefs.GetString("myName");

        //判断数据是否存在（意义不大 可以用默认值）
        if (PlayerPrefs.HasKey("myName"))
        {
            print("exist");

        }

        //删除指定键值对
        PlayerPrefs.DeleteKey("myName");
        //删除所有存储的信息
        PlayerPrefs.DeleteAll();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
