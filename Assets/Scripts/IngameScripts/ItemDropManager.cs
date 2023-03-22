using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDropManager{

    public static bool Drop_Percent(float percentage)
    {
        if(percentage < 0.001f)
        {
            percentage = 0.001f;
        }
        bool isWin = false;
        int accuracy = 1000;
        float RandomRange = percentage * accuracy;
        int rand = Random.Range(1, accuracy + 1);

        if (rand <= RandomRange)
        {
            isWin = true;
        }
        return isWin;
    }
}
