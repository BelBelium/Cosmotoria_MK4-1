using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Planet
{
    Planteria, CookieMonster, Gostoon
}


public class PlanetData : MonoBehaviour
{
    public static PlanetData instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            return;
    }

    public Planet currentPlanet;
}
