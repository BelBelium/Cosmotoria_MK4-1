using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.IngameWork.EnemySpawn
{
    public class EnemySpawn : MonoBehaviour
    {
        public Stage_Data stage;
        public GameObject N_Enemy;
        public Image timer;



        private GameObject Prefab;
        private Coroutine EnemySpawner;

        void Start()
        {
            if(GameManager.Instance.Stage == 11)
            {
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
            }
            else if(GameManager.Instance.Stage == 22)
            {
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
            }
            else
            {
                EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
            }
        }

        void Update()
        {
            if(timer.GetComponent<Image>().fillAmount == 0 && EnemySpawner != null)
            {
                StopCoroutine(EnemySpawner);
                EnemySpawner = null;
            }
        }

        IEnumerator Normal_Enemy_Spawn()
        {
            while (true)
            {
                if (GameManager.Instance.isPlayerStart)
                {
                    Vector3 SpawnPosition = new Vector3(Random.Range(stage.LimitMin.x, stage.LimitMax.x),
                                                        stage.LimitMax.y + 1);
                    Prefab = Instantiate(N_Enemy, SpawnPosition, transform.rotation);
                }
                yield return new WaitForSeconds(0.6f);
            }

        }
    }
}