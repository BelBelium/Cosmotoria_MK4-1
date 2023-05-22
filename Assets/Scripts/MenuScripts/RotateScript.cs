using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}
