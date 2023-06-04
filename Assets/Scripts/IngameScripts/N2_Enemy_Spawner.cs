using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class N2_Enemy_Spawner : MonoBehaviour
{
    public float spawnRate;
    public Stage_Data stage;
    public Image timer;
    public int spawnCount = 0;


    private Coroutine N2_EnemySpawner;

    void Start()
    {
        spawnRate = 1.0f;

        N2_EnemySpawner = StartCoroutine(Smart_Enemy_Spawn());
    }

    void Update()
    {
        if (timer.GetComponent<Image>().fillAmount == 0 && N2_EnemySpawner != null)
        {
            StopCoroutine(N2_EnemySpawner);
            N2_EnemySpawner = null;
        }

    }
    IEnumerator Smart_Enemy_Spawn()
    {
        while (true)
        {
            if (KDH.IngameWork.IngameManager.IngameManager.Instance.isPlayerStart && spawnCount < 6)
            {
                Vector3 SpawnPosition = new Vector3((stage.LimitMin.x + stage.LimitMax.x) / 2, stage.LimitMax.y + 1);

                var N2_Enemy = N2_Enemy_ObjectPool.instance.GetEnemy(SpawnPosition);
                spawnCount++;
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }
}
