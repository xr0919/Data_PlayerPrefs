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
        //1.����֪ʶ�ع�
        //1T 2A
        //Type-- ���ڻ�ȡ ���������Ϣ �ֶ� ���� ���� �ȵ�
        //Assembly-- ���ڻ�ȡ���� ͨ�����򼯻�ȡType �����ã�
        //Activator-- ����ʵ��������

        //2.�ж�һ�����͵Ķ����Ƿ��������һ������Ϊ�Լ�����ռ�
        //����װ����
        Type fatherType = typeof(Father);
        Type SonType = typeof(Son);

        //������ ͨ���÷��������ж�  �ж��Ƿ����ͨ�����������Ϊ�Լ�����ռ�
        if (fatherType.IsAssignableFrom(SonType))
        {
            print("����װ");
            Father f = Activator.CreateInstance(SonType) as Father;
            print(f);

        }
        else
        {
            print("����װ");

        }

        //3.ͨ�������ȡ��������
        List<string> list = new List<string>();
        Type listType = list.GetType();

        Type[] types = listType.GetGenericArguments();//�õ����Ͳ�������
        print(types[0]);

        Dictionary<string,float> dic = new Dictionary<string,float>();  
        Type dicType = dic.GetType(); //���ж��󶼿��Ե���GetType�������õ�����Type��Ϣ
        types = dicType.GetGenericArguments();
        print(types[0]);
        print(types[1]);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
