using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Father
{

}
public class Son : Father
{

}

public class Reflection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1.反射知识回顾
        //1T 2A
        //Type-- 用于获取 类的所有信息 字段 属性 方法 等等
        //Assembly-- 用于获取程序集 通过程序集获取Type （少用）
        //Activator-- 快速实例化对象

        //2.判断一个类型的对象是否可以让另一个类型为自己分配空间
        //父类装子类
        Type fatherType = typeof(Father);
        Type SonType = typeof(Son);

        //调用者 通过该方法进行判断  判断是否可以通过传入的类型为自己分配空间
        if (fatherType.IsAssignableFrom(SonType))
        {
            print("可以装");
            Father f = Activator.CreateInstance(SonType) as Father;
            print(f);

        }
        else
        {
            print("不能装");

        }

        //3.通过反射获取泛型类型
        List<string> list = new List<string>();
        Type listType = list.GetType();

        Type[] types = listType.GetGenericArguments();//得到泛型参数类型
        print(types[0]);

        Dictionary<string,float> dic = new Dictionary<string,float>();  
        Type dicType = dic.GetType(); //所有对象都可以调用GetType（）来得到他的Type信息
        types = dicType.GetGenericArguments();
        print(types[0]);
        print(types[1]);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
