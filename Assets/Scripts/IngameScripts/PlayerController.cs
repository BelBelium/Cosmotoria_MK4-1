using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields
    private SpriteRenderer spriteRenderer;
    private KDH.IngameWork.PlayerEffectManager.PlayerEffectManager effect;
    #endregion

    #region Public Fields
    public int Health;
    public float Speed = 8.0f;
    public float BulletSpeed = 15.0f;
    public Stage_Data sd;
    public int Power;
    public int Power_Gage;
    public int Boom;
    public GameObject bullet1;
    public float MaxDelay;
    public float DestroyBullet; //��ź �ı� �ð�
    public KDH.IngameWork.CameraShake.CameraShake cameraShake;
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
        effect = GetComponent<KDH.IngameWork.PlayerEffectManager.PlayerEffectManager>();
        GameManager.Instance.isPlayerSurvive = true;
    }


    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(xInput, yInput, 0);
        transform.position += dir * Time.deltaTime * Speed;

        MovingAnim(xInput); //�÷��̾� �ִϸ��̼�
        Fire();
        Reload();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] E_obj = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] B_obj = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
            foreach (GameObject des in E_obj)
            {
                Destroy(des);
            }
            foreach (GameObject des in B_obj)
            {
                Destroy(des);
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, sd.LimitMin.x, sd.LimitMax.x), Mathf.Clamp(transform.position.y, sd.LimitMin.y, sd.LimitMax.y), transform.position.z);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy_Bullet")
        {
            if (!effect.isInvincible)
            {
                cameraShake.Shake();
                effect.Invincible();
                Destroy(collision.gameObject);
                Health -= 1;
                if(Power >= 2)
                {
                    Power -= 1;
                    Power_Gage = 0;
                }

                if(Health == 0)
                {
                    GameManager.Instance.isPlayerSurvive = false;
                    Dead();
                }
            }
        }
        if(collision.gameObject.tag == "Item_Power")
        {
            if (Power < 3)
            {
                Power_Gage += 1;
                if (Power_Gage == Power)
                {
                    Power += 1;
                    Power_Gage = 0;
                }
            }
            Destroy(collision.gameObject);
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

    void Dead()
    {
        Destroy(gameObject);
    }
    #endregion

}