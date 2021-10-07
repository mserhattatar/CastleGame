using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool _powerIconsPool;
    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private int powerAmount;

    private void Start()
    {
        _powerIconsPool = new ObjectPool(powerIconPrefab, powerAmount);
        GeneratePowerIcon(powerAmount);
    }

    public void GeneratePowerIcon(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            var pIcon = _powerIconsPool.GetPooledObject();
            if (pIcon != null)
            {
                pIcon.transform.position = new Vector3(Random.Range(-8, 8), 0.55f, Random.Range(-8, 8));
                pIcon.SetActive(true);
            }
        }
    }

    public GameObject GetActivePowerIcon()
    {
        return _powerIconsPool.GetActiveObject();
    }
}