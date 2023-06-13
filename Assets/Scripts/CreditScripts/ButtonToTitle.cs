using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToTitle : MonoBehaviour
{
    public void TitleButton()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
