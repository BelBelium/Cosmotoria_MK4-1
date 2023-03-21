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
        #endregion

        #region Public Fields
        public bool isInvincible;
        public float Invin_Time;
        #endregion

        void Start()
        {
            SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
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