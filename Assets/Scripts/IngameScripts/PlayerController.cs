using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Fields
    private SpriteRenderer spriteRenderer;
    private bool isCoolTime;
    #endregion

    #region Public Fields
    public bool isUlti = false; //총격 중지
    public int Health;
    public float Speed = 8.0f;
    public float BulletSpeed = 15.0f;
    public Stage_Data sd;
    public int Power;
    public int Power_Gage;
    public int Boom;
    public GameObject bullet1;
    public float MaxDelay;
    public float DestroyBullet; //총탄 파괴 시간
    public GameObject[] Ultimit_pos;
    public GameObject Ulti_obj;
    public GameObject[] target = new GameObject[2];
    public GameObject Ulti_Time;
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
        GameManager.Instance.isPlayerSurvive = true;
    }


    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(xInput, yInput, 0);
        transform.position += dir * Time.deltaTime * Speed;

        MovingAnim(xInput); //플레이어 애니메이션
        Fire();
        Reload();
        Ultimit();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, sd.LimitMin.x, sd.LimitMax.x), Mathf.Clamp(transform.position.y, sd.LimitMin.y, sd.LimitMax.y), transform.position.z);
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

    void Ultimit()
    {
        float moveTime = 0.4f;
        if (Input.GetKeyDown(KeyCode.Space) && isCoolTime == false)
        {
            isUlti = true;
            isCoolTime = true;
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
            StartCoroutine(Ultimit_Action(moveTime));
        }
    }
    #endregion


    IEnumerator Ultimit_Action(float moveTime)
    {
        GameObject[] ulti_obj = new GameObject[5];
        int i = 0;
        Vector3 vecVel = Vector3.zero;
        Vector3[] targetPos = new Vector3[ulti_obj.Length];
        Vector3[] Current = new Vector3[ulti_obj.Length];
        foreach (GameObject des in ulti_obj)
        {
            ulti_obj[i] = Instantiate(Ulti_obj, Ultimit_pos[i].transform.position, Quaternion.identity);
            targetPos[i] = new Vector3(ulti_obj[i].transform.position.x, target[0].transform.position.y, ulti_obj[i].transform.position.z);
            i++;
        }
        i = 0;
        while (true)
        {
            bool allDone = true;
            for (int k = 0; k < ulti_obj.Length; k++)
            {
                ulti_obj[k].transform.position = Vector3.SmoothDamp(ulti_obj[k].transform.position, targetPos[k], ref vecVel, moveTime);
                if (Vector3.Distance(ulti_obj[k].transform.position, targetPos[k]) > 0.1f)
                {
                    allDone = false;
                }
            }
            if (allDone)
            {
                Debug.Log("이동완료");
                break;
            }
            yield return null;
        }
        foreach(GameObject des in ulti_obj)
        {
            Current[i] = new Vector3(ulti_obj[i].transform.position.x, ulti_obj[i].transform.position.y, ulti_obj[i].transform.position.z);
            targetPos[i] = new Vector3(ulti_obj[i].transform.position.x, target[1].transform.position.y, ulti_obj[i].transform.position.z);
            i++;
        }
        i = 0;
        float move2 = 3.0f;
        float deltaMove = 0.1f;

        yield return new WaitForSeconds(1.5f);
        isUlti = false;

        yield return new WaitForSeconds(1.0f);
        foreach(GameObject des in ulti_obj)
        {
            ulti_obj[i].GetComponent<Rigidbody2D>().gravityScale = 1.5f;
            i++;
        }

        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            bool allDone = true;
            for (int k = 0; k < ulti_obj.Length; k++)
            {
                if (ulti_obj[k] != null && ulti_obj[k].GetComponent<Rigidbody2D>() != null)
                {
                    if (ulti_obj[k].GetComponent<Rigidbody2D>().gravityScale == 1.5f)
                    {
                        ulti_obj[k].GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                        Destroy(ulti_obj[k].gameObject, 0.6f);
                        Debug.Log("완료");
                    }
                    Vector3 newPosition = Vector3.Lerp(Current[k], targetPos[k], deltaMove / move2);
                    ulti_obj[k].GetComponent<Rigidbody2D>().MovePosition(newPosition);
                    if (Vector3.Distance(ulti_obj[k].transform.position, targetPos[k]) > 0.1f)
                    {
                        allDone = false;
                    }
                }
                if (allDone)
                {
                    isCoolTime = false;
                    break;
                }
                deltaMove += Time.deltaTime;
            }
            if (ulti_obj == null)
            {
                break;
            }
            yield return null;
        }
    }

}