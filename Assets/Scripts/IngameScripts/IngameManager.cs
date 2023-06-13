using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public GameObject GameoverUI;
        public Fade BossWarning;
        public bool isBossStage;
        public bool isPlayerSurvive;
        public bool isPlayerStart = false;
        public bool isAppearBoss;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                isPlayerStart = false;
                isAppearBoss = false;
                isPlayerSurvive = false;
            }

        }

        void Update()
        {

            if(isPlayerSurvive == false)
            {
                GameoverUI.gameObject.SetActive(true);
            }

            if (SceneManager.GetActiveScene().name == "InGameScene" || SceneManager.GetActiveScene().name == "InGameScene_2")
            {
                if (timer.GetComponent<Image>().fillAmount == 0 && !isClear)
                {
                    isClear = true;

                    clearUI.gameObject.SetActive(true);

                    GameObject[] E_obj = GameObject.FindGameObjectsWithTag("Enemy");
                    GameObject[] B_obj = GameObject.FindGameObjectsWithTag("Enemy_Bullet");
                    foreach (GameObject des in E_obj)
                    {
                        if (des.GetComponent<N1_Enemy_Controller>() != null)
                        {
                            des.GetComponent<N1_Enemy_Controller>().DestroyEffect();
                            N1_Enemy_ObjectPool.instance.DestroyEnemy_N1(des);
                        }

                        else if (des.GetComponent<N2_Enemy_Controller>() != null)
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
                }
            }
            else
            {
                if (timer.GetComponent<Image>().fillAmount == 0 && !isBossStage)
                {
                    isBossStage = true;
                    BossWarning.stopOut = false;
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

        public void ReStart()
        {
            Loading.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnMain()
        {
            Loading.LoadScene("Planteria_TopDown");
        }
    }
}