using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameManager : MonoBehaviour
{
    public float textSpeed;
    public bool isAction;
    public int talkIndex;

    public QuestManager questManager;
    public Image portraitImage;
    public Image FleryImage;
    public TalkManager talkManager;
    public TypeEffect talk;
    public GameObject scanObject;
    public GameObject menuPanel;
    public GameObject player;
    public TextMeshProUGUI questText;
    public Animator portraitAnim;
    public Animator talkPanel;
    public Sprite prevPortrait; // �� �ʻ�ȭ�� ����

    void Start()
    {
        //GameLoad();
        questText.text = questManager.CheckQuest();

        //Invoke("IntroTalk", 2f);
    }

    void Update()
    {       
        if (Input.GetButtonDown("Cancel"))
            SubMenuActive();
    }

    public void SubMenuActive()
    {
        if (menuPanel.activeSelf)
            menuPanel.SetActive(false);
        else
            menuPanel.SetActive(true);
    }

    public void Scan(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjectData objectData = scanObject.GetComponent<ObjectData>();
        Talk(objectData.id, objectData.isNpc, objectData.objectName);

        talkPanel.SetBool("isShow", isAction);
    }

    void Talk(int id, bool isNpc, string objName)
    {
        // Set Talk Data
        
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }
        
        // ��ȭ ����
        if (talkData == null)
        {           
            isAction = false;
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return;
        }
        // ��ȭ ��
        if (isNpc)
        {
            talk.SetMsg(talkData.Split(':')[0]);
            // �ʻ�ȭ �����ֱ�
            portraitImage.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImage.color = new Color(1, 1, 1, 1);
            // �ʻ�ȭ �ִϸ��̼�
            if (prevPortrait != portraitImage.sprite)
            {
                portraitAnim.SetTrigger("doEffect");
                prevPortrait = portraitImage.sprite;
            }
            
        }
        else
        {
            talk.SetMsg(talkData);
            // �ʻ�ȭ �����
            portraitImage.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    //public void GameSave()
    //{
    //    PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
    //    PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
    //    PlayerPrefs.SetInt("QuestId", questManager.questId);
    //    PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
    //    PlayerPrefs.Save();

    //    menuPanel.SetActive(false);
    //}

    //public void GameLoad()
    //{
    //    // ���� ���� �������� �� �����Ͱ� ������ ����ó�� ����
    //    if (!PlayerPrefs.HasKey("PlayerX"))
    //    {
    //        return;
    //    }

    //    float x = PlayerPrefs.GetFloat("PlayerX");
    //    float y = PlayerPrefs.GetFloat("PlayerY");
    //    int questId = PlayerPrefs.GetInt("QuestId");
    //    int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

    //    player.transform.position = new Vector3(x, y, 0);
    //    questManager.questId = questId;
    //    questManager.questActionIndex = questActionIndex;
    //    questManager.ControlObject();
    //}

    public void GameExit()
    {
        Application.Quit();
    }

    public void GoInGame()
    {
        SceneManager.LoadScene("InGameScene");
    }
}