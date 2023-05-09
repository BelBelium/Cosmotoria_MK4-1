using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId; // ����Ʈ ��ȣ
    public int questActionIndex; // ����Ʈ ����
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    public void GenerateData()
    {
        questList.Add(10, new QuestData("����Ʈ ù ����", new int[] { 0 }));

        questList.Add(20, new QuestData("���� ù ��ȭ", new int[] { 1000, 1000 }));

        questList.Add(30, new QuestData("���� ã���� ����", new int[] { 0, 2000 }));

        //questList.Add(40, new QuestData("�纸�ٰ� ��ȭ�ϱ�", new int[] { 2000, 200, 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // ���� ��ȭ ���
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        // ��ȭ �Ϸ� & ���� ����Ʈ
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // ����Ʈ �̸�
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        // ����Ʈ �̸�
        return questList[questId].questName;
    }

    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObject()
    {
        switch (questId)
        {
            case 10:
                //if (questActionIndex == 0)
                    
                break;
            case 20:
                if (questActionIndex == 1)
                    questObject[1].SetActive(true);
                break;
            case 30:
                if (questActionIndex == 0)
                {
                    questObject[1].SetActive(false);
                    questObject[2].SetActive(true);
                }
                    
                break;
        }
    }
}
