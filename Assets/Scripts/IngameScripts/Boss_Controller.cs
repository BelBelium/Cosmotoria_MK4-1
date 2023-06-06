using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Controller : MonoBehaviour
{
    public GameObject BossPrefab;
    public Transform BossPos;
    public GameObject BossHP_Slider;

    private GameObject Boss;
    private Vector3 velVec = Vector3.zero;
    private float currentDelay;
    private float appearDelay = 3.0f;
    private Vector3 targetPos;
    private bool moveComplete;

    void Awake()
    {
        currentDelay = 0;
        Boss = Instantiate(BossPrefab, BossPos.transform.position, Quaternion.identity);
        targetPos = new Vector3(Boss.transform.position.x, Boss.transform.position.y - 9.5f, Boss.transform.position.z);
    }

    void Update()
    {
        if (KDH.IngameWork.IngameManager.IngameManager.Instance.isBossStage)
        {
            currentDelay += Time.deltaTime;
            //Debug.Log(currentDelay);
            //보스출현.
            if (currentDelay >= appearDelay)
            {
                Boss.transform.position = Vector3.SmoothDamp(Boss.transform.position, targetPos, ref velVec, 0.5f);
                if (Vector3.Distance(Boss.transform.position, targetPos) <= 0.1f && !moveComplete)
                {
                    moveComplete = true;
                    KDH.IngameWork.IngameManager.IngameManager.Instance.isAppearBoss = true;
                    BossHP_Slider.gameObject.SetActive(true);
                    
                }
            }
        }
    }
}
