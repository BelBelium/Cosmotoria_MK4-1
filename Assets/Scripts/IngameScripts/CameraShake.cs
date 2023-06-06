using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KDH.IngameWork.CameraShake
{
    public class CameraShake : MonoBehaviour
    {
        #region Public Fields
        public float intensity;
        public float duration;
        #endregion
        public void Shake()
        {
            StartCoroutine(CameraShaking(intensity,duration));
        }

        IEnumerator CameraShaking(float intensity,float duration)
        {
            Vector3 originalPosition = transform.localPosition;
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.localPosition = originalPosition + Random.insideUnitSphere * intensity;
                yield return null;

                if (time >= duration)
                {
                    transform.localPosition = originalPosition;
                }
            }
        }
    }
}