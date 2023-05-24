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
        // 플렌테리아
        // 플렌시아: 1000, 사보텐: 2000,
        talkData.Add(100, new string[] { "알아볼 수 없는 글씨로 쓰여져있다. 이게 뭐람..." });
        talkData.Add(200, new string[] { "[선인장 방목]", "아무 이유 없이 좌, 우로 흔드는 중." });
        talkData.Add(300, new string[] { "눈치 채셨을 수도 있지만 성이 대칭이 아닙니다." });
        talkData.Add(400, new string[] { "이미 누가 상자를 열고 안의 내용물을 가져가고 없다.", "남은 것이 있나 살펴보지만 역시나 없다." });
        talkData.Add(500, new string[] { "." });
        talkData.Add(1000, new string[] { "안녕....:3", "쿨쿨...:4" });
        talkData.Add(2000, new string[] { "(자고있다...):2", "쿨쿨...:4" });
        talkData.Add(3000, new string[] { "어라? 아직도 안가셨나요?:3", "아하하...! 좀만 있다가 가려고:1", "안돼죠!! 왕녀님이 기다리신다구요!:7",
        "아... 알겠어! 지금 가볼게!!:2", "(생각보다 무서운걸 티폿포...):0"});

        // Portrait Data
        // 플렌테리아
        portraitData.Add(0 + 0, fleryPortraitArr[0]); // 기본
        portraitData.Add(0 + 1, fleryPortraitArr[1]); // 기쁨
        portraitData.Add(0 + 2, fleryPortraitArr[2]); // 황당
        portraitData.Add(0 + 3, nullPortrait);

        portraitData.Add(1000 + 0, fleryPortraitArr[0]);
        portraitData.Add(1000 + 1, fleryPortraitArr[1]);
        portraitData.Add(1000 + 2, fleryPortraitArr[2]);
        portraitData.Add(1000 + 3, queenPortraitArr[0]); // 기본
        portraitData.Add(1000 + 4, queenPortraitArr[1]); // 자기
        portraitData.Add(1000 + 5, nullPortrait);

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
        talkData.Add(10 + 0, new string[] { "여왕님이 급한 일이라고 부르셨는데...:0", "또 시덥잖은 잔심부름이시려나?:0",
            "별일 아니었으면 좋겠는데...:0", "어쨌든 일단 길을 따라서 왕녀님한테 가볼까!:0" });

        talkData.Add(20 + 3000, new string[] { "티폿포~:1", "플레리 메이드님~ 무슨 일이시죠?:4", "아하하 그냥 놀러와봤어! ㅎㅎ:1",
        "아~ ㅎㅎ:4", "어! 근데 왕녀님이 플레리님을 찾고계셨어요!:7", "가봐야하지 않을까요?:3", "아 맞다 내 정신 좀 봐!:0",
        "그럼 이따가 다시 보자~!:1", "네~~ 다녀오세요!:4" });

        talkData.Add(20 + 1000, new string[] { "Zzz...:4", "여왕님...?:0", "Zzz... Zzz...:4", 
        "여~~ 왕~~ 님~!!:0", "... 응.......?:3", "하암... 뭐야... 플레리잖아...?:3", "한참 잘자고 있는데 왜 깨운거야...:3",
        "아하하... 죄송해요...:2", "가 아니고! 여왕님이 급한 일이라고 저를 부르셨잖아요!:0", "음...?:3",
        "아, 그래 맞다......:3", "뭐 사실 그렇게 급한 일은 아니고:3", "(역시 그럴 줄 알았어...):2", "이걸 한번 봐바...:3"});

        talkData.Add(21 + 1000, new string[] { "이게 도대체 뭔가요?:0", "전설로만 등장하는 우주의 음식이라는데...:3", 
            "한번 먹으면 절대로 그 맛을 잊지 못한다는데...:1", "그렇군요:0", "그걸 네가 좀 가져와줘:3", "(역시나...):0",
        "네, 알겠습니다 왕녀님!:1", "그럼... 잘 부탁해...:3", ".........:3", "Zzz....:4", "(다시 자버렸다.):0",
        "일단 나가볼까...:0"});
       
        talkData.Add(30 + 0, new string[] { "흐음... 그런데 혼자서 할 수 있을까?:0", "Zzz... 쿨쿨...:3",
            "응? 분명 왕국 밖으로 나와서 왕녀님은 안계실텐데...?:0", "왜 코고는 소리가 나는거지?:0"});
        talkData.Add(31 + 2000, new string[] { "Zzz... 쿨쿨...:4", "이 사람이구나!:0", "한번 도와달라고 부탁 해봐야겠다!:0",
        "Zzz... 쿨쿨...:4", "저... 저기요!:0", "음!:5", "아, 무슨일이시오?:3", "그... 혹시 이 음식이 어디있는지 알고 있으세요?:0",
        "(사보텐에게 음식 사진을 보여줬다.):6", "흠... 잘 모르겠구려, 난생 처음 보는 음식이옵네만:3", "아 그렇군요!:0",
        "도움이 못 돼서 미안하구려:3", "아니에요! 괜찮아요!:0", "그런데 혹시... 실례가 안된다면:0",
        "저랑 같이 이 음식을 찾는 것을 도와주실 수 있나요?:0", "좋소, 마침 심심하던 참이니:3", "(엄청 흔쾌한 성격이다?!):1",
        "가... 감사합니다! 그럼 지금부터 같이 찾으러 가봐요!:0", "쿨쿨...:4", "(그새 자고있어??!!):2", "일어나세욧!!:0",
        "어... 엇!:5", "알겠소...:3"});

        //talkData.Add(30 + 1000, new string[] { "(일어날 기미가 보이지 않는다...):1" });
        //talkData.Add(30 + 2000, new string[] { "여왕님이?:0", "그렇구만...:0" });
        //talkData.Add(31 + 2000, new string[] { "소인이 물건 하나를 잃어버려서 말일세...:0", "상자 하나만 찾아주시게나:0" });
        //talkData.Add(31 + 200, new string[] { "상자를 찾았다!" });
        //talkData.Add(32 + 2000, new string[] { "오오 고맙네!:0" });
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
