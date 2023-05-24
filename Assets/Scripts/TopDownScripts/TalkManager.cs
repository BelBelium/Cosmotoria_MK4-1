using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] fleryPortraitArr;
    public Sprite[] sabotenPortraitArr;
    public Sprite[] queenPortraitArr;
    public Sprite[] teapotpoPortraitArr;
    public Sprite nullPortrait;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();

        GenerateData();
    }

    private void GenerateData()
    {
        // Talk Data
        // �÷��׸���
        // �÷��þ�: 1000, �纸��: 2000,
        talkData.Add(100, new string[] { "�˾ƺ� �� ���� �۾��� �������ִ�. �̰� ����..." });
        talkData.Add(200, new string[] { "[������ ���]", "�ƹ� ���� ���� ��, ��� ���� ��." });
        talkData.Add(300, new string[] { "��ġ ä���� ���� ������ ���� ��Ī�� �ƴմϴ�." });
        talkData.Add(400, new string[] { "�̹� ���� ���ڸ� ���� ���� ���빰�� �������� ����.", "���� ���� �ֳ� ���캸���� ���ó� ����." });
        talkData.Add(500, new string[] { "." });
        talkData.Add(1000, new string[] { "�ȳ�....:3", "����...:4" });
        talkData.Add(2000, new string[] { "(�ڰ��ִ�...):2", "����...:4" });
        talkData.Add(3000, new string[] { "���? ������ �Ȱ��̳���?:3", "������...! ���� �ִٰ� ������:1", "�ȵ���!! �ճ���� ��ٸ��Ŵٱ���!:7",
        "��... �˰ھ�! ���� ������!!:2", "(�������� ������� Ƽ����...):0"});

        // Portrait Data
        // �÷��׸���
        portraitData.Add(0 + 0, fleryPortraitArr[0]); // �⺻
        portraitData.Add(0 + 1, fleryPortraitArr[1]); // ���
        portraitData.Add(0 + 2, fleryPortraitArr[2]); // Ȳ��
        portraitData.Add(0 + 3, nullPortrait);

        portraitData.Add(1000 + 0, fleryPortraitArr[0]);
        portraitData.Add(1000 + 1, fleryPortraitArr[1]);
        portraitData.Add(1000 + 2, fleryPortraitArr[2]);
        portraitData.Add(1000 + 3, queenPortraitArr[0]); // �⺻
        portraitData.Add(1000 + 4, queenPortraitArr[1]); // �ڱ�
        portraitData.Add(1000 + 5, nullPortrait);

        portraitData.Add(2000 + 0, fleryPortraitArr[0]);
        portraitData.Add(2000 + 1, fleryPortraitArr[1]);
        portraitData.Add(2000 + 2, fleryPortraitArr[2]);
        portraitData.Add(2000 + 3, sabotenPortraitArr[0]); // �⺻
        portraitData.Add(2000 + 4, sabotenPortraitArr[1]); // �ڱ�
        portraitData.Add(2000 + 5, sabotenPortraitArr[2]); // �����
        portraitData.Add(2000 + 6, nullPortrait);

        portraitData.Add(3000 + 0, fleryPortraitArr[0]);
        portraitData.Add(3000 + 1, fleryPortraitArr[1]);
        portraitData.Add(3000 + 2, fleryPortraitArr[2]);
        portraitData.Add(3000 + 3, teapotpoPortraitArr[0]); // �⺻
        portraitData.Add(3000 + 4, teapotpoPortraitArr[1]); // ����
        portraitData.Add(3000 + 5, teapotpoPortraitArr[2]); // Ȳ��
        portraitData.Add(3000 + 6, teapotpoPortraitArr[3]); // ��������
        portraitData.Add(3000 + 7, teapotpoPortraitArr[4]); // ȭ��
        portraitData.Add(3000 + 8, nullPortrait);

        // Quest Talk
        talkData.Add(10 + 0, new string[] { "���մ��� ���� ���̶�� �θ��̴µ�...:0", "�� �ô����� �ܽɺθ��̽÷���?:0",
            "���� �ƴϾ����� ���ڴµ�...:0", "��·�� �ϴ� ���� ���� �ճ������ ������!:0" });

        talkData.Add(20 + 3000, new string[] { "Ƽ����~:1", "�÷��� ���̵��~ ���� ���̽���?:4", "������ �׳� ��ͺþ�! ����:1",
        "��~ ����:4", "��! �ٵ� �ճ���� �÷������� ã���̾��!:7", "���������� �������?:3", "�� �´� �� ���� �� ��!:0",
        "�׷� �̵��� �ٽ� ����~!:1", "��~~ �ٳ������!:4" });

        talkData.Add(20 + 1000, new string[] { "Zzz...:4", "���մ�...?:0", "Zzz... Zzz...:4", 
        "��~~ ��~~ ��~!!:0", "... ��.......?:3", "�Ͼ�... ����... �÷����ݾ�...?:3", "���� ���ڰ� �ִµ� �� ����ž�...:3",
        "������... �˼��ؿ�...:2", "�� �ƴϰ�! ���մ��� ���� ���̶�� ���� �θ����ݾƿ�!:0", "��...?:3",
        "��, �׷� �´�......:3", "�� ��� �׷��� ���� ���� �ƴϰ�:3", "(���� �׷� �� �˾Ҿ�...):2", "�̰� �ѹ� ����...:3"});

        talkData.Add(21 + 1000, new string[] { "�̰� ����ü ������?:0", "�����θ� �����ϴ� ������ �����̶�µ�...:3", 
            "�ѹ� ������ ����� �� ���� ���� ���Ѵٴµ�...:1", "�׷�����:0", "�װ� �װ� �� ��������:3", "(���ó�...):0",
        "��, �˰ڽ��ϴ� �ճ��!:1", "�׷�... �� ��Ź��...:3", ".........:3", "Zzz....:4", "(�ٽ� �ڹ��ȴ�.):0",
        "�ϴ� ��������...:0"});
       
        talkData.Add(30 + 0, new string[] { "����... �׷��� ȥ�ڼ� �� �� ������?:0", "Zzz... ����...:3",
            "��? �и� �ձ� ������ ���ͼ� �ճ���� �Ȱ���ٵ�...?:0", "�� �ڰ�� �Ҹ��� ���°���?:0"});
        talkData.Add(31 + 2000, new string[] { "Zzz... ����...:4", "�� ����̱���!:0", "�ѹ� ���ʹ޶�� ��Ź �غ��߰ڴ�!:0",
        "Zzz... ����...:4", "��... �����!:0", "��!:5", "��, �������̽ÿ�?:3", "��... Ȥ�� �� ������ ����ִ��� �˰� ��������?:0",
        "(�纸�ٿ��� ���� ������ �������.):6", "��... �� �𸣰ڱ���, ���� ó�� ���� �����̿ɳ׸�:3", "�� �׷�����!:0",
        "������ �� �ż� �̾��ϱ���:3", "�ƴϿ���! �����ƿ�!:0", "�׷��� Ȥ��... �Ƿʰ� �ȵȴٸ�:0",
        "���� ���� �� ������ ã�� ���� �����ֽ� �� �ֳ���?:0", "����, ��ħ �ɽ��ϴ� ���̴�:3", "(��û ������ �����̴�?!):1",
        "��... �����մϴ�! �׷� ���ݺ��� ���� ã���� ������!:0", "����...:4", "(�׻� �ڰ��־�??!!):2", "�Ͼ����!!:0",
        "��... ��!:5", "�˰ڼ�...:3"});

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
