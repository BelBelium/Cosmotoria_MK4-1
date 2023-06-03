using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear_UI : MonoBehaviour
{
    public RectTransform targetRectTransform;
    public Vector2 targetPosition;
    public float duration = 1f;
    public float delay = 1.5f;
    public bool isFinish;
    public float recent_Delay = 0.0f;
    private float elapsedTime = 0f;

    private void Update()
    {
        // ���� �ð� ���
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / duration);

        // ������ ��ġ ���
        Vector2 currentPosition = targetRectTransform.anchoredPosition;
        Vector2 newPosition = Vector2.Lerp(currentPosition, targetPosition, t);
        if(Vector2.Distance(currentPosition, targetPosition) < 0.1f)
        {
            recent_Delay += Time.deltaTime;
            if (recent_Delay > delay && !isFinish)
            {
                targetPosition = new Vector2(targetPosition.x - 1300.0f, targetPosition.y);
                isFinish = true;
            }
        }

        // ��ġ ����
        targetRectTransform.anchoredPosition = newPosition;
    }
}

