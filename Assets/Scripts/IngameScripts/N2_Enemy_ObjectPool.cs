using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N2_Enemy_ObjectPool : MonoBehaviour
{
    public static N2_Enemy_ObjectPool instance;
    public GameObject N2_Enemy_Prefab;
    public int N1_Enemy_Count;

    private Queue<GameObject> N2_Enemy_Queue = new Queue<GameObject>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        InitializedEnemy_N2(N1_Enemy_Count);
    }
    #region N2_EnmeyObjectPooling

    private GameObject MakeEnemy_N2()
    {
        var obj = Instantiate(N2_Enemy_Prefab, transform.position, Quaternion.identity);
        obj.gameObject.SetActive(false);
        return obj;
    }

    private void InitializedEnemy_N2(int Count)
    {
        for (int i = 0; i < N1_Enemy_Count; i++)
        {
            N2_Enemy_Queue.Enqueue(MakeEnemy_N2());
        }
    }

    public GameObject GetEnemy(Vector3 Pos)
    {
        if (N2_Enemy_Queue.Count > 0)
        {
            var obj = N2_Enemy_Queue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.transform.position = Pos;
            return obj;
        }
        else
        {
            var newobj = MakeEnemy_N2();
            newobj.transform.SetParent(null);
            newobj.gameObject.SetActive(true);
            newobj.transform.position = Pos;
            return newobj;
        }
    }

    public void DestroyEnemy_N2(GameObject N2_Enemy)
    {
        if (N2_Enemy.gameObject.activeInHierarchy)
        {
            N2_Enemy.GetComponent<N2_Enemy_Controller>().HP = 3;
            N2_Enemy.gameObject.SetActive(false);
            N2_Enemy.transform.position = gameObject.transform.position;
            N2_Enemy_Queue.Enqueue(N2_Enemy);
        }
    }
    #endregion


}
