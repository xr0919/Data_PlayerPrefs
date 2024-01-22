using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/// <summary>
/// PlayerPrefs数据管理类 统一管理数据的存储和读取
/// </summary>
//管理类 单例模式 不继承Monobehavior
public class PlayerPrefsDataMgr
{
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();
    private PlayerPrefsDataMgr() { }
    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="keyName">数据对象的唯一key 自己控制</param>
    public void SaveData(object data, string keyName)
    {
        //1.获取传入数据对象的所有字段
        Type type = data.GetType();
        //得到所有字段
        FieldInfo[] fieldInfos = type.GetFields();
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            Debug.Log(fieldInfos[i].Name);
        }

        //2.定义一个key规则 进行数据存储
        //保证key的唯一性 要自己顶一个key规则
        //自己定一个规则
        //keyName_数据类型_字段类型_字段名



        //3.遍历这些字段 进行数据存储
        string saveKeyName = "";
        FieldInfo fieldInfo = null;//优化代码 避免每次在循环时创建临时变量
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            //Player1_PlayerTest
            //根据key的拼接规则 进行key的生成
            fieldInfo = fieldInfos[i];
            saveKeyName = keyName + "_" + type.Name + "_" + fieldInfo.FieldType.Name + "_" + fieldInfo.Name;
            //Debug.Log(saveKeyName);
            //得到字段类型
            //fieldInfo.FieldType.Name;
            //得到字段的名字
            //fieldInfo.Name

            //现在以及得到key 接下来通过PlayerPrefs存储
            //如何获取值
            //fieldInfo.GetValue(data);
            //封装了一个方法专门存储值
            SaveValue(fieldInfo.GetValue(data), saveKeyName);
        }
        PlayerPrefs.Save();
    }
    private void SaveValue(object value, string saveKeyName)
    {
        //根据数据类型不同 来决定使用哪一个API来存储
        //判断数据类型 调用具体方法存储
        Type fieldType = value.GetType();
        if (fieldType == typeof(int))
        {
            Debug.Log("存储int " + saveKeyName);
            PlayerPrefs.SetInt(saveKeyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            Debug.Log("存储float " + saveKeyName);
            PlayerPrefs.SetFloat(saveKeyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            Debug.Log("存储string " + saveKeyName);
            PlayerPrefs.SetString(saveKeyName, value.ToString());
        }
        else if (fieldType == typeof(bool))
        {
            Debug.Log("存储bool " + saveKeyName);
            //自己定义一个存储bool的规则
            PlayerPrefs.SetInt(saveKeyName, (bool)value ? 1 : 0);
        }
        //如何判断 泛型类的类型  == typeof(List<>)不行 因为不确定它里面泛型的类型
        //通过反射 判断 父子关系 判断它有没有继承一个共同的父类 这个父类也不能有泛型
        //判断字段是不是IList的子类
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            Debug.Log("存储List " + saveKeyName);
            //父类装子类
            IList list = value as IList;
            //先存储数量
            PlayerPrefs.SetInt(saveKeyName, list.Count);
            //用于区分key
            int index = 0;
            foreach (object obj in list)
            {
                //存储具体的值 已经确定了是List类型但是不确定具体泛型类型
                //递归
                SaveValue(obj, saveKeyName + index);
                ++index;
            }
        }
        //判断是不是Dictionary类型 通过Dictionary的父类来判断
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            Debug.Log("存储Dictionary " + saveKeyName);
            IDictionary iDic = value as IDictionary;
            PlayerPrefs.SetInt(saveKeyName, iDic.Count);
            int index = 0;
            foreach (object key in iDic.Keys)
            {
                SaveValue(key, saveKeyName + "_key_" + index);
                SaveValue(iDic[key], saveKeyName + "_value_" + index);
                ++index;
            }
        }
        //若以上基础数据类型都不是则是自定义类
        //要把其他常用数据类型都补充齐
        else
        {
            //再递归
            //因为自定义类等于传入的类中再套一个类
            SaveData(value, saveKeyName);
        }
    }

    /// <summary>
    /// 不用object对象传入 而使用 Type传入
    /// 主要目的节约一行代码在外部
    /// 假设现在 读取一个Player类型的数据 如果是object 你就必须在外部new一个对象
    /// 现在有Type 你只要传入一个Type typeof（Player）然后在内部动态创建一个对象给你返回出来
    /// </summary>
    /// <param name="type"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public object LoadData(Type type, string keyName)
    {
        //根据传入的Type 创建一个对象 用于存储数据
        object data = Activator.CreateInstance(type);//前提是有无参构造
        //要往这个new出来的对象中存储数据 填充数据
        //得到所有字段
        FieldInfo[] fieldInfos = type.GetFields();
        string loadKeyName = "";
        FieldInfo fieldInfo = null;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            fieldInfo = fieldInfos[i];
            loadKeyName = keyName + "_" + type.Name + "_" + fieldInfo.FieldType.Name + "_" + fieldInfo.Name;

            //填充数据到data中
            fieldInfo.SetValue(data, LoadValue(fieldInfo.FieldType, loadKeyName));
        }
        return data;
    }
    private object LoadValue(Type fieldType, string loadKeyName)
    {
        if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(loadKeyName);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(loadKeyName);
        }
        else if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(loadKeyName);
        }
        else if (fieldType == typeof(bool))
        {
            //自己定义一个存储bool的规则
            return PlayerPrefs.GetInt(loadKeyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //得到长度
            int count = PlayerPrefs.GetInt(loadKeyName, 0);
            //实例化一个List对象 来进行赋值
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //fieldType.GetGenericArguments()[0]得到list中泛型的类型
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], loadKeyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //得到长度
            int count = PlayerPrefs.GetInt(loadKeyName, 0);
            //实例化一个List对象 来进行赋值
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                //fieldType.GetGenericArguments()[0]得到list中泛型的类型
                dic.Add(LoadValue(kvType[0], loadKeyName +"_key_"+ i), LoadValue(kvType[1], loadKeyName + "_value_" + i));
            }
            return dic;
        }
        else
        {
            return LoadData(fieldType, loadKeyName);
        }
        return null;
    }
}
