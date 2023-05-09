using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, Sprite> fleryPortraitData;

    public Sprite[] portraitArr;
    public Sprite[] fleryPortraitArr;

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
        portraitData.Add(0 + 0, fleryPortraitArr[0]);

        portraitData.Add(1000 + 0, fleryPortraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[0]);
        portraitData.Add(1000 + 2, portraitArr[1]);

        portraitData.Add(2000 + 0, fleryPortraitArr[0]);
        portraitData.Add(2000 + 1, portraitArr[2]);


        // Quest Talk
        talkData.Add(10 + 0, new string[] { "���մ��� ���� ���̶�� �θ��̴µ�...:0", "�� �ô����� �ܽɺθ��̽÷���?:0",
            "���� �ƴϾ����� ���ڴµ�...:0", "��·�� �ճ������ ������!:0" });

        talkData.Add(20 + 1000, new string[] { "Zzz...:2", "���մ�...?:0", "Zzz... Zzz...:2", 
        "��~~ ��~~ ��~!!:0", "... ��.......?:1", "�Ͼ�... ����... �÷����ݾ�...?:1", "���� ���ڰ� �ִµ� �� ����ž�...:1",
        "������... �˼��ؿ�...:0", "�� �ƴϰ�! ���մ��� ���� ���̶�� ���� �θ����ݾƿ�!:0", "��...?:1",
        "��, �׷� �´�......:1", "�� ��� �׷��� ���� ���� �ƴϰ�:1", "(���� �׷� �� �˾Ҿ�...):0", "�̰� �ѹ� ����...:1"});

        talkData.Add(21 + 1000, new string[] { "�̰� ����ü ������?:0", "�����θ� �����ϴ� ������ �����̶�µ�...:1", 
            "�ѹ� ������ ����� �� ���� ���� ���Ѵٴµ�...:1", "�׷�����:0", "�װ� �װ� �� ��������:1", "(���ó�...):0",
        "��, �˰ڽ��ϴ� �ճ��!:0", "�׷�... �� ��Ź��...:1", ".........:1", "Zzz....:2", "(�ٽ� �ڹ��ȴ�.):0",
        "�ϴ� ��������...:0"});

        talkData.Add(30 + 1000, new string[] { "(�Ͼ ��̰� ������ �ʴ´�...):1" });
        talkData.Add(30 + 0, new string[] { "����... �׷��� ȥ�ڼ� �� �� ������?:0", "Zzz... ����...",
            "��? �и� �ձ� ������ ���ͼ� �ճ���� �Ȱ���ٵ�...?:0", "�� �ڰ�� �Ҹ��� ���°���?:0"});
        talkData.Add(30 + 2000, new string[] { "Zzz... ����...:1", "�� ����̱���!:0", "�ѹ� ���ʹ޶�� ��Ź �غ��߰ڴ�!:0",
        "Zzz... ����...:1", "��... �����!:0", "��!:1"});

        //talkData.Add(30 + 1000, new string[] { "(�Ͼ ��̰� ������ �ʴ´�...):1" });
        //talkData.Add(30 + 2000, new string[] { "���մ���?:0", "�׷�����...:0" });
        //talkData.Add(31 + 2000, new string[] { "������ ���� �ϳ��� �Ҿ������ ���ϼ�...:0", "���� �ϳ��� ã���ֽðԳ�:0" });
        //talkData.Add(31 + 200, new string[] { "���ڸ� ã�Ҵ�!" });
        //talkData.Add(32 + 2000, new string[] { "���� ����!:0" });
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
