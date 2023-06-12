using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ultimiate_UI_Anim : MonoBehaviour
{
    private float elapsedTime;
    private float duration = 1.0f;
    private float recent_Delay;
    private float delay = 1.0f;
    private bool isComplete;
    private Vector2 originPos;
    private Vector2 origin_targetPos;

    public RectTransform targetRectTransform;
    public Vector2 targetPosition;
    
    // Update is called once per frame
    void Start()
    {
        originPos = targetRectTransform.anchoredPosition;
        origin_targetPos = targetPosition;
    }

    void OnEnable()
    {
        elapsedTime = 0.0f;
        isComplete = false;
        recent_Delay = 0.0f;
    }

    void OnDisable()
    {
        targetRectTransform.anchoredPosition = originPos;
        targetPosition = origin_targetPos;
    }

    void Update()
    {
        // 보간 시간 계산
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // 보간된 위치 계산
        Vector2 currentPosition = targetRectTransform.anchoredPosition;
        Vector2 newPosition = Vector2.Lerp(currentPosition, targetPosition, t);
        if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            recent_Delay += Time.deltaTime;
            if(recent_Delay > delay && !isComplete)
            {
                isComplete = true;
                targetPosition = new Vector2(targetPosition.x - 1500.0f, targetPosition.y);
                elapsedTime = 0.0f;
                Invoke("hidingObject", 0.5f);
            }

        }

        // 위치 적용
        targetRectTransform.anchoredPosition = newPosition;
    }

    void hidingObject()
    {
        gameObject.SetActive(false);
    }
}
