using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{ 
    one, two, three, four
}

public class PlanteriaIntro : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject[] target;

    public InGameManager manager;

    public string autoTalk;

    public Target currentTarget;

    public void Update()
    {
        autoTalk = currentTarget.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(autoTalk)
        {
            case "one":
                if (collision.CompareTag("Player"))
                {
                    manager.Scan(target[0]);
                    Debug.Log("¥Í¿Ω");
                }
                break;

            case "two":
                if (collision.CompareTag("Player"))
                {
                    manager.Scan(target[1]);
                    Debug.Log("¥Í¿Ω");
                }
                break;
        }
    }
}
