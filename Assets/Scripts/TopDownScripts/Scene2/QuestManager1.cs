using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestManager1 : MonoBehaviour
{
    public int questId; // 퀘스트 번호
    public int questActionIndex; // 퀘스트 순서
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
        questList.Add(10, new QuestData("퀘스트 첫 시작", new int[] { 0 }));
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
                if (questActionIndex == 1)
                {
                    introObject[2].SetActive(false);
                    SceneManager.LoadScene("InGameScene_2");
                }
                    
                break;           
        }
    }
}
