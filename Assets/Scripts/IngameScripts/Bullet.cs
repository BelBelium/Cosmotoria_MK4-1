using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 8.0f;
    private Vector3 targetTr;
    private bool isRot;
    private bool isLeft;

    public Stage_Data sd;

    public void Shoot(bool Rot,bool LorR)
    {
        isRot = Rot;
        isLeft = LorR;
    }
    private void OnDisable()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        isRot = false;
        isLeft = false;
    }

    void Update()
    {
        if (!isRot)
        {
            transform.position = transform.position + Vector3.up * (BulletSpeed * Time.deltaTime);
        }
        else if(isRot && !isLeft)
        {
            transform.position = transform.position + ((Vector3.up + Vector3.right) * (BulletSpeed * Time.deltaTime));
        }
        else
        {
            transform.position = transform.position + ((Vector3.up + Vector3.left) * (BulletSpeed * Time.deltaTime));
        }


      
    }

    void LateUpdate()
    {
        if(transform.position.y > sd.LimitMax.y + 0.5f ||
            transform.position.x < sd.LimitMin.x - 0.5f ||
            transform.position.x > sd.LimitMax.x + 0.5f)
        {
            DestroyBulletObj();
        }
    }

    void DestroyBulletObj()
    {
        ObjectPooling.instance.DestroyBullet(this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Enemy2" || collision.gameObject.tag == "Minion")
        {
            DestroyBulletObj();
        }  
    }
}
