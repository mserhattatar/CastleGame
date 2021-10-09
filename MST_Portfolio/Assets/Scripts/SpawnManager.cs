using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ComponentContainer MyComponent;

    private ObjectPool _powerIconsPool;
    private ObjectPool _magnetPowerIconsPool;
    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private GameObject magnetPowerIconPrefab;
    [SerializeField] private int powerAmount;
    [SerializeField] private int magnetPowerIconAmount;

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
    }

    private void Start()
    {
        _powerIconsPool = new ObjectPool(powerIconPrefab, powerAmount);
        GeneratePowerIcon(powerAmount);

        _magnetPowerIconsPool = new ObjectPool(magnetPowerIconPrefab, magnetPowerIconAmount);
        StartCoroutine(GenerateMagnetPowerIcon(magnetPowerIconAmount, 10f));
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
        StartCoroutine(GenerateMagnetPowerIcon(magnetPowerIconAmount, 25f));
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
}