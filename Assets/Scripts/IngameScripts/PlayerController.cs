using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Public Fields
    public int Health;
    public float Speed = 8.0f;
    public float BulletSpeed = 15.0f;
    public Stage_Data sd;
    public int Power;
    public GameObject bullet1;
    public float MaxDelay;
    public float DestroyBullet; //ÃÑÅº ÆÄ±« ½Ã°£
    #endregion

    #region Serialize Fields
    [SerializeField]
    private Sprite[] sprites = new Sprite[3];
    [SerializeField]
    float ShootDelay;
    #endregion

    #region MonoBehaviour Callbacks

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(xInput, yInput, 0);
        transform.position += dir * Time.deltaTime * Speed;

        MovingAnim(xInput); //ÇÃ·¹ÀÌ¾î ¾Ö´Ï¸ÞÀÌ¼Ç
        Fire();
        Reload();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, sd.LimitMin.x, sd.LimitMax.x), Mathf.Clamp(transform.position.y, sd.LimitMin.y, sd.LimitMax.y), transform.position.z);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy_Bullet")
        {
            Destroy(collision.gameObject);
            Health -= 1;
        }
    }

    #endregion

    #region Public Methods
    void MovingAnim(float Xdir)
    {
        if (Xdir >= 0.5)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (Xdir <= -0.5)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else
        {
            spriteRenderer.sprite = sprites[0];
        }

    }
    void Fire()
    {

        if (ShootDelay < MaxDelay)
            return;

        if (Input.GetButton("Fire1"))
        {
            if (Power == 1)
            {
                GameObject Bullet = Instantiate(bullet1, transform.position, transform.rotation);
                Rigidbody2D Brigid = Bullet.GetComponent<Rigidbody2D>();
                Brigid.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);

                Destroy(Bullet, DestroyBullet);
            }
            else if (Power == 2)
            {
                GameObject BulletL = Instantiate(bullet1, transform.position + Vector3.left * 0.5f, transform.rotation);
                GameObject BulletR = Instantiate(bullet1, transform.position + Vector3.right * 0.5f, transform.rotation);
                Rigidbody2D BrigidL = BulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D BrigidR = BulletR.GetComponent<Rigidbody2D>();
                BrigidL.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                BrigidR.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);

                Destroy(BulletL, DestroyBullet);
                Destroy(BulletR, DestroyBullet);
            }
            else if (Power == 3)
            {
                GameObject BulletL = Instantiate(bullet1, transform.position + Vector3.left * 0.5f, transform.rotation);
                GameObject BulletR = Instantiate(bullet1, transform.position + Vector3.right * 0.5f, transform.rotation);
                GameObject BulletCR = Instantiate(bullet1, transform.position + Vector3.right * 0.5f, Quaternion.Euler(new Vector3(0, 0, -45)));
                GameObject BulletCL = Instantiate(bullet1, transform.position + Vector3.left * 0.5f, Quaternion.Euler(new Vector3(0, 0, 45)));
                Rigidbody2D BrigidL = BulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D BrigidR = BulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D BrigidCR = BulletCR.GetComponent<Rigidbody2D>();
                Rigidbody2D BrigidCL = BulletCL.GetComponent<Rigidbody2D>();
                BrigidL.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                BrigidR.AddForce(Vector2.up * BulletSpeed, ForceMode2D.Impulse);
                BrigidCR.AddForce(Vector2.one * BulletSpeed, ForceMode2D.Impulse);
                BrigidCL.AddForce((Vector2.up + Vector2.left) * BulletSpeed, ForceMode2D.Impulse);

                Destroy(BulletL, DestroyBullet);
                Destroy(BulletR, DestroyBullet);
                Destroy(BulletCR, DestroyBullet - 0.5f);
                Destroy(BulletCL, DestroyBullet - 0.5f);
            }

            ShootDelay = 0;
        }
    }

    void Reload()
    {
        ShootDelay += Time.deltaTime;
    }
    #endregion

}