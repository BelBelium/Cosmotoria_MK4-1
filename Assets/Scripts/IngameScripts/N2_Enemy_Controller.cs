using KDH.IngameWork.EnemySpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_Enemy_Controller : MonoBehaviour
{
    public GameObject Bullet;
    public Stage_Data moveData;
    public int HP;
    public float moveTime = 3.0f; //�̵��ϱ���� �ɸ� �ð�(��)
    public float recentTime = 3.0f; //ù �̵� ���� Ÿ�̸ӿ���.
    public float moveSpeed = 1.5f;
    public EnemyHitEffect EnemyHitEffect;
    public GameObject Des_Effect;
    public GameObject I_Power;
    public EnemySpawn count;

    private Transform targetPos;
    private Vector3 movePos;
    private Vector3 velVec = Vector3.zero;

    void Start()
    {
        count = GameObject.Find("EnemySpawner").GetComponent<EnemySpawn>();
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        movePos = new Vector3(Random.Range(moveData.LimitMin.x, moveData.LimitMax.x), Random.Range(moveData.LimitMin.y, moveData.LimitMax.y));
    }

    void Update()
    {
        if(recentTime > moveTime)
        {
            transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velVec, moveSpeed);

            if (Vector3.Distance(transform.position, movePos) < 0.1f)
            {
                recentTime = 0;
                movePos = new Vector3(Random.Range(moveData.LimitMin.x, moveData.LimitMax.x), Random.Range(moveData.LimitMin.y, moveData.LimitMax.y));
                GameObject clone2 = Instantiate(Bullet, transform.position, Quaternion.identity);
                Vector3 dir = targetPos.transform.position - clone2.transform.position;
                clone2.GetComponent<BossMode2_Movement>().MoveTo(dir.normalized);
            }
        }

        recentTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ultimate_Bullet")
        {
            DestroyEffect();
            Destroy(gameObject);
            count.spawnCount--;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            HP -= 1;
            EnemyHitEffect.recent_Delay = 0;

            if (HP <= 0)
            {
                if (ItemDropManager.Drop_Percent(0.12f))
                {
                    DropItem();
                }
                count.spawnCount--;
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
