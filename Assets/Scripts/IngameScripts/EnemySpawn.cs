using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KDH.IngameWork.EnemySpawn
{
    public class EnemySpawn : MonoBehaviour
    {
        public Stage_Data stage;
        public GameObject N_Enemy;
        private GameObject Prefab;

        void Start()
        {
            StartCoroutine("Normal_Enemy_Spawn");
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