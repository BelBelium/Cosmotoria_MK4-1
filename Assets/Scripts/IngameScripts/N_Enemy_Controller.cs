using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Enemy_Controller : MonoBehaviour
{
    public float Speed = 8.0f;
    public int HP = 3;
    public EnemyHitEffect EnemyHitEffect;
    public GameObject I_Power;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Speed * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ultimate_Bullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 1;
            Destroy(collision.gameObject);
            EnemyHitEffect.recent_Delay = 0;
            
            if(HP <= 0)
            {
                if (ItemDropManager.Drop_Percent(0.12f))
                {
                    DropItem();
                }
                Destroy(gameObject);
            }
        }
    }


    void DropItem()
    {
        GameObject ins_Power = Instantiate(I_Power, transform.position, Quaternion.identity);
        Rigidbody2D rb = ins_Power.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector3.up * 3.0f, ForceMode2D.Impulse);
    }
}
