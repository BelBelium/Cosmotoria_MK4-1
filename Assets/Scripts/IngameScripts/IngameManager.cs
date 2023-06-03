using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.IngameWork.IngameManager
{
    public class IngameManager : MonoBehaviour
    {
        public static IngameManager Instance;
        public Image timer;
        public bool isClear = false;
        public GameObject clearUI;
        public Fade fadeImage;
        public GameObject playerBoom;
        public GameObject playerHP;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        void Update()
        {
            if(timer.GetComponent<Image>().fillAmount == 0 && !isClear)
            {
                isClear = true;
                clearUI.gameObject.SetActive(true);

                GameObject[] E_obj = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] B_obj = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
                foreach (GameObject des in E_obj)
                {
                    des.GetComponent<N1_Enemy_Controller>().DestroyEffect();
                    N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(des);
                }
                foreach (GameObject des in B_obj)
                {
                    Destroy(des);
                }
            }


            if (clearUI.GetComponent<Clear_UI>().isFinish)
            {
                playerBoom.gameObject.SetActive(false);
                playerHP.gameObject.SetActive(false);
                fadeImage.stopOut = false;
            }

            if(fadeImage.time > 2.0f)
            {
                Loading.LoadScene("Planteria_TopDown");
            }
        }
    }
}