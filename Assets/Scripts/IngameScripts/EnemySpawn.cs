using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KDH.IngameWork.EnemySpawn
{
    public class EnemySpawn : MonoBehaviour
    {
        public Stage_Data stage;
        public GameObject N_Enemy;
        public GameObject S_Enemy;
        public Image timer;
        public GameObject BossPrefab;
        public Transform BossPos;
        public Fade BossWarning;
        public int spawnCount = 0;

        private float spawnRate;
        private Vector3 velVec = Vector3.zero;
        private Vector3 targetPos;
        private GameObject Boss;
        private float appearDelay = 3.0f;
        private float currentDelay;
        private GameObject Prefab;
        private Coroutine EnemySpawner;
        private Coroutine S_EnemySpawner;
        private bool isBossStage;

        void Start()
        {
            if(SceneManager.GetActiveScene().name == "IngameScene")
            {
                spawnRate = 1.0f;
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
                S_EnemySpawner = null;
            }
            else if(SceneManager.GetActiveScene().name == "IngameScene_2")
            {
                spawnRate = 2.5f;
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
                S_EnemySpawner = StartCoroutine(Smart_Enemy_Spawn());
            }
            else
            {
                spawnRate= 1.0f;
                Boss = Instantiate(BossPrefab, BossPos.transform.position, Quaternion.identity);
                targetPos = new Vector3(Boss.transform.position.x, Boss.transform.position.y - 9.5f, Boss.transform.position.z);
                //Boss.SetActive(false);
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
                S_EnemySpawner = StartCoroutine(Smart_Enemy_Spawn());

            }
        }

        void Update()
        {
            if (timer.GetComponent<Image>().fillAmount == 0 && EnemySpawner != null)
            {
                StopCoroutine(EnemySpawner);
                EnemySpawner = null;

                StopCoroutine(S_EnemySpawner);
                S_EnemySpawner = null;

                if (SceneManager.GetActiveScene().name == "IngameScene_Boss")
                {

                    isBossStage = true;
                    BossWarning.stopOut = false;
                }
                else
                {
                    //보스가 아닐시 결과창 출력.
                }
            }

            if (SceneManager.GetActiveScene().name == "IngameScene_Boss" && isBossStage)
            {
                currentDelay += Time.deltaTime;
                //Debug.Log(currentDelay);
                //보스출현.
                if (currentDelay >= appearDelay)
                {
                    Boss.transform.position = Vector3.SmoothDamp(Boss.transform.position, targetPos, ref velVec, 0.5f);
                    if (Vector3.Distance(Boss.transform.position, targetPos) <= 0.1f)
                    {
                        KDH.IngameWork.IngameManager.IngameManager.Instance.isAppearBoss = true;
                    }
                }
            }
        }
        IEnumerator Normal_Enemy_Spawn()
        {
            while (true)
            {
                if (KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart )
                {
                    Vector3 SpawnPosition = new Vector3(Random.Range(stage.LimitMin.x, stage.LimitMax.x),
                                                        stage.LimitMax.y + 1);
                    Prefab = Instantiate(N_Enemy, SpawnPosition, transform.rotation);
                }
                yield return new WaitForSeconds(spawnRate);
            }

        }

        IEnumerator Smart_Enemy_Spawn()
        {
            while (true)
            {
                if(KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart && spawnCount < 6)
                {
                    Vector3 SpawnPosition = new Vector3((stage.LimitMin.x + stage.LimitMax.x)/2,stage.LimitMax.y + 1);

                    Prefab = Instantiate(S_Enemy, SpawnPosition, transform.rotation);
                    spawnCount++;
                }

                yield return new WaitForSeconds(spawnRate+0.5f);
            }
        }
    }
}