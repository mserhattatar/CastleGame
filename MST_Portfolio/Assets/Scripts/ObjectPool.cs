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

        if (_pooledObjects.Count > 0)
        {
            poolAmount = -_pooledObjects.Count;
            foreach (var pooledObject in _pooledObjects)
            {
                pooledObject.SetActive(false);
            }
        }

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

    public GameObject GetActiveObject()
    {
        return _pooledObjects.FirstOrDefault(obj => obj.activeInHierarchy);
    }

    public GameObject GetPooledObject(bool ifNotHaveSendNew = false)
    {
        //return _pooledObjects.FirstOrDefault(obj => !obj.activeInHierarchy);
        foreach (var obj in _pooledObjects.Where(obj => !obj.activeInHierarchy))
            return obj;

        return ifNotHaveSendNew ? CreateObject() : null;
    }
}