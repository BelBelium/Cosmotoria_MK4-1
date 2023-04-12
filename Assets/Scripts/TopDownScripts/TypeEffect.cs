using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    public string targetMsg;
    public int textSpeed;
    public bool isAnim;
    public GameObject textCursor;

    TextMeshProUGUI msgText;
    AudioSource audioSource;

    int index;
    float interval;
    

    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }
    // 텍스트 시작
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        textCursor.SetActive(false);

        interval = 1.0f / textSpeed;
        isAnim = true;

        Invoke("Effecting", interval);
    }
    // 텍스트 중
    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index];
        // 텍스트 사운드
        // 공백과 .은 제외
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
        {
            audioSource.Play();
        }

        index++;


        Invoke("Effecting", interval);
    }
    // 텍스트 끝
    void EffectEnd()
    {
        isAnim = false;
        textCursor.SetActive(true);
    }
}
