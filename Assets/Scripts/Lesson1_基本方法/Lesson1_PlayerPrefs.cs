using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson1_PlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ///pLayerprefs��ʲô
        //��Unity�ṩ�Ŀ������ڴ洢��ȡ������ݵĹ�����

        ///�洢���
        //PlayerPrefs�����ݴ洢 �����ڼ�ֵ��
        //�� string
        //ֵ int float string
        PlayerPrefs.SetInt("myAge", 18);
        PlayerPrefs.SetFloat("myHeight", 177.5f);
        PlayerPrefs.SetString("myName", "XR");

        //ֱ�ӵ���Set��ط��� ֻ������ݴ浽 �ڴ� ��
        //����Ϸ������ Unity���Զ������ݴ浽Ӳ����
        //�����Ϸ�������������� ���ݲ���浽Ӳ����
        //ֻҪ���ø÷��� �ͻ����ϴ洢��Ӳ����
        PlayerPrefs.Save();

        //PlayerPrefs�о����� ֻ�ܴ��������͵�����
        //�������Ҫ�洢������͵����� ֻ�ܽ��;��� �����������������д洢
        bool sex = true;
        PlayerPrefs.SetInt("SEX", sex ? 1 : 0);
        //�����ͬ��ͬһ�����ü������д洢������и���
        PlayerPrefs.SetFloat("myAge", 20.2f);


        ///��ȡ���
        //���ȴ��ڴ��ң���ʹû�����ϴ洢save�ڱ���
        //int
        int age = PlayerPrefs.GetInt("myAge");
        print(age); //0 �Ҳ���
        age = PlayerPrefs.GetInt("myAge", 100);//�Ҳ�����Ӧ��ֵ �ͷ��صڶ���ֵĬ��ֵ
        //float
        float height = PlayerPrefs.GetFloat("myHeight", 1000f);//�ڶ�������Ĭ��ֵ ��û�����ݵ�ʱ�򣬿��Խ��л������ݵĳ�ʼ��
        print(height);
        //string
        string name = PlayerPrefs.GetString("myName");

        //�ж������Ƿ���ڣ����岻�� ������Ĭ��ֵ��
        if (PlayerPrefs.HasKey("myName"))
        {
            print("exist");

        }

        //ɾ��ָ����ֵ��
        PlayerPrefs.DeleteKey("myName");
        //ɾ�����д洢����Ϣ
        PlayerPrefs.DeleteAll();



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
