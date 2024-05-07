using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoSingleton<PoolManager>
{
    [Serializable]
    private class ObjectInfo
    {
        public string objectName;
        public PoolAble prefab;
        public int cnt;
    }

    public bool IsReady { get; private set; }

    [SerializeField]
    private ObjectInfo[] _objectInfos = null;

    private string _objectName;

    private Dictionary<string, IObjectPool<GameObject>> _objectPoolDictionary = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> _gameobjectDictionary = new Dictionary<string, GameObject>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        IsReady = false;

        for (int idx = 0; idx < _objectInfos.Length; idx++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, _objectInfos[idx].cnt, 1000);

            if (_gameobjectDictionary.ContainsKey(_objectInfos[idx].objectName))
            {
                continue;
            }

            GameObject prefab = _objectInfos[idx].prefab.gameObject;
            _gameobjectDictionary.Add(_objectInfos[idx].objectName, prefab);
            _objectPoolDictionary.Add(_objectInfos[idx].objectName, pool);

            for (int i = 0; i < _objectInfos[idx].cnt; i++)
            {
                _objectName = _objectInfos[idx].objectName;
                PoolAble poolAbleGameObject = CreatePooledItem().GetComponent<PoolAble>();
                poolAbleGameObject.transform.SetParent(transform);
                poolAbleGameObject.Pool.Release(poolAbleGameObject.gameObject);
            }
        }

        IsReady = true;
    }

    private GameObject CreatePooledItem()
    {
        GameObject poolGo = Instantiate(_gameobjectDictionary[_objectName]);
        poolGo.GetComponent<PoolAble>().Pool = _objectPoolDictionary[_objectName];
        return poolGo;
    }

    // 대여
    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    public GameObject GetGameObject(string objectName)
    {
        _objectName = objectName;

        if (_gameobjectDictionary.ContainsKey(objectName) is false)
        {
            return null;
        }

        return _objectPoolDictionary[objectName].Get();
    }
}
