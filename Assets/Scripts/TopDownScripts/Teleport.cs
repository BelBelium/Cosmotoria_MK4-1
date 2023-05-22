using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetPoint;
    public GameObject[] toPoint;

    public bool isTown = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetPoint = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targetPoint = null;
    }

    public void MoveSpawn()
    {
        if (isTown == true && targetPoint != null)
        {
            targetPoint.transform.position = toPoint[1].transform.position;
            isTown = false;
            Debug.Log(isTown);
        }

        else if (isTown == false && targetPoint != null)
        {
            targetPoint.transform.position = toPoint[0].transform.position;
            isTown = true;
            Debug.Log(isTown);
        }

    }       

}
