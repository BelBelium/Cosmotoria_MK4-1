using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime; // fadeTime 값이 10이면 1초 (값이 클수록 빠름)
    [SerializeField]
    private AnimationCurve fadeCurve; // 페이드 효과가 적용되는 알파 값을 곡선의 값으로 설정 
    private Image image;    // 페이드 효과에 사용되는 검은 바탕 이미지
    private FadeState fadeState;

    private void Awake()
    {
        image = GetComponent<Image>();

        // FadeIn 배경의 알파값이 1에서 0으로
        //StartCoroutine(Fade(1, 0));
        // FadeOut 배경의 알파값이 0에서 1로
        //StartCoroutine(Fade(0, 1));

        OnFade(FadeState.FadeIn);
    }

    //private void Update()
    //{
    //    // image의 color 프로퍼티는 a 변수만 따로 set이 불가능해서 변수에 저장
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
            case FadeState.FadeIn:  // FadeIn 배경의 알파값이 1에서 0으로
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut:    // FadeOut 배경의 알파값이 0에서 1로
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut:   // Fade 효과를 In -> Out 1회 반복
            case FadeState.FadeLoop:    // Fade 효과를 In -> Out 무한 반복
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            // 코루틴 내부에서 코루틴 함수를 호출하면 해당 코루틴 함수가 종료되어야 다음 문장 실행
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            yield return StartCoroutine(Fade(0, 1)); // Fade Out

            // 1회만 재생하는 상태일 때 break;
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
            // fadeTime으로 나누어서 fadeTime 시간 동안
            // percent 값이 0에서 1로 증가하도록 함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파값을 start부터 end까지 fadeTime 시간 동안 변화시킴
            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }
}
