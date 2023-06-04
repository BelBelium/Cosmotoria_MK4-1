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
        targetPos = new Vector3(transform.position.x, transform.position.y - 90.0f, transform.position.z);
        GaugeImage = Gauge.GetComponent<Image>();
        OriginalPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart == true)
        {
            if (!gameStart)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velVec, moveTime);
                if (Vector3.Distance(transform.position, targetPos) < 0.1f)
                {
                    gameStart = true;
                }
            }
            GaugeImage.fillAmount -= Time.deltaTime / 30.0f;


            
            if(GaugeImage.fillAmount == 0)
            {
                transform.position = Vector3.SmoothDamp(transform.position, OriginalPos, ref velVec, moveTime);
            }
        }
    }
}
