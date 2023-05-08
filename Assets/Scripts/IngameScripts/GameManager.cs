using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public bool isPlayerSurvive;
    public bool isPlayerStart = false;
    public int Stage;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void GameStart()
    {
        StartCoroutine("EntryMain");
    }

    public void ReStart()
    {

    }


    IEnumerator EntryMain()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Ingame");
    }
}
