using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : SingletonCore<PoolManager>
{
    [Serializable]
    public class PoolData
    {
        public string name;
        public GameObject prefab;
    }

    public List<PoolData> poolList = new List<PoolData>();
    
    // 이름으로 pool 검색
    private Dictionary<string, IObjectPool<GameObject>> poolsDic = new Dictionary<string, IObjectPool<GameObject>>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var poolData in poolList)
        {
            poolsDic[poolData.name] = new ObjectPool<GameObject>(
                createFunc: () => Instantiate(poolData.prefab), // 생성하는 기능
                actionOnGet:(obj) => obj.SetActive(true),   // 꺼내는 기능
                actionOnRelease:(obj) => obj.SetActive(false)); // 넣는 기능
        }        
    }

    public GameObject GetObject(string key)
    {
        if (!poolsDic.ContainsKey(key))
        {
            Debug.Log($"Pool {key} not found");
            return null;
        }
        
        GameObject obj = poolsDic[key].Get();
        return obj;
    }

    public void ReleaseObject(string key, GameObject obj)
    {
        if (!poolsDic.ContainsKey(key))
        {
            Debug.Log($"Pool {key} not found");
        }
        
        poolsDic[key].Release(obj);
    }
}
