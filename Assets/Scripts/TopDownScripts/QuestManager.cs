using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId; // 퀘스트 번호
    public int questActionIndex; // 퀘스트 순서
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    public void GenerateData()
    {
        questList.Add(10, new QuestData("퀘스트 첫 시작", new int[] { 0 }));

        questList.Add(20, new QuestData("여왕 첫 대화", new int[] { 1000, 1000 }));

        questList.Add(30, new QuestData("음식 찾으러 가기", new int[] { 0, 2000 }));

        //questList.Add(40, new QuestData("사보텐과 대화하기", new int[] { 2000, 200, 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // 다음 대화 상대
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        // 대화 완료 & 다음 퀘스트
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // 퀘스트 이름
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        // 퀘스트 이름
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
