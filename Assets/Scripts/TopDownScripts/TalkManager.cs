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
        // 플렌테리아
        // 플렌시아: 1000, 사보텐: 2000,
        talkData.Add(100, new string[] { "알아볼 수 없는 글씨로 쓰여져있다. 이게 뭐람..." });
        talkData.Add(1000, new string[] { "안녕....:0", "쿨쿨...:1" });
        talkData.Add(2000, new string[] { "(자고있다...):0", "쿨쿨...:0" });

        // Portrait Data
        // 플렌테리아
        portraitData.Add(0 + 0, fleryPortraitArr[0]);

        portraitData.Add(1000 + 0, fleryPortraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[0]);
        portraitData.Add(1000 + 2, portraitArr[1]);

        portraitData.Add(2000 + 0, fleryPortraitArr[0]);
        portraitData.Add(2000 + 1, portraitArr[2]);


        // Quest Talk
        talkData.Add(10 + 0, new string[] { "여왕님이 급한 일이라고 부르셨는데...:0", "또 시덥잖은 잔심부름이시려나?:0",
            "별일 아니었으면 좋겠는데...:0", "어쨌든 왕녀님한테 가볼까!:0" });

        talkData.Add(20 + 1000, new string[] { "Zzz...:2", "여왕님...?:0", "Zzz... Zzz...:2", 
        "여~~ 왕~~ 님~!!:0", "... 응.......?:1", "하암... 뭐야... 플레리잖아...?:1", "한참 잘자고 있는데 왜 깨운거야...:1",
        "아하하... 죄송해요...:0", "가 아니고! 여왕님이 급한 일이라고 저를 부르셨잖아요!:0", "음...?:1",
        "아, 그래 맞다......:1", "뭐 사실 그렇게 급한 일은 아니고:1", "(역시 그럴 줄 알았어...):0", "이걸 한번 봐바...:1"});

        talkData.Add(21 + 1000, new string[] { "이게 도대체 뭔가요?:0", "전설로만 등장하는 우주의 음식이라는데...:1", 
            "한번 먹으면 절대로 그 맛을 잊지 못한다는데...:1", "그렇군요:0", "그걸 네가 좀 가져와줘:1", "(역시나...):0",
        "네, 알겠습니다 왕녀님!:0", "그럼... 잘 부탁해...:1", ".........:1", "Zzz....:2", "(다시 자버렸다.):0",
        "일단 나가볼까...:0"});

        talkData.Add(30 + 1000, new string[] { "(일어날 기미가 보이지 않는다...):1" });
        talkData.Add(30 + 0, new string[] { "흐음... 그런데 혼자서 할 수 있을까?:0", "Zzz... 쿨쿨...",
            "응? 분명 왕국 밖으로 나와서 왕녀님은 안계실텐데...?:0", "왜 코고는 소리가 나는거지?:0"});
        talkData.Add(30 + 2000, new string[] { "Zzz... 쿨쿨...:1", "이 사람이구나!:0", "한번 도와달라고 부탁 해봐야겠다!:0",
        "Zzz... 쿨쿨...:1", "저... 저기요!:0", "음!:1"});

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
