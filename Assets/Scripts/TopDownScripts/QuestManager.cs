using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId; // Äù½ºÆ® ¹øÈ£
    public int questActionIndex; // Äù½ºÆ® ¼ø¼­
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    public void GenerateData()
    {
        questList.Add(10, new QuestData("¿©¿Õ Ã¹ ´ëÈ­", new int[] { 1000 }));

        questList.Add(20, new QuestData("¿©¿Õ ºÎÅ¹ µè±â", new int[] { 1000, 1000 }));

        questList.Add(30, new QuestData("»çº¸ÅÙ ºÎÅ¹ µè±â", new int[] { 2000, 200, 2000 }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        // ´ÙÀ½ ´ëÈ­ »ó´ë
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        // Control Quest Object
        ControlObject();

        // ´ëÈ­ ¿Ï·á & ´ÙÀ½ Äù½ºÆ®
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        // Äù½ºÆ® ÀÌ¸§
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        // Äù½ºÆ® ÀÌ¸§
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
                    questObject[0].SetActive(true);
                break;
            case 30:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 2)
                    questObject[0].SetActive(false);
                break;
        }
    }
}
