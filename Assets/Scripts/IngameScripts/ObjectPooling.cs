using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public int Bullet_Count = 30;
    public GameObject bulletPrefab;

    private Queue<Bullet> bullet_Queue = new Queue<Bullet>();

    void Awake()
    {
        instance = this;
        InitializedBullet(Bullet_Count);
    }


    #region BulletObjectPooling

    private Bullet MakeBullet()
    {
        var obj = Instantiate(bulletPrefab, transform.position,Quaternion.identity).GetComponent<Bullet>();
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void InitializedBullet(int Count)
    {
        for(int i = 0; i < Bullet_Count; i++)
        {
            bullet_Queue.Enqueue(MakeBullet());
        }
    }

    public Bullet GetBullet()
    {
        if(bullet_Queue.Count > 0)
        {
            var obj = bullet_Queue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = MakeBullet();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public void DestroyBullet(Bullet bullet)
    {
        if (bullet.gameObject.activeInHierarchy)
        {
            bullet.gameObject.SetActive(false);
            bullet.transform.position = gameObject.transform.position;
            bullet_Queue.Enqueue(bullet);
        }
    }
    #endregion

}
