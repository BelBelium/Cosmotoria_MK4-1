using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace KDH.IngameWork.Enemy_BulletShot
{

    public class Enemy_BulletShot : MonoBehaviour
    {
        #region Public Fields
        public GameObject E_Bullet;
        public float Bullet_Speed;
        #endregion

        #region Serialize Fields
        [SerializeField]
        private Transform Player_Tr;
        #endregion

        #region Private Fields
        private Vector3 dir;
        private bool isShooting;
        private bool isPlayerSurvive;
        private float delay;
        private float ShootTime;
        #endregion

        void Start()
        {
            if (GameManager.Instance.isPlayerSurvive == true)
            {
                Player_Tr = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
            ShootTime = Random.Range(0.3f, 0.9f);
        }

        void Update()
        {
            if(Player_Tr == null)
            {
                return;
            }
            ShootBullet();
        }

        void ShootBullet()
        {
            delay += Time.deltaTime;
            if (!isShooting && delay >= ShootTime)
            {
                dir = Player_Tr.position - transform.position;
                dir.Normalize();
                GameObject bullet = Instantiate(E_Bullet, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(dir * Bullet_Speed, ForceMode2D.Impulse);

                Destroy(bullet, 2.3f);
                isShooting = true;
            }
        }
    }
}