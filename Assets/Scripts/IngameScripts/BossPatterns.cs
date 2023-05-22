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
    public float bullet_Speed = 10.0f;
    public Transform targetPos;
    #endregion

    #region Private Fields
    private float Boss_HP = 1000.0f;
    private int Pattern_Num = -1;
    private int Previous_Num;
    private Coroutine enumerator;
    private float Delay = 2.0f;
    private float currentDelay;

    #endregion

    void Start()
    {
        targetPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (GameManager.Instance.isAppearBoss)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay >= Delay)
            {
                BossPattern();
            }
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
            Pattern_Num = Random.Range(0, 6);
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
                case 3:
                    if (Boss_HP >= 300.0f)
                        return;
                    enumerator = StartCoroutine(ShockWavePattern());
                    break;
                case 4:
                    if (Boss_HP >= 300.0f)
                        return;
                    enumerator = StartCoroutine(SectorFormRaizerPattern());
                    break;
                case 5:
                    if (Boss_HP >= 300.0f)
                        return;
                    enumerator = StartCoroutine(LeftRightPattern());
                    break;
            }
            Previous_Num = Pattern_Num;
        }
    }

    IEnumerator WavePattern()
    {
        float startAngle = 180.0f;
        float angle = 0.0f;
        int recycle = 0;
        bool isMoveLeft = false;
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            GameObject WaveBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Rigidbody2D Wave_Rigid = WaveBullet.GetComponent<Rigidbody2D>();
            float x = Mathf.Cos(angle + (startAngle * Mathf.PI / 180.0f));
            float y = Mathf.Sin(angle + (startAngle * Mathf.PI / 180.0f));
            //angle += 0.3f;
            Vector3 dir = new Vector3(x, y, 0);
            
            dir.Normalize();
            Wave_Rigid.AddForce(dir * bullet_Speed, ForceMode2D.Impulse);
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
                isMoveLeft = true;
                recycle += 1;
            }
            if(angle <= 0.0f)
            {
                isMoveLeft = false;
                recycle += 1;
                
            }

            yield return new WaitForSeconds(0.15f);

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
        yield return new WaitForSeconds(1.0f);
        while (true)
        {
            if (!isTargeting)
            {
                target = targetPos.position;
                isTargeting = true;
            }
            Vector3 dir = target - transform.position;
            GameObject bullet = Instantiate(Bullet,transform.position,Quaternion.identity);
            Rigidbody2D rigid_2d = bullet.GetComponent<Rigidbody2D>();
            rigid_2d.AddForce(dir * 2.0f, ForceMode2D.Impulse);

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
        //float circleAngle = 0.0f;
        GameObject[] shotBullets = new GameObject[8];
        Rigidbody2D[] shotBullets_rigid = new Rigidbody2D[8];
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
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
                float x = Mathf.Cos(i * boomAngle * Mathf.PI / 180.0f);
                float y = Mathf.Sin(i * boomAngle * Mathf.PI / 180.0f);
                boomVec[i] = new Vector3(x, y, 0);
                shotBullets_rigid[i] = shotBullets[i].GetComponent<Rigidbody2D>();
            }

            for (int k = 0; k < shotBullets.Length; k++)
            {
                shotBullets_rigid[k].AddForce(boomVec[k]*4.0f, ForceMode2D.Impulse);
                yield return null;
            }


            yield return new WaitForSeconds(0.4f);
            BoomCount++;
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
        yield return new WaitForSeconds(1.0f);
        Debug.Log("파동 패턴입니다.");
        yield return new WaitForSeconds(3.0f);

        Debug.Log("파동 종료");
        ExitPattern = true;
    }
    IEnumerator SectorFormRaizerPattern()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("부채꼴레이저 패턴입니다.");
        yield return new WaitForSeconds(3.0f);

        Debug.Log("부채꼴레이저 종료");
        ExitPattern = true;
    }
    IEnumerator LeftRightPattern()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("좌우레이저 패턴입니다.");
        yield return new WaitForSeconds(3.0f);

        Debug.Log("좌우레이저 종료");
        ExitPattern = true;
    }
}
