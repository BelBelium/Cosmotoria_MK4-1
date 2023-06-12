using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(AudioSource))]

public class PlayerController : MonoBehaviour
{
    #region Private Fields
    private SpriteRenderer spriteRenderer;
    private bool isCoolTime;
    private bool Btn_Pointer;
    private PlayerBullet playerBullet;
    #endregion

    #region Public Fields
    public bool isUlti = false; //총격 중지
    public int Health;
    public float Speed = 8.0f;
    public float BulletSpeed = 15.0f;
    public Stage_Data sd;
    public int Boom;
    public GameObject bullet1;
    public GameObject[] Ultimit_pos;
    public GameObject Ulti_obj;
    public GameObject[] target = new GameObject[2];
    public GameObject Ulti_Time;
    public AudioClip[] Player_AC;
    public VariableJoystick joystick;
    public Health_UI health;
    public GameObject Ultimit_UI;
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
        KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerSurvive = true;
        //Player_audio = gameObject.GetComponent<AudioSource>();
        playerBullet = gameObject.GetComponent<PlayerBullet>();
    }


    void Update()
    {
        float xInput = joystick.Horizontal;
        float yInput = joystick.Vertical;
        if (KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart)
        {

            Vector3 dir = new Vector3(xInput, yInput, 0);
            transform.position += dir * Time.deltaTime * Speed;

            MovingAnim(xInput); //플레이어 애니메이션
            if (Btn_Pointer)
            {
                playerBullet.Fire();
            }
        }
    }

    private void LateUpdate()
    {
        if (KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, sd.LimitMin.x, sd.LimitMax.x), Mathf.Clamp(transform.position.y, sd.LimitMin.y, sd.LimitMax.y), transform.position.z);
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

    public void PoinerDown()
    {
        Btn_Pointer = true;
    }
    public void PointerUp()
    {
        Btn_Pointer = false;
    }

    public void Ultimit()
    {
        if (KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart && Boom > 0)
        {
            Debug.Log("실행완료");
            float moveTime = 0.4f;
            if (isCoolTime == false)
            {
                Boom -= 1;
                Ultimit_UI.gameObject.SetActive(true);
                health.Use_Boom();
                AudioSource.PlayClipAtPoint(Player_AC[1], transform.position);
                isUlti = true;
                isCoolTime = true;
                GameObject[] E_obj = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] B_obj = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
                foreach (GameObject des in E_obj)
                {
                    if(des.GetComponent<N1_Enemy_Controller>() != null)
                    {
                        des.GetComponent<N1_Enemy_Controller>().DestroyEffect();
                        N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(des);
                    }

                    else if(des.GetComponent<N2_Enemy_Controller>() != null)
                    {
                        des.GetComponent<N2_Enemy_Controller>().DestroyEffect();
                        des.GetComponent<N2_Enemy_Controller>().count.spawnCount -= 1;
                        N2_Enemy_ObjectPool.instance.DestroyEnemy_N2(des);
                    }
                }
                foreach (GameObject des in B_obj)
                {
                    Destroy(des);
                }
                StartCoroutine(Ultimit_Action(moveTime));

            }
        }
    }
    #endregion

    IEnumerator Ultimit_Action(float moveTime)
    {
        GameObject[] ulti_obj = new GameObject[5];
        int i = 0;
        Vector3[] vecVel = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
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
                ulti_obj[k].transform.position = Vector3.SmoothDamp(ulti_obj[k].transform.position, targetPos[k], ref vecVel[k], moveTime);
                if (Vector3.Distance(ulti_obj[k].transform.position, targetPos[k]) > 0.3f)
                {
                    allDone = false;
                }
            }
            if (allDone)
            {
                //Debug.Log("이동완료");
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
                        //Debug.Log("완료");
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
            if (isCoolTime == false)
            {
                //Debug.Log("빠져나옴");
                break;
            }
            yield return null;
        }
    }

}