using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool _gemsPool;
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private int gemAmount;

    private void Start()
    {
        _gemsPool = new ObjectPool(gemPrefab, gemAmount);
    }

    private void LateUpdate()
    {
        GenerateGem();
    }

    private void GenerateGem()
    {
        var gem = _gemsPool.GetPooledObject();
        if (gem != null)
        {
            gem.transform.position = new Vector3(Random.Range(-8, 8), 0.5f, Random.Range(-8, 8));
            gem.SetActive(true);
        }
    }
}