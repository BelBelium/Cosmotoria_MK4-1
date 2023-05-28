using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
