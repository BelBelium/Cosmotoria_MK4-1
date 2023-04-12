using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlanet : MonoBehaviour
{
    public Planet planet;
    public GameObject[] planetPanels;
    public string planetName;

    private void Start()
    {

    }

    private void Update()
    {
        planetName = planet.ToString();
    }

    private void OnMouseUpAsButton()
    {
        PlanetData.instance.currentPlanet = planet;
        OnSelect(planetName);
        OnDeselect(planetName);
        
    }

    void OnSelect(string planetName)
    {
        switch (planetName)
        {
            case "Planteria":
                planetPanels[0].SetActive(true);
                break;
            case "CookieMonster":
                planetPanels[1].SetActive(true);
                break;
            case "Gostoon":
                planetPanels[2].SetActive(true);
                break;
        }
    }
    
    void OnDeselect(string planetName)
    {
        switch (planetName)
        {
            case "Planteria":
                planetPanels[1].SetActive(false);
                planetPanels[2].SetActive(false);
                break;
            case "CookieMonster":
                planetPanels[0].SetActive(false);
                planetPanels[2].SetActive(false);
                break;
            case "Gostoon":
                planetPanels[0].SetActive(false);
                planetPanels[1].SetActive(false);
                break;
        }
    }
}
