using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool _gemsPool;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int gemAmount;

    public int gemBagAmount;

    private void Start()
    {
        _gemsPool = new ObjectPool(gemPrefab, gemAmount);
        GenerateGem(gemAmount);
    }

    public void GenerateGem(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            var gem = _gemsPool.GetPooledObject();
            if (gem != null)
            {
                gem.transform.position = new Vector3(Random.Range(-8, 8), 0.5f, Random.Range(-8, 8));
                gem.SetActive(true);
            }
        }
    }
}