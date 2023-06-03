using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float Speed;

    [SerializeField]
    Vector3 direction = Vector3.zero;

    void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    public void MoveTo(Vector3 dir)
    {
        direction = dir;
    }
}
