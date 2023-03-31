using KDH.IngameWork.CameraShake;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KDH.IngameWork.PlayerEffectManager
{

    public class PlayerEffectManager : MonoBehaviour
    {
        #region Private Fields
        private SpriteRenderer SpriteRenderer;
        private float recent_Time;
        private PlayerController playerController;
        #endregion

        #region Public Fields
        public bool isInvincible;
        public float Invin_Time;
        public KDH.IngameWork.CameraShake.CameraShake cameraShake;
        #endregion

        #region MonoBehaviour Callbacks
        void Start()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (isInvincible)
            {
                recent_Time += Time.deltaTime;
            }
        }

        public void Invincible()
        {
            StartCoroutine("Player_Twinkle");
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy_Bullet")
            {
                if (!isInvincible)
                {
                    cameraShake.Shake();
                    Invincible();
                    Destroy(collision.gameObject);
                    playerController.Health -= 1;
                    if (playerController.Power >= 2)
                    {
                        playerController.Power -= 1;
                        playerController.Power_Gage = 0;
                    }

                    if (playerController.Health == 0)
                    {
                        GameManager.Instance.isPlayerSurvive = false;
                        Dead();
                    }
                }
            }
            if (collision.gameObject.tag == "Item_Power")
            {
                if (playerController.Power < 3)
                {
                    playerController.Power_Gage += 1;
                    if (playerController.Power_Gage == playerController.Power)
                    {
                        playerController.Power += 1;
                        playerController.Power_Gage = 0;
                    }
                }
                Destroy(collision.gameObject);
            }
        }
        #endregion


        #region Private Methods
        void Dead()
        {
            Destroy(gameObject);
        }
        #endregion


        IEnumerator Player_Twinkle()
        {
            isInvincible = true;
            while (recent_Time <= Invin_Time)
            {
                Color attackColor;
                if (SpriteRenderer.color.a == 1.0f)
                {
                    attackColor = new Color(255.0f, 255.0f, 255.0f, 0.5f);
                    SpriteRenderer.color = attackColor;
                    yield return new WaitForSeconds(0.1f);
                }
                attackColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);
                SpriteRenderer.color = attackColor;
                yield return new WaitForSeconds(0.1f);
            }
            recent_Time = 0;
            isInvincible = false;
        }
    }

}