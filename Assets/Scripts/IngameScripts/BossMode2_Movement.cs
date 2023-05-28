using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMode2_Movement : MonoBehaviour
{
    public float Speed;

    [SerializeField]
    Vector3 direction = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 dir)
    {
        direction = dir;
    }
}
