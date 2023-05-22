using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartIngame : MonoBehaviour
{
    public void onClick()
    {
        SceneManager.LoadScene("InGameScene");
        GameManager.Instance.Stage = 11;
    }
}
