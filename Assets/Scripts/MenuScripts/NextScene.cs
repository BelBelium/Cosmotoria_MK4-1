using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public Planet planet;
    string planetName;

    private void Awake()
    {
    }


    public void SelectPlanet()
    {     
        ClickPlanet(PlanetData.instance.currentPlanet.ToString());
    }

    public void ClickPlanet(string planetName)
    {      
        Debug.Log(planetName);

        switch (planetName)
        {
            case "Planteria":
                SceneManager.LoadScene("Planteria_TopDown");
                break;
            case "CookieMonster":
                SceneManager.LoadScene("CookieMonster_TopDown");
                break;
            case "Gostoon":
                SceneManager.LoadScene("");
                break;
        }

    }
}
