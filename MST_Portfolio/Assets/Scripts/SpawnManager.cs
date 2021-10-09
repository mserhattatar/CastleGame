using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ComponentContainer myComponent;
    private GameManager _gameManager;

    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private GameObject magnetPowerIconPrefab;
    private ObjectPool _powerIconsPool;
    private ObjectPool _magnetPowerIconsPool;
    private int powerIconAmount = 20;
    private int magnetPowerIconAmount = 1;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }


    private void Start()
    {
        _gameManager = myComponent.GetComponent("GameManager") as GameManager;
        ReloadSpawnManager();
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

    private void ReloadSpawnManager()
    {
        powerIconAmount = _gameManager.GetPowerIconAmount();
        magnetPowerIconAmount = _gameManager.GetMagnetPowerIconAmount();

        if (_powerIconsPool == null)
        {
            _powerIconsPool = new ObjectPool(powerIconPrefab, powerIconAmount);
            _magnetPowerIconsPool = new ObjectPool(magnetPowerIconPrefab, magnetPowerIconAmount);
        }

        GeneratePowerIcon(powerIconAmount);
        StartCoroutine(GenerateMagnetPowerIcon(magnetPowerIconAmount, 10f));
    }
}