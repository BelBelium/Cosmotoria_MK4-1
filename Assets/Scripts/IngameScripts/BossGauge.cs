using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGauge : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 velVec = Vector3.zero;
    Vector3 OriginalPos;
    float moveTime = 0.4f;
    public GameObject Gauge;
    private Image GaugeImage;
    private bool gameStart;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3(transform.position.x, transform.position.y - 30.0f, transform.position.z);
        GaugeImage = Gauge.GetComponent<Image>();
        OriginalPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isPlayerStart == true)
        {
            if (!gameStart)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velVec, moveTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    gameStart = true;
                }
            }
            GaugeImage.fillAmount -= Time.deltaTime / 25.0f;


            if(GameManager.Instance.Stage == 11 && GaugeImage.fillAmount == 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, OriginalPos, ref velVec, moveTime);
            }
            else if(GameManager.Instance.Stage == 22 && GaugeImage.fillAmount == 0)
            {
                //게임 클리어
            }
            else if (GameManager.Instance.Stage == 33 && GaugeImage.fillAmount == 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, OriginalPos, ref velVec, moveTime);
            }
        }
    }
}
