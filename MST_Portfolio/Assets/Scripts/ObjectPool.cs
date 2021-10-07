using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    private readonly List<GameObject> _pooledObjects;
    private readonly GameObject _objectToPool;

    public ObjectPool(GameObject objPrefab, int poolAmount)
    {
        _objectToPool = objPrefab;
        _pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolAmount; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        var obj = Object.Instantiate(_objectToPool);
        obj.SetActive(false);
        _pooledObjects.Add(obj);
        return obj;
    }

    public GameObject GetPooledObject(bool ifNotHaveSendNew = false)
    {
        foreach (var obj in _pooledObjects.Where(obj => !obj.activeInHierarchy))
        {
            return obj;
        }

        //return _pooledObjects.First(objs => !objs.activeInHierarchy);

        return ifNotHaveSendNew ? CreateObject() : null;
    }
}