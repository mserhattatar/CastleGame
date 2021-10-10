using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private GameObject magnetPowerIconPrefab;
    private ObjectPool _powerIconsPool;
    private ObjectPool _magnetPowerIconsPool;
    private int _powerIconAmount;
    private int _magnetPowerIconAmount;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.ReloadLevelHandler += ReloadSpawnManager;
    }

    public void GeneratePowerIcon(int amount)
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

    public void SetMagnetPowerIcon()
    {
        StartCoroutine(GenerateMagnetPowerIcon(_magnetPowerIconAmount, 25f));
    }

    private IEnumerator GenerateMagnetPowerIcon(int amount, float waitForS)
    {
        yield return new WaitForSeconds(waitForS);
        for (int i = 0; i < amount; i++)
        {
            var pIcon = _magnetPowerIconsPool.GetPooledObject();
            if (pIcon != null)
            {
                pIcon.transform.position = new Vector3(Random.Range(-8, 8), 0.55f, Random.Range(-8, 8));
                pIcon.SetActive(true);
            }
        }
    }

    private void ReloadSpawnManager(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _powerIconAmount = powerIconAmount;
        _magnetPowerIconAmount = magnetPowerIconAmount;

        if (_powerIconsPool == null)
        {
            _powerIconsPool = new ObjectPool(powerIconPrefab, _powerIconAmount);
            _magnetPowerIconsPool = new ObjectPool(magnetPowerIconPrefab, _magnetPowerIconAmount);
        }

        GeneratePowerIcon(_powerIconAmount);
        StartCoroutine(GenerateMagnetPowerIcon(_magnetPowerIconAmount, 10f));
    }
}