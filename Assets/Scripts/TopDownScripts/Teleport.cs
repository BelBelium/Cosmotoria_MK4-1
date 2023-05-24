using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject targetPoint;
    public Transform[] toPoint;

    PlayerAction player;

    public bool isTown = true;

    public void Start()
    {
        player = GameObject.Find("Flery").GetComponent<PlayerAction>();
    }

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
        switch (player.teleportName.name)
        {
            case "TownTeleport":
                targetPoint.transform.position = toPoint[1].position;
                break;
            case "CastleTeleport":
                targetPoint.transform.position = toPoint[0].position;
                break;
            case "TeapotpoTeleport":
                targetPoint.transform.position = toPoint[3].position;
                break;
            case "TeapotpoToTown":
                targetPoint.transform.position = toPoint[2].position;
                break;
        }

    }

    //public void MoveSpawn()
    //{
    //    if (isTown == true && targetPoint != null)
    //    {
    //        targetPoint.transform.position = toPoint[1].transform.position;
    //        isTown = false;
    //        Debug.Log(isTown);
    //    }

    //    else if (isTown == false && targetPoint != null)
    //    {
    //        targetPoint.transform.position = toPoint[0].transform.position;
    //        isTown = true;
    //        Debug.Log(isTown);
    //    }

    //}       

}
