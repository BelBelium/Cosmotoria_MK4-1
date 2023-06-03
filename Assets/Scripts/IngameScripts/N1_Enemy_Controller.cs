using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_Enemy_Controller : MonoBehaviour
{
    public float Speed = 8.0f;
    public int HP = 3;
    public EnemyHitEffect EnemyHitEffect;
    public GameObject I_Power;
    public GameObject Des_Effect;
    public Stage_Data sd;

    private AudioSource E_audio;

    // Start is called before the first frame update
    void Start()
    {
        E_audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        if (transform.position.y < sd.LimitMin.y - 1.0f)
        {
            N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ultimate_Bullet")
        {
            DestroyEffect();
            N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 1;
            EnemyHitEffect.recent_Delay = 0;
            
            if(HP <= 0)
            {
                if (ItemDropManager.Drop_Percent(0.3f))
                {
                    DropItem();
                }
                DestroyEffect();
                N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(gameObject);
            }
        }
    }


    void DropItem()
    {
        GameObject ins_Power = Instantiate(I_Power, transform.position, Quaternion.identity);
        Rigidbody2D rb = ins_Power.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.up * 3.0f, ForceMode2D.Impulse);
    }

    public void DestroyEffect()
    {
        GameObject Des_Ins = Instantiate(Des_Effect, transform.position, Quaternion.identity);
        Des_Ins.GetComponent<AudioSource>().Play();
        Destroy(Des_Ins, 0.5f);
    }
}
