using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KDH.IngameWork.Ultimate_BulletShot {

    public class Ultimate_BulletShot : MonoBehaviour
    {
        public GameObject U_Bullet;
        public float U_Speed;
        private float shootDelay;
        private float maxDelay = 0.1f;
        public bool Ultimate_time = false;
        public Stage_Data sd;
        private PlayerController playerController;
        // Start is called before the first frame update
        void Start()
        {
            if(GameManager.Instance.isPlayerSurvive == true)
            {
                playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(playerController.isUlti)
            {
                shootDelay += Time.deltaTime;

                if (shootDelay > maxDelay)
                {
                    GameObject ins = Instantiate(U_Bullet, transform.position, Quaternion.identity);
                    Rigidbody2D rb = ins.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.up * U_Speed, ForceMode2D.Impulse);
                    Destroy(ins, 0.85f);
                    shootDelay = 0;
                }
            }
        }
    }
}