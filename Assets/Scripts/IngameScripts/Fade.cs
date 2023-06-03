using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour

{
    public float animTime = 1.0f;         // Fade �ִϸ��̼� ��� �ð� (����:��).  
    public float fadeTime = 2.0f;
    private Image fadeImage;            // UGUI�� Image������Ʈ ���� ����.  

    public float start = 1f;           // Mathf.Lerp �޼ҵ��� ù��° ��.  
    public float end = 0f;             // Mathf.Lerp �޼ҵ��� �ι�° ��.  
    public float time = 0f;            // Mathf.Lerp �޼ҵ��� �ð� ��.  


    public bool stopIn = true; //false�϶� ����Ǵ°ǵ�, �ʱⰪ�� false�� �� ������ ���� �����Ҷ� ���̵������� ������...�װ� ������ true�� �ϸ��.
    public bool stopOut = true;

    void Awake()
    {
        // Image ������Ʈ�� �˻��ؼ� ���� ���� �� ����.  
        fadeImage = GetComponent<Image>();
    }

    void Update()
    {
        
        // ���������� = FadeIn �ִϸ��̼� ���.  
        if (stopIn == false && time <= fadeTime)
        {
            PlayFadeIn();
        }
        if (stopOut == false && time <= fadeTime)
        {
            PlayFadeOut();
        }
        
        if (time >= 2 && stopIn == false)
        {
            stopIn = true;
            time = 0;
            Debug.Log("StopIn");
        }
        /*
        if (time > 1 && stopOut == false)
        {
            stopIn = false; //�Ͼ�� ��ȯ�ǰ� ���� �� ��ȯ �� �ٽ� Ǯ�Ŷ� �־���. �׳� ���� �����Ÿ� ���� �ʿ� ����.
            stopOut = true;
            time = 0;
            Debug.Log("StopOut");
        }
   */

    }

    // ���->����
    public void PlayFadeIn()
    {
        // ��� �ð� ���.  
        // 2��(animTime)���� ����� �� �ֵ��� animTime���� ������.  
        time += Time.deltaTime / animTime;

        // Image ������Ʈ�� ���� �� �о����.  
        Color color = fadeImage.color;
        // ���� �� ���.  
        color.a = Mathf.Lerp(start, end, time);
        // ����� ���� �� �ٽ� ����.  
        fadeImage.color = color;
        // Debug.Log(time);
    }

    // ����->���
    public void PlayFadeOut()
    {
        //Debug.Log("����Ϸ�");
        stopOut = false;
        // ��� �ð� ���.  
        // 2��(animTime)���� ����� �� �ֵ��� animTime���� ������.  
        time += Time.deltaTime / animTime;
        Debug.Log(time);
        // Image ������Ʈ�� ���� �� �о����.  
        Color color = fadeImage.color;
        // ���� �� ���.  
        color.a = Mathf.Lerp(end, start, time);  //FadeIn���� �޸� start, end�� �ݴ��.
        // ����� ���� �� �ٽ� ����.  
        fadeImage.color = color;
    }

}
