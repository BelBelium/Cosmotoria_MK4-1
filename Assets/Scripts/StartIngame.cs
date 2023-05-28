using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIngame : MonoBehaviour
{
    public bool isTouch = false;

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
        Loading.LoadScene("InGameScene");
        GameManager.Instance.Stage = 22;
    }
}
