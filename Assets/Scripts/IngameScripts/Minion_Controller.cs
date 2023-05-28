using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_Controller : MonoBehaviour
{
    private Transform targetPos;
    private float ShootRate = 2.0f;
    public GameObject Bullet;
    public float Minion_HP = 10.0f;

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
            clone.GetComponent<BossMode2_Movement>().MoveTo(dir.normalized);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Minion_HP -= 1;
            if(Minion_HP <= 0)
            {
                StopCoroutine(MinionCor);
                MinionCor = null;
                Destroy(gameObject);
            }
        }
    }
}
