using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class RankListInfo
{
    public List<RankInfo> rankList;
    public RankListInfo() { Load(); }

    //新加排行榜信息
    public void Add(string name, int score, int time)
    {
        rankList.Add(new RankInfo(name, score, time));
    }

    public void Save()
    {
        PlayerPrefs.SetInt("renkListNum", rankList.Count);
        for (int i = 0; i < rankList.Count; i++)
        {
            RankInfo info = rankList[i];
            PlayerPrefs.SetString("name" + i, info.name);
            PlayerPrefs.SetInt("score" + i, info.score);
            PlayerPrefs.SetInt("time" + i, info.time);
        }
        //PlayerPrefs.Save();
    }
    private void Load()
    {
        int num = PlayerPrefs.GetInt("rankListNum",0);
        rankList = new List<RankInfo>();
        for (int i = 0; i < num; i++)
        {
            string name = PlayerPrefs.GetString("name"+i);
            int score = PlayerPrefs.GetInt("score"+i);
            int time = PlayerPrefs.GetInt("time" + i);
            RankInfo info = new RankInfo(name, score, time);
            rankList.Add(info);
        }
    }
}

public class RankInfo
{
    public string name;
    public int score;
    public int time;

    public RankInfo(string name, int score, int time)
    {
        this.name = name;
        this.score = score;
        this.time = time;
    }


}
public class Lesson2_E : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RankListInfo rl = new RankListInfo();
        print(rl.rankList.Count);
        for (int i = 0;i < rl.rankList.Count;i++)
        {
            print("name" + rl.rankList[i].name);
            print("score" + rl.rankList[i].score);
            print("time" + rl.rankList[i].time);

        }
        rl.Add("XR", 100, 1000);
        rl.Save();
    }

}
