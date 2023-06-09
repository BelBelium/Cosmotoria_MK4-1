using KDH.IngameWork.IngameManager;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatterns : MonoBehaviour
{
    #region Public Fields
    public bool ExitPattern;
    public GameObject Bullet;
    public GameObject Boom_Bullet;
    public GameObject Minions;
    public float bullet_Speed = 10.0f;
    public Transform targetPos;
    public Transform[] minions_pos;
    public GameObject BossMode2;
    public GameObject DangerousEffect;
    public bool bossIsClear;
    public GameObject[] Des_Effect = new GameObject[5];
    #endregion

    #region Private Fields
    [SerializeField]
    private float Boss_HP = 1000.0f;
    private float HP_origin;
    private int Pattern_Num = -1;
    private int Previous_Num;
    private Coroutine enumerator;
    private Coroutine bossMode2_Cor;
    private Coroutine bossMode3_Cor;
    private float Delay = 2.0f;
    private float currentDelay;
    private bool bossMode2 = false;
    private bool berserk = false;
    private PlayerController playerController;
    private float intensity = 0.6f;
    private float duration = 0.5f;
    #endregion

    public float boss_HP => Boss_HP;
    public float hp_origin => HP_origin;

    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        HP_origin = Boss_HP;
    }

    void Update()
    {
        if (KDH.IngameWork.IngameManager.IngameManager.Instance.isAppearBoss)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= Delay)
            {
                BossPattern();
                Debug.Log("실행중");
            }
        }

        if(Boss_HP <= (HP_origin * 0.6f) && !bossMode2)
        {
            bossMode2 = true;
            bossMode2_Cor = StartCoroutine(SummonMinion());
        }

        if(Boss_HP <= (HP_origin * 0.3f) && !berserk)
        {
            berserk = true;
            StopCoroutine(bossMode2_Cor);
            bossMode2_Cor = null;
            bossMode2_Cor = StartCoroutine(SummonMinion());
            bossMode3_Cor = StartCoroutine(ShockWavePattern());
        }

        if(Boss_HP<= 0 && !bossIsClear)
        {
            bossIsClear = true;

            StopCoroutine(bossMode3_Cor);
            StopCoroutine(enumerator);
            enumerator = null;
            bossMode3_Cor = null;
            KDH.IngameWork.IngameManager.IngameManager.Instance.isAppearBoss = false;
            StartCoroutine(BossDestroyAction(intensity, duration));

            GameObject[] B_obj = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
            GameObject[] M_obj = GameObject.FindGameObjectsWithTag("Minion");
            foreach (GameObject des in B_obj)
            {
                Destroy(des);
            }
            foreach (GameObject des in M_obj)
            {
                Destroy(des);
            }

            KDH.IngameWork.IngameManager.IngameManager.Instance.BossClear();
        }
    }



    void BossPattern()
    {
        if (enumerator != null && ExitPattern) // 현재 enumerator 안에 돌고 있는 코루틴이 있다면 
        {
            StopCoroutine(enumerator);
            enumerator = null;
            ExitPattern = false;
        }


        if (enumerator == null)
        { //현재 enumerator 안에 돌고 있는 코루틴이 없으면
            Pattern_Num = Random.Range(0, 3);
            if (Previous_Num == Pattern_Num)
            {
                Debug.Log("이전 패턴과 똑같음. 재실행");
                return;
            }
            switch (Pattern_Num)
            {
                case 0:
                    enumerator = StartCoroutine(WavePattern());
                    break;
                case 1:
                    enumerator = StartCoroutine(BurstPattern());
                    break;
                case 2:
                    enumerator = StartCoroutine(BoomBulletPattern());
                    break;
            }
            Previous_Num = Pattern_Num;
        }
    }


    IEnumerator SummonMinion()
    {
        Vector3[] velVec = new Vector3[4] {Vector3.zero, Vector3.zero , Vector3.zero , Vector3.zero };
        int moveComplete = 0;
        Vector3 pos1 = new Vector3(minions_pos[0].transform.position.x, minions_pos[0].transform.position.y, minions_pos[0].transform.position.z);
        Vector3 pos2 = new Vector3(minions_pos[1].transform.position.x, minions_pos[1].transform.position.y, minions_pos[1].transform.position.z);
        Vector3 pos3 = new Vector3(minions_pos[2].transform.position.x, minions_pos[2].transform.position.y, minions_pos[2].transform.position.z);
        Vector3 pos4 = new Vector3(minions_pos[3].transform.position.x, minions_pos[3].transform.position.y, minions_pos[3].transform.position.z);
        GameObject mini1 = Instantiate(Minions, transform.position, Quaternion.identity);
        GameObject mini2 = Instantiate(Minions, transform.position, Quaternion.identity);
        GameObject mini3 = Instantiate(Minions, transform.position, Quaternion.identity);
        GameObject mini4 = Instantiate(Minions, transform.position, Quaternion.identity);
        while (true) {

            mini1.transform.position = Vector3.SmoothDamp(mini1.transform.position, pos1,ref velVec[0],0.7f);
            if (Vector3.Distance(mini1.transform.position, minions_pos[0].position) < 0.1f && moveComplete == 0)
            {
                Debug.Log("하수인 이동완료");
                moveComplete++;
            }
            mini2.transform.position = Vector3.SmoothDamp(mini2.transform.position, pos2, ref velVec[1], 0.7f);
            if (Vector3.Distance(mini2.transform.position, minions_pos[1].position) < 0.1f && moveComplete == 1)
            {
                moveComplete++;
            }
            mini3.transform.position = Vector3.SmoothDamp(mini3.transform.position, pos3, ref velVec[2], 0.7f);
            if (Vector3.Distance(mini3.transform.position, minions_pos[2].position) < 0.1f && moveComplete == 2 )
            {
                moveComplete++;
            }

            mini4.transform.position = Vector3.SmoothDamp(mini4.transform.position, pos4, ref velVec[3], 0.7f);
            if (Vector3.Distance(mini4.transform.position, minions_pos[3].position) < 0.1f && moveComplete == 3)
            {
                moveComplete++;
            }

            if(moveComplete >= 4)
            {
                break;
            }

            yield return null;
        }
        yield return null;
    }
    IEnumerator WavePattern()
    {
        float startAngle = 180.0f;
        float angleAim = 0.0f;
        float angle = 0.0f;
        int recycle = 0;
        bool isMoveLeft = false;
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            GameObject WaveBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            float x = Mathf.Cos((angle+angleAim) + (startAngle * Mathf.PI / 180.0f));
            float y = Mathf.Sin((angle+angleAim) + (startAngle * Mathf.PI / 180.0f));
            //angle += 0.3f;
            Vector3 dir = new Vector3(x, y, 0);
            
            dir.Normalize();
            WaveBullet.GetComponent<Movement2D>().MoveTo(dir*bullet_Speed);
            if (isMoveLeft)
            {
                angle -= 0.3f;
            }
            else
            {
                angle += 0.3f;
            }

            if(angle >= 2.5f)
            {
                angleAim += 0.4f;
                isMoveLeft = true;
                recycle += 1;
            }
            if(angle <= 0.0f)
            {
                isMoveLeft = false;
                recycle += 1;
                
            }

            yield return new WaitForSeconds(0.1f);

            if (recycle == 5)
            {
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);

        Debug.Log("물결 종료");
        ExitPattern = true;
    }
    IEnumerator BurstPattern()
    {
        Debug.Log("점사패턴입니다.");
        int burstCount = 0;
        int burst = 0;
        bool isTargeting = false;
        Vector3 target = targetPos.position;
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (!isTargeting)
            {
                target = targetPos.position;
                isTargeting = true;
            }
            Vector3 dir = target - transform.position;
            GameObject bullet = Instantiate(Bullet,transform.position,Quaternion.identity);
            bullet.GetComponent<Movement2D>().MoveTo(dir.normalized * bullet_Speed);

            yield return new WaitForSeconds(0.1f);
            burstCount++;
            if(burstCount >= 5)
            {
                isTargeting = false;
                burst++;
                yield return new WaitForSeconds(0.9f);
                burstCount = 0;
            }

            if(burst == 5)
            {
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);

        Debug.Log("점사 종료");
        ExitPattern = true;
    }
    IEnumerator BoomBulletPattern()
    {
        Debug.Log("폭탄총알 패턴입니다.");
        Vector3 RandomBoomPos;
        Vector3 vecVel = Vector3.zero;
        Vector3 BoomPos;
        Vector3[] boomVec = new Vector3[8];
        float moveTime = 0.28f;
        int BoomCount = 0;
        float boomAngle = 360.0f / 8.0f;
        float angleRot = 0.0f;
        //float circleAngle = 0.0f;
        GameObject[] shotBullets = new GameObject[8];
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if(playerController.isUlti == true)
            {
                break;
            }
            RandomBoomPos = new Vector3(Random.Range(-2.0f,2.0f), Random.Range(-4.0f,3.0f), 0);
            //Debug.Log(RandomBoomPos);
            GameObject Boom = Instantiate(Boom_Bullet,transform.position,Quaternion.identity);
            while (true)
            {
                Boom.transform.position = Vector3.SmoothDamp(Boom.transform.position, RandomBoomPos, ref vecVel, moveTime);
                if (Vector3.Distance(Boom.transform.position, RandomBoomPos) < 0.1f)
                {
                    BoomPos = Boom.transform.position;
                    Destroy(Boom,0.5f);
                    break;
                }
                yield return null;
            }

            for (int i = 0; i < 8; i++)
            {
                shotBullets[i] = Instantiate(Bullet, BoomPos, Quaternion.identity);
                float x = Mathf.Cos(angleRot + i * boomAngle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angleRot + i * boomAngle * Mathf.PI / 180.0f);
                boomVec[i] = new Vector3(x, y, 0);
                shotBullets[i].GetComponent<Movement2D>().MoveTo(boomVec[i].normalized);
            }



            yield return new WaitForSeconds(0.4f);
            BoomCount++;
            angleRot += 0.3f;
            Debug.Log(BoomCount);
            if(BoomCount == 4)
            {
                break;
            }

        }
        yield return new WaitForSeconds(0.5f);

        Debug.Log("폭탄총알 종료");
        ExitPattern = true;
    }
    IEnumerator ShockWavePattern()
    {
        float attackRate = 1.5f;
        int count = 20;
        float startAngle = 180.0f;
        float interAngle = 360 / count;

        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            for(int i = 0; i < count/2; i++)
            {
                GameObject clone = Instantiate(BossMode2, transform.position, Quaternion.identity);
                float angle = startAngle + interAngle * i;
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
                clone.GetComponent<Movement2D>().MoveTo(new Vector3(x, y));
            }

            if(startAngle <= 180.0f)
            {
                startAngle += 10;
            }
            else
            {
                startAngle -= 10;
            }

            yield return new WaitForSeconds(attackRate);
        }
    }

    IEnumerator BossDestroyAction(float intensity, float duration)
    {
        Vector2 originalPosition = transform.localPosition;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            transform.localPosition = originalPosition + Random.insideUnitCircle * intensity;
            yield return null;

            if(time >= duration)
            {
                yield return new WaitForSeconds(0.1f);
                time = 0;
                DestroyEffect();
            }
        }
    }

    public void DestroyEffect()
    {
        Vector2 spawnEffectPos = new Vector2(Random.Range(gameObject.transform.position.x - 1.5f, gameObject.transform.position.x + 1.5f), Random.Range(gameObject.transform.position.y - 1.5f, gameObject.transform.position.y + 1.5f));
        GameObject Des_Ins = Instantiate(Des_Effect[Random.Range(0,5)], spawnEffectPos, Quaternion.identity);
        //Des_Ins.GetComponent<AudioSource>().Play();
        Destroy(Des_Ins, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GetComponentInChildren<EnemyHitEffect>().recent_Delay = 0;
            Boss_HP -= 1.0f;
        }

        if(collision.gameObject.tag == "Ultimate_Bullet")
        {
            GetComponentInChildren<EnemyHitEffect>().recent_Delay = 0;
            Boss_HP -= 0.5f;
        }
    }
}
