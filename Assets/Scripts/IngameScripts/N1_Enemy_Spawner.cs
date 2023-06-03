using KDH.IngameWork.EnemySpawn;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class N1_Enemy_Spawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject N1_Enemy_Prefab;
    public Stage_Data stage;
    public Image timer;

    private Coroutine N1_EnemySpawner;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "InGameScene")
        {
            spawnRate = 1.0f;
        }
        N1_EnemySpawner = StartCoroutine(Normal_Enemy_Spawn());
    }

    void Update()
    {
        if (timer.GetComponent<Image>().fillAmount == 0 && N1_EnemySpawner != null)
        {
            StopCoroutine(N1_EnemySpawner);
            N1_EnemySpawner = null;
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
                var N1_Enemy = N1_Enemy_ObjectPool.instance.GetEnemy(SpawnPosition);
            }
            yield return new WaitForSeconds(spawnRate);
        }

    }
}
