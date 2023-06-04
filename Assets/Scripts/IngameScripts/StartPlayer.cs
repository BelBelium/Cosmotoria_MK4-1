using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class StartPlayer : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 targetPos;
    public GameObject Attack_Btn;
    public GameObject Boom_Btn;
    public GameObject JoyPad;
    IEnumerator Cor;
    void Start()
    {
        playerTransform = gameObject.GetComponent<Transform>();
        targetPos = new Vector3(playerTransform.position.x, playerTransform.position.y + 6.0f, playerTransform.position.z);
        Cor = PlayerStart();
        StartCoroutine(PlayerStart());
    }
    void Update()
    {
        if (Vector3.Distance(playerTransform.position, targetPos) < 0.1f)
        {
            StopCoroutine(Cor);
        }
    }

    IEnumerator PlayerStart()
    {
        float moveTime = 0.65f;
        Vector3 vecVel = Vector3.zero;
        while (true)
        {
            playerTransform.position = Vector3.SmoothDamp(playerTransform.transform.position, targetPos, ref vecVel, moveTime);
            if (Vector3.Distance(playerTransform.position, targetPos) < 0.1f)
            {
                KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart = true;
                Attack_Btn.SetActive(true);
                Boom_Btn.SetActive(true);
                JoyPad.SetActive(true);
                //Debug.Log("±» " + GameManager.Instance.isPlayerStart);
                break;
            }
            yield return null;
        }

    }

}
