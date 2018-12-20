using UnityEngine;
using System.Collections.Generic;
using System.IO;
using LitJson;

[SerializeField]
public class SlateInfo
{
    public NoteType[] Notes = new NoteType[8];

    public SlateInfo(string notes)
    {
        for (int i = 0; i < 8; i++)
            Notes[i] = (NoteType)(notes[i] - '0');
    }
}

public class SlateInfoLoader : Singleton<SlateInfoLoader>
{
    public List<string> m_SaveSlateInfos = new List<string>();
    public List<SlateInfo> m_SlateInfoDatas = new List<SlateInfo>();
    public bool isSave = false;

    public void SaveOrLoad()
    {
        if (isSave)
            SavePlayerInfo();
        else
            LoadPlayerInfo();
    }

    public void SavePlayerInfo()
    {
        m_SaveSlateInfos.Add("01201201");
        m_SaveSlateInfos.Add("01231201");
        m_SaveSlateInfos.Add("01201208");

        JsonData infoJson = JsonMapper.ToJson(m_SaveSlateInfos);

        File.WriteAllText(Application.dataPath + "/Resources/Data/SlateInfoData.json",
            infoJson.ToString());

        isSave = false;
    }

    public void LoadPlayerInfo()
    {
        Debug.Log("Load Data : SlateInfo");

        if (File.Exists(Application.dataPath + "/Resources/Data/SlateInfoData.json"))
        {
            string jsonStr = File.ReadAllText(Application.dataPath + "/Resources/Data/SlateInfoData.json");

            JsonData slateDatas = JsonMapper.ToObject(jsonStr);

            for (int i = 0; i < slateDatas.Count; i++)
            {
                Debug.Log(slateDatas[i].ToString());
                m_SlateInfoDatas.Add(new SlateInfo(slateDatas[i].ToString()));
            }
        }
        else
        {
            Debug.Log("파일이 존재하지 않아용 !");
        }
    }

    public SlateInfo GetSlateInfo()
    {
        return m_SlateInfoDatas[Random.Range(0, m_SlateInfoDatas.Count)];
    }
}
