using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public int Stage;
    void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
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
