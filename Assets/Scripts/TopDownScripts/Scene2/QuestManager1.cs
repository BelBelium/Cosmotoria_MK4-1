using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager1 : MonoBehaviour
{
    public int questId; // ����Ʈ ��ȣ
    public int questActionIndex; // ����Ʈ ����
    public GameObject[] questObject;
    public GameObject[] introObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    public void GenerateData()
    {
        questList.Add(10, new QuestData("����Ʈ ù ����", new int[] { 0 }));
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
                if (questActionIndex == 1)
                {
                    introObject[2].SetActive(false);
                    SceneManager.LoadScene("InGameScene_2");
                }
                    
                break;           
        }
    }
}
