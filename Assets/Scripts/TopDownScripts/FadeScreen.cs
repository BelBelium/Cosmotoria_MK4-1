using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // fadeTime ���� 10�̸� 1�� (���� Ŭ���� ����)
    [SerializeField]
    private AnimationCurve fadeCurve; // ���̵� ȿ���� ����Ǵ� ���� ���� ��� ������ ���� 
    private Image image;    // ���̵� ȿ���� ���Ǵ� ���� ���� �̹���
    private FadeState fadeState;

    private void Awake()
    {
        image = GetComponent<Image>();

        // FadeIn ����� ���İ��� 1���� 0����
        //StartCoroutine(Fade(1, 0));
        // FadeOut ����� ���İ��� 0���� 1��
        //StartCoroutine(Fade(0, 1));

        OnFade(FadeState.FadeIn);
    }

    //private void Update()
    //{
    //    // image�� color ������Ƽ�� a ������ ���� set�� �Ұ����ؼ� ������ ����
    //    Color color = image.color;

    //    if (color.a > 0)
    //    {
    //        color.a -= Time.deltaTime;
    //    }

    //    image.color = color;
    //} 
    
    public void OnFade(FadeState state)
    {
        fadeState = state;

        switch (fadeState)
        {
            case FadeState.FadeIn:  // FadeIn ����� ���İ��� 1���� 0����
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:    // FadeOut ����� ���İ��� 0���� 1��
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:   // Fade ȿ���� In -> Out 1ȸ �ݺ�
            case FadeState.FadeLoop:    // Fade ȿ���� In -> Out ���� �ݺ�
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            // �ڷ�ƾ ���ο��� �ڷ�ƾ �Լ��� ȣ���ϸ� �ش� �ڷ�ƾ �Լ��� ����Ǿ�� ���� ���� ����
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            yield return StartCoroutine(Fade(0, 1)); // Fade Out

            // 1ȸ�� ����ϴ� ������ �� break;
            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }       
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime���� ����� fadeTime �ð� ����
            // percent ���� 0���� 1�� �����ϵ��� ��
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // ���İ��� start���� end���� fadeTime �ð� ���� ��ȭ��Ŵ
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
