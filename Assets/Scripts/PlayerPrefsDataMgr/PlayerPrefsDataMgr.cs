using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


/// <summary>
/// PlayerPrefs���ݹ����� ͳһ�������ݵĴ洢�Ͷ�ȡ
/// </summary>
//������ ����ģʽ ���̳�Monobehavior
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
    /// �洢����
    /// </summary>
    /// <param name="data">���ݶ���</param>
    /// <param name="keyName">���ݶ����Ψһkey �Լ�����</param>
    public void SaveData(object data, string keyName)
    {
        //1.��ȡ�������ݶ���������ֶ�
        Type type = data.GetType();
        //�õ������ֶ�
        FieldInfo[] fieldInfos = type.GetFields();
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            Debug.Log(fieldInfos[i].Name);
        }

        //2.����һ��key���� �������ݴ洢
        //��֤key��Ψһ�� Ҫ�Լ���һ��key����
        //�Լ���һ������
        //keyName_��������_�ֶ�����_�ֶ���



        //3.������Щ�ֶ� �������ݴ洢
        string saveKeyName = "";
        FieldInfo fieldInfo = null;//�Ż����� ����ÿ����ѭ��ʱ������ʱ����
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            //Player1_PlayerTest
            //����key��ƴ�ӹ��� ����key������
            fieldInfo = fieldInfos[i];
            saveKeyName = keyName + "_" + type.Name + "_" + fieldInfo.FieldType.Name + "_" + fieldInfo.Name;
            //Debug.Log(saveKeyName);
            //�õ��ֶ�����
            //fieldInfo.FieldType.Name;
            //�õ��ֶε�����
            //fieldInfo.Name

            //�����Լ��õ�key ������ͨ��PlayerPrefs�洢
            //��λ�ȡֵ
            //fieldInfo.GetValue(data);
            //��װ��һ������ר�Ŵ洢ֵ
            SaveValue(fieldInfo.GetValue(data), saveKeyName);
        }
        PlayerPrefs.Save();
    }
    private void SaveValue(object value, string saveKeyName)
    {
        //�����������Ͳ�ͬ ������ʹ����һ��API���洢
        //�ж��������� ���þ��巽���洢
        Type fieldType = value.GetType();
        if (fieldType == typeof(int))
        {
            Debug.Log("�洢int " + saveKeyName);
            PlayerPrefs.SetInt(saveKeyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            Debug.Log("�洢float " + saveKeyName);
            PlayerPrefs.SetFloat(saveKeyName, (float)value);
        }
        else if (fieldType == typeof(string))
        {
            Debug.Log("�洢string " + saveKeyName);
            PlayerPrefs.SetString(saveKeyName, value.ToString());
        }
        else if (fieldType == typeof(bool))
        {
            Debug.Log("�洢bool " + saveKeyName);
            //�Լ�����һ���洢bool�Ĺ���
            PlayerPrefs.SetInt(saveKeyName, (bool)value ? 1 : 0);
        }
        //����ж� �����������  == typeof(List<>)���� ��Ϊ��ȷ�������淺�͵�����
        //ͨ������ �ж� ���ӹ�ϵ �ж�����û�м̳�һ����ͬ�ĸ��� �������Ҳ�����з���
        //�ж��ֶ��ǲ���IList������
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            Debug.Log("�洢List " + saveKeyName);
            //����װ����
            IList list = value as IList;
            //�ȴ洢����
            PlayerPrefs.SetInt(saveKeyName, list.Count);
            //��������key
            int index = 0;
            foreach (object obj in list)
            {
                //�洢�����ֵ �Ѿ�ȷ������List���͵��ǲ�ȷ�����巺������
                //�ݹ�
                SaveValue(obj, saveKeyName + index);
                ++index;
            }
        }
        //�ж��ǲ���Dictionary���� ͨ��Dictionary�ĸ������ж�
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            Debug.Log("�洢Dictionary " + saveKeyName);
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
        //�����ϻ����������Ͷ����������Զ�����
        //Ҫ�����������������Ͷ�������
        else
        {
            //�ٵݹ�
            //��Ϊ�Զ�������ڴ������������һ����
            SaveData(value, saveKeyName);
        }
    }

    /// <summary>
    /// ����object������ ��ʹ�� Type����
    /// ��ҪĿ�Ľ�Լһ�д������ⲿ
    /// �������� ��ȡһ��Player���͵����� �����object ��ͱ������ⲿnewһ������
    /// ������Type ��ֻҪ����һ��Type typeof��Player��Ȼ�����ڲ���̬����һ��������㷵�س���
    /// </summary>
    /// <param name="type"></param>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public object LoadData(Type type, string keyName)
    {
        //���ݴ����Type ����һ������ ���ڴ洢����
        object data = Activator.CreateInstance(type);//ǰ�������޲ι���
        //Ҫ�����new�����Ķ����д洢���� �������
        //�õ������ֶ�
        FieldInfo[] fieldInfos = type.GetFields();
        string loadKeyName = "";
        FieldInfo fieldInfo = null;
        for (int i = 0; i < fieldInfos.Length; i++)
        {
            fieldInfo = fieldInfos[i];
            loadKeyName = keyName + "_" + type.Name + "_" + fieldInfo.FieldType.Name + "_" + fieldInfo.Name;

            //������ݵ�data��
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
            //�Լ�����һ���洢bool�Ĺ���
            return PlayerPrefs.GetInt(loadKeyName, 0) == 1 ? true : false;
        }
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            //�õ�����
            int count = PlayerPrefs.GetInt(loadKeyName, 0);
            //ʵ����һ��List���� �����и�ֵ
            IList list = Activator.CreateInstance(fieldType) as IList;
            for (int i = 0; i < count; i++)
            {
                //fieldType.GetGenericArguments()[0]�õ�list�з��͵�����
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], loadKeyName + i));
            }
            return list;
        }
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            //�õ�����
            int count = PlayerPrefs.GetInt(loadKeyName, 0);
            //ʵ����һ��List���� �����и�ֵ
            IDictionary dic = Activator.CreateInstance(fieldType) as IDictionary;
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                //fieldType.GetGenericArguments()[0]�õ�list�з��͵�����
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
