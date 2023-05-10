using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGauge : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 velVec = Vector3.zero;
    float moveTime = 0.4f;
    private Image Gaugeimage;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3(transform.position.x, transform.position.y - 30.0f, transform.position.z);
        Gaugeimage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.isPlayerStart == true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos,ref velVec,moveTime);
            Gaugeimage.fillAmount -= Time.deltaTime / 25.0f;
            if(GameManager.Instance.Stage == 11 && Gaugeimage.fillAmount == 0)
            {
                //게임 클리어.
            }
            else if(GameManager.Instance.Stage == 12 && Gaugeimage.fillAmount == 0)
            {
                //게임 클리어
            }
            else if (GameManager.Instance.Stage == 13 && Gaugeimage.fillAmount == 0)
            {
                //보스 등장.
            }
        }
    }
}
