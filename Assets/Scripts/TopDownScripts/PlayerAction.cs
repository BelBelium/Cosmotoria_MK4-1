using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h, v;

    // 수평 Move 체크
    bool isHorizonMove;
    public float speed;
    public InGameManager manager;
    public Teleport teleport;

    Rigidbody2D rigidBody;
    Animator animator;
    GameObject scanObject;

    Vector3 dirVec;

    // 버튼 전용 함수
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;
    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        // PC 이동 값과 모바일 이동 값도 추가
        // PC, 모바일에서도 가능
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;


        // 버튼 다운 체크. 모바일, PC에서도 가능
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal") || right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical") || up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;


        // 수평 Move인가?
        if (hDown)
            isHorizonMove = true;
        else if (vDown)
            isHorizonMove = false;
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // 애니메이션
        if (animator.GetInteger("hAxisRaw") != h)
        {          
            animator.SetInteger("hAxisRaw", (int)h);
            animator.SetBool("isChange", true);
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetInteger("vAxisRaw", (int)v);
            animator.SetBool("isChange", true);
        }
        else
            animator.SetBool("isChange", false);

        // Direction
        if (vDown && v == 1)
            dirVec = Vector3.up;
        else if (vDown && v == -1)
            dirVec = Vector3.down;
        else if (hDown && h == 1)
            dirVec = Vector3.right;
        else if (hDown && h == -1)
            dirVec = Vector3.left;

        // ScanObject
        if (Input.GetButtonDown("Jump") && scanObject != null)
            manager.Scan(scanObject);

        // 모바일 값 초기화(Init)
        up_Down = false;
        down_Down = false;
        left_Down = false;
        right_Down = false;
        up_Up = false;
        down_Up = false;
        left_Up = false;
        right_Up = false;
    }

    private void FixedUpdate()
    {
        // Move
        Vector2 moveVector = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        rigidBody.velocity = moveVector * speed;


        // Ray
        Debug.DrawRay(rigidBody.position, dirVec * 0.8f, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigidBody.position, dirVec, 0.7f, 
            LayerMask.GetMask("Object"));

        if (rayHit.collider != null)
            scanObject = rayHit.collider.gameObject;
        else
            scanObject = null;
    }

    public void ButtonDown(string type)
    {
        switch (type) 
        {
            case "UP":
                up_Value = 1;
                up_Down = true;
                break;
            case "DOWN":
                down_Value = -1;
                down_Down = true;
                break;
            case "LEFT":
                left_Value = -1;
                left_Down = true;
                break;
            case "RIGHT":
                right_Value = 1;
                right_Down = true;
                break;
            case "ACTION":
                if (scanObject != null)
                    manager.Scan(scanObject);
                break;
            case "CANCEL":
                manager.SubMenuActive();
                break;
            case "TELEPORT":
                teleport.MoveSpawn();
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "UP":
                up_Value = 0;
                up_Up = true;
                break;
            case "DOWN":
                down_Value = 0;
                down_Up = true;
                break;
            case "LEFT":
                left_Value = 0;
                left_Up = true;
                break;
            case "RIGHT":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
