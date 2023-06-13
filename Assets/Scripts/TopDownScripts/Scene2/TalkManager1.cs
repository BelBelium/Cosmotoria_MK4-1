using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager1 : MonoBehaviour
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
        talkData.Add(500, new string[] { "�뵵�� �� �𸣰ڴ� �����̴�.", "���� �ɾƺ��� ���� �ʴ�." });
        talkData.Add(600, new string[] { "���� �������� ���� �����ִ�.", "... �������� ����..." });
        talkData.Add(1000, new string[] { "�ȳ�....:3", "����...:4" });
        talkData.Add(2000, new string[] { "(�ڰ��ִ�...):2", "����...:4" });
        talkData.Add(3000, new string[] { "���? ������ �Ȱ��̳���?:3", "������...! ���� �ִٰ� ������:1", "�ȵ���!! �ճ���� ��ٸ��Ŵٱ���!:7",
        "��... �˰ھ�! ���� ������!!:2", "(�������� ������� Ƽ����...):0"});

        // Portrait Data
        // �÷��׸���
        portraitData.Add(0 + 0, fleryPortraitArr[0]); // �⺻
        portraitData.Add(0 + 1, fleryPortraitArr[1]); // ���
        portraitData.Add(0 + 2, fleryPortraitArr[2]); // Ȳ��
        portraitData.Add(0 + 3, sabotenPortraitArr[0]);
        portraitData.Add(0 + 4, sabotenPortraitArr[1]);
        portraitData.Add(0 + 5, sabotenPortraitArr[2]);
        portraitData.Add(0 + 6, teapotpoPortraitArr[0]);
        portraitData.Add(0 + 7, teapotpoPortraitArr[3]);
        portraitData.Add(0 + 8, teapotpoPortraitArr[4]);
        portraitData.Add(0 + 9, nullPortrait);

        portraitData.Add(1000 + 0, fleryPortraitArr[0]);
        portraitData.Add(1000 + 1, fleryPortraitArr[1]);
        portraitData.Add(1000 + 2, fleryPortraitArr[2]);
        portraitData.Add(1000 + 3, queenPortraitArr[0]); // �⺻
        portraitData.Add(1000 + 4, queenPortraitArr[1]); // �ڱ�
        portraitData.Add(1000 + 5, queenPortraitArr[2]); // ��¦��¦
        portraitData.Add(1000 + 6, nullPortrait);

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
        talkData.Add(10 + 0, new string[] { "�̾�... �׷��� ������� ������ ���� �����µ�:2", "��;�! ����������??!:7",
            "��! Ƽ����! ���� �����ذž�?:1", "�λ纸�� ���� ����������??!:7", "����е��� �����ؿԴٰ� ������!!:8",
            "�׷���! ���� ū���̿�...!:3", "� �ճ�Կ��� �˷���...:3", "... ����...:4", "���� �� ���� �ƴϿ���!!:2",
            "���..!!:5", "��°�� ����е��� �׷��� �Ȱǰ���..?:6", "�ƿ��.. �׷��� ���̾�...:2", "������ ������ �� �ʿ䰡 �ְھ�:0",
            "���� �������� �Ƹ�...:3", "... ����...:4", "�� ����� ���ΰ�...:2", "�ϴ� �ճ������ ���� �ؾ߰���?:0",
            "�ճ�Կ��� ���ϱ� ���� �ϴ� �ڼ��� ��Ȳ�� ���캼 �ʿ䰡 �ִٰ� �����ϴµ���...:6", "Ȯ���� ���� ������ �˾Ƴ��� ���� �帮�� ���� ���� �� ���⵵...:0",
            "�ƿ�... ������ �ճ�� �ɺθ��� �ؾ��ϰ�, �ΰ��� ���� �� �س� �� ������...??:2", "�������� ���� '��ī����'���� ���� �ΰɱ��?:6",
            "�׷��ٰ� �ϱ⿣ ���������� �����ߴ� ���... �ƴ� �����̾��µ�...:0", "�ϴ��� ��ī���� �԰� �̾߱⸦ �������� �� �� ���׿�!:8",
            "��ī���� ���� �������� �����̱� �߾ �� ���� ������ �ƴϾ��µ�:0", "Zzz...:4", "���� �Ͼ����!! �� �ð� ���ϴٰ��!:2",
            "��...!! �˰ڼ�!!:5", "�� ��ī������� �ڿ��� �������� ����!:3", "(����...? �ڸ鼭 �� ����־���...?:2"});


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
