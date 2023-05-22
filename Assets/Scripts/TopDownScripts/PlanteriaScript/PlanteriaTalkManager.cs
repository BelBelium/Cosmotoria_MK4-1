using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanteriaTalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, Sprite> fleryPortraitData;

    public Sprite[] portraitArr;
    public Sprite[] FleryportraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        fleryPortraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    private void GenerateData()
    {
        // Talk Data
        // �÷��׸���
        // �÷��þ�: 1000, �纸��: 2000,
        talkData.Add(100, new string[] { "�˾ƺ� �� ���� �۾��� �������ִ�. �̰� ����..." });
        talkData.Add(1000, new string[] { "�ȳ�....:0", "����...:1" });
        talkData.Add(2000, new string[] { "(�ڰ��ִ�...):0", "����...:0" });

        // Portrait Data
        // �÷��׸���
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(2000 + 0, portraitArr[2]);


        // Quest Talk        
        talkData.Add(10 + 1000, new string[] { "Zzz...:1", "��... �Դ�...:0", "����....:1" });

        talkData.Add(20 + 1000, new string[] { "��...:0", "��... �ϴ� �纸������ ����...:0", "����....:1" });
        talkData.Add(21 + 1000, new string[] { "����� ���� ���� ����̾�...:0", "����....:1" });
        talkData.Add(30 + 1000, new string[] { "(�Ͼ ��̰� ������ �ʴ´�...):1" });
        talkData.Add(30 + 2000, new string[] { "���մ���?:0", "�׷�����...:0" });
        talkData.Add(31 + 2000, new string[] { "������ ���� �ϳ��� �Ҿ������ ���ϼ�...:0", "���� �ϳ��� ã���ֽðԳ�:0" });
        talkData.Add(31 + 200, new string[] { "���ڸ� ã�Ҵ�!" });
        talkData.Add(32 + 2000, new string[] { "���� ������!:0" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            // ����Ʈ �� ó�� ��縶�� ���� ��
            // �⺻ ��縦 ���  
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); // Get First Talk
            // �ش� ����Ʈ ���� ���� ��簡 ���� ��
            // ����Ʈ �� ó�� ��縦 ������ ��
            else
                return GetTalk(id - id % 100, talkIndex); // Get First Quest Talk
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}