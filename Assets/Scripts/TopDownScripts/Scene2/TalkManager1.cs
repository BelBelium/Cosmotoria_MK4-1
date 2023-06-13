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
        // 플렌테리아
        // 플렌시아: 1000, 사보텐: 2000,
        talkData.Add(100, new string[] { "알아볼 수 없는 글씨로 쓰여져있다. 이게 뭐람..." });
        talkData.Add(200, new string[] { "[선인장 방목]", "아무 이유 없이 좌, 우로 흔드는 중." });
        talkData.Add(300, new string[] { "눈치 채셨을 수도 있지만 성이 대칭이 아닙니다." });
        talkData.Add(400, new string[] { "이미 누가 상자를 열고 안의 내용물을 가져가고 없다.", "남은 것이 있나 살펴보지만 역시나 없다." });
        talkData.Add(500, new string[] { "용도를 잘 모르겠는 의자이다.", "별로 앉아보고 싶지 않다." });
        talkData.Add(600, new string[] { "새끼 선인장이 길을 막고있다.", "... 방해하지 말자..." });
        talkData.Add(1000, new string[] { "안녕....:3", "쿨쿨...:4" });
        talkData.Add(2000, new string[] { "(자고있다...):2", "쿨쿨...:4" });
        talkData.Add(3000, new string[] { "어라? 아직도 안가셨나요?:3", "아하하...! 좀만 있다가 가려고:1", "안돼죠!! 왕녀님이 기다리신다구요!:7",
        "아... 알겠어! 지금 가볼게!!:2", "(생각보다 무서운걸 티폿포...):0"});

        // Portrait Data
        // 플렌테리아
        portraitData.Add(0 + 0, fleryPortraitArr[0]); // 기본
        portraitData.Add(0 + 1, fleryPortraitArr[1]); // 기쁨
        portraitData.Add(0 + 2, fleryPortraitArr[2]); // 황당
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
        portraitData.Add(1000 + 3, queenPortraitArr[0]); // 기본
        portraitData.Add(1000 + 4, queenPortraitArr[1]); // 자기
        portraitData.Add(1000 + 5, queenPortraitArr[2]); // 반짝반짝
        portraitData.Add(1000 + 6, nullPortrait);

        portraitData.Add(2000 + 0, fleryPortraitArr[0]);
        portraitData.Add(2000 + 1, fleryPortraitArr[1]);
        portraitData.Add(2000 + 2, fleryPortraitArr[2]);
        portraitData.Add(2000 + 3, sabotenPortraitArr[0]); // 기본
        portraitData.Add(2000 + 4, sabotenPortraitArr[1]); // 자기
        portraitData.Add(2000 + 5, sabotenPortraitArr[2]); // 잠깨기
        portraitData.Add(2000 + 6, nullPortrait);

        portraitData.Add(3000 + 0, fleryPortraitArr[0]);
        portraitData.Add(3000 + 1, fleryPortraitArr[1]);
        portraitData.Add(3000 + 2, fleryPortraitArr[2]);
        portraitData.Add(3000 + 3, teapotpoPortraitArr[0]); // 기본
        portraitData.Add(3000 + 4, teapotpoPortraitArr[1]); // 웃음
        portraitData.Add(3000 + 5, teapotpoPortraitArr[2]); // 황당
        portraitData.Add(3000 + 6, teapotpoPortraitArr[3]); // 어지러움
        portraitData.Add(3000 + 7, teapotpoPortraitArr[4]); // 화남
        portraitData.Add(3000 + 8, nullPortrait);

        // Quest Talk
        talkData.Add(10 + 0, new string[] { "이야... 그렇게 곤충들이 공격할 줄은 몰랐는데:2", "우와앗! 괜찮으세요??!:7",
            "오! 티폿포! 마중 나와준거야?:1", "인사보단 몸은 괜찮으세요??!:7", "곤충분들이 공격해왔다고 들었어요!!:8",
            "그렇소! 아주 큰일이오...!:3", "어서 왕녀님에게 알려야...:3", "... 쿨쿨...:4", "지금 잘 때가 아니에욧!!:2",
            "우옷..!!:5", "어째서 곤충분들이 그렇게 된건가요..?:6", "아우우.. 그러게 말이야...:2", "원인을 조사해 볼 필요가 있겠어:0",
            "소인 생각에는 아마...:3", "... 쿨쿨...:4", "저 양반은 냅두고...:2", "일단 왕녀님한테 보고를 해야겠지?:0",
            "왕녀님에게 말하기 전에 일단 자세한 정황을 살펴볼 필요가 있다고 생각하는데요...:6", "확실히 일의 원인을 알아내고 보고 드리는 편이 좋을 것 같기도...:0",
            "아우... 하지만 왕녀님 심부름도 해야하고, 두가지 일을 다 해낼 수 있을까...??:2", "벌레들의 왕인 '코카서스'님이 원인 인걸까요?:6",
            "그렇다고 하기엔 전날까지는 멀쩡했던 사람... 아니 곤충이었는데...:0", "일단은 코카서스 님과 이야기를 나눠봐야 할 것 같네요!:8",
            "코카서스 님이 다혈질인 곤충이긴 했어도 이 정도 까지는 아니었는데:0", "Zzz...:4", "빨리 일어나세요!! 한 시가 급하다고요!:2",
            "음...!! 알겠소!!:5", "얼른 코카서스라는 자에게 가보도록 하지!:3", "(뭐지...? 자면서 다 듣고있었어...?:2"});


    }

    public string GetTalk(int id, int talkIndex)
    {
        if (!talkData.ContainsKey(id))
        {
            // 퀘스트 맨 처음 대사마저 없을 때
            // 기본 대사를 출력  
            if (!talkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 100, talkIndex); // Get First Talk
            // 해당 퀘스트 진행 순서 대사가 없을 때
            // 퀘스트 맨 처음 대사를 가지고 옴
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
