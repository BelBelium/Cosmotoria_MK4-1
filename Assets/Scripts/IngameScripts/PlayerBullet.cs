using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public int Power = 1;
    public int Power_Gage;
    private float ShootDelay;
    public float MaxDelay;
    public AudioSource Player_audio;

    void Awake()
    {
        Player_audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Reload();
    }
    public void Fire()
    {
        if (ShootDelay < MaxDelay)
            return;

        Player_audio.Play();
        if (Power == 1)
        {
            var Bullet = ObjectPooling.instance.GetBullet();
            Bullet.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z);

        }
        else if (Power == 2)
        {
            var BulletL = ObjectPooling.instance.GetBullet();
            var BulletR = ObjectPooling.instance.GetBullet();
            BulletL.transform.position = new Vector3(gameObject.transform.position.x - 0.35f, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z);
            BulletR.transform.position = new Vector3(gameObject.transform.position.x + 0.35f, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z);

        }
        else if (Power == 3)
        {
            var BulletL = ObjectPooling.instance.GetBullet();
            var BulletR = ObjectPooling.instance.GetBullet();
            var BulletCR = ObjectPooling.instance.GetBullet();
            var BulletCL = ObjectPooling.instance.GetBullet();
            BulletL.transform.position = new Vector3(gameObject.transform.position.x - 0.35f, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z);
            BulletR.transform.position = new Vector3(gameObject.transform.position.x + 0.35f, gameObject.transform.position.y + 0.8f, gameObject.transform.position.z);
            BulletCL.transform.position = new Vector3(gameObject.transform.position.x - 0.4f, gameObject.transform.position.y + 0.6f, gameObject.transform.position.z);
            BulletCR.transform.position = new Vector3(gameObject.transform.position.x + 0.4f, gameObject.transform.position.y + 0.6f, gameObject.transform.position.z);
            BulletCL.transform.rotation = Quaternion.Euler(0, 0, 40);
            BulletCR.transform.rotation = Quaternion.Euler(0, 0, -40);
            BulletCL.Shoot(true,true);
            BulletCR.Shoot(true,false);
            //Rigidbody2D BrigidL = BulletL.GetComponent<Rigidbody2D>();
            //Rigidbody2D BrigidR = BulletR.GetComponent<Rigidbody2D>();
            //Rigidbody2D BrigidCR = BulletCR.GetComponent<Rigidbody2D>();
            //Rigidbody2D BrigidCL = BulletCL.GetComponent<Rigidbody2D>();
            //BrigidL.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
            //BrigidR.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
            //BrigidCR.AddForce(Vector2.one * BulletSpeed, ForceMode2D.Impulse);
            //BrigidCL.AddForce((Vector2.up + Vector2.left) * BulletSpeed, ForceMode2D.Impulse);

        }
        ShootDelay = 0;
    }
    void Reload()
    {
        ShootDelay += Time.deltaTime;
    }
}
