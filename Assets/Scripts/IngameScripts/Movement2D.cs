using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    public float Speed;
    public Stage_Data stage;

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

    void LateUpdate()
    {
        if(transform.position.x < stage.LimitMin.x - 1.0f ||
            transform.position.x > stage.LimitMax.x + 1.0f||
            transform.position.y < stage.LimitMin.y - 1.0f)
        {
            Destroy(gameObject);
        }
    }

    public void MoveTo(Vector3 dir)
    {
        direction = dir;
    }
}
