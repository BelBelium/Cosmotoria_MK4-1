using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Number
{
    a, b, c
}

public class StartIngame : MonoBehaviour
{
    public bool isTouch = false;

    public Number number;
    public string stageNumber;

    private void Update()
    {
        stageNumber = number.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;
    }

    public void onClick()
    {
        switch (stageNumber)
        {
            case "a":
                GameManager.Instance.Stage = 11;
                Loading.LoadScene("InGameScene");
                Debug.Log("a 누름");
                break;
            case "b":
                GameManager.Instance.Stage = 22;
                Loading.LoadScene("InGameScene");
                Debug.Log("b 누름");
                break;
            case "c":
                GameManager.Instance.Stage = 33;
                Loading.LoadScene("InGameScene");
                Debug.Log("c 누름");
                break;
        }         
        
    }
}
