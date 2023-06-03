using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N1_Enemy_ObjectPool : MonoBehaviour
{
    static public N1_Enemy_ObjectPool instance;

    public GameObject N1_Enemy_Prefab;
    public int N1_Enemy_Count = 10;

    private Queue<GameObject> N1_Enemy_Queue = new Queue<GameObject>();

    void Awake()
    {
        instance = this;
        InitializedEnemy_N1(N1_Enemy_Count);
    }

    #region N1_EnmeyObjectPooling

    private GameObject MakeEnemy_N1()
    {
        var obj = Instantiate(N1_Enemy_Prefab, transform.position, Quaternion.identity);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void InitializedEnemy_N1(int Count)
    {
        for (int i = 0; i < N1_Enemy_Count; i++)
        {
            N1_Enemy_Queue.Enqueue(MakeEnemy_N1());
        }
    }

    public GameObject GetEnemy(Vector3 Pos)
    {
        if (N1_Enemy_Queue.Count > 0)
        {
            var obj = N1_Enemy_Queue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.transform.position = Pos;
            return obj;
        }
        else
        {
            var newobj = MakeEnemy_N1();
            newobj.transform.SetParent(null);
            newobj.gameObject.SetActive(true);
            newobj.transform.position = Pos;
            return newobj;
        }
    }

    public void DestroyEnemy_N1(GameObject N1_Enemy)
    {
        if (N1_Enemy.gameObject.activeInHierarchy)
        {
            N1_Enemy.GetComponent<N1_Enemy_Controller>().HP = 3;
            N1_Enemy.gameObject.SetActive(false);
            N1_Enemy.transform.position = gameObject.transform.position;
            N1_Enemy_Queue.Enqueue(N1_Enemy);
        }
    }
    #endregion
}
