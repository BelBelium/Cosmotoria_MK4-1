using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Controller : MonoBehaviour
{
    private Transform targetPos;
    private float ShootRate = 2.0f;
    public GameObject Bullet;
    public float Minion_HP = 10.0f;
    public GameObject Des_Effect;
    public GameObject I_Power;

    Coroutine MinionCor;

    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").transform;
        if(MinionCor == null)
        {
            MinionCor = StartCoroutine(Minion_Shot());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Minion_Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(ShootRate);
            GameObject clone = Instantiate(Bullet, transform.position, Quaternion.identity);
            Vector3 dir = targetPos.transform.position - clone.transform.position;
            clone.GetComponent<Movement2D>().MoveTo(dir.normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Minion_HP -= 1;
            if(Minion_HP <= 0)
            {
                if (ItemDropManager.Drop_Percent(1.0f))
                {
                    DropItem();
                }
                StopCoroutine(MinionCor);
                MinionCor = null;
                DestroyEffect();
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

    public void DestroyEffect()
    {
        GameObject Des_Ins = Instantiate(Des_Effect, transform.position, Quaternion.identity);
        Des_Ins.GetComponent<AudioSource>().Play();
        Destroy(Des_Ins, 0.5f);
    }
}
