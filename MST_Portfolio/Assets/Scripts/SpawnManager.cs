using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    private ObjectPool _powerIconsPool;
    private ObjectPool _bombPool;
    private ObjectPool _magnetPowerIconsPool;
    private int _powerIconAmount;
    private int _magnetPowerIconAmount;

    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject magnetPowerIconPrefab;

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
                pIcon.transform.position = RandomVector3Pos();
                pIcon.SetActive(true);
            }
        }
    }
    public void ExplodingPowerIcons(Vector3 pos, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var pIconScript = _powerIconsPool.GetPooledObject().GetComponent<PowerIconScript>();
            if (pIconScript != null)
            {
                pIconScript.transform.position = new Vector3(pos.x, 2f, pos.z);
                pIconScript.SetVisibility(true);
                pIconScript.explodingPowerIconPos(RandomVector3Pos());
            }
        }
    }

    public GameObject GetActivePowerIcon()
    {
        return _powerIconsPool.GetActiveObject();
    }

    public void SetMagnetPowerIcon()
    {
        StartCoroutine(GenerateMagnetPowerAndBombIcon(_magnetPowerIconAmount, 25f));
    }
    private IEnumerator GenerateMagnetPowerAndBombIcon(int amount, float waitForS)
    {
        yield return new WaitForSeconds(waitForS);
        for (int i = 0; i < amount; i++)
        {
            var mPIcon = _magnetPowerIconsPool.GetPooledObject();
            var bomb = _bombPool.GetPooledObject();
            if (mPIcon != null)
            {
                mPIcon.transform.position = RandomVector3Pos();
                mPIcon.SetActive(true);
            }
            if (bomb != null)
            {
                bomb.transform.position = RandomVector3Pos();
                bomb.SetActive(true);
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
            _bombPool = new ObjectPool(bombPrefab, _magnetPowerIconAmount);
        }

        GeneratePowerIcon(_powerIconAmount);
        StartCoroutine(GenerateMagnetPowerAndBombIcon(_magnetPowerIconAmount, 10f));
    }

    private static Vector3 RandomVector3Pos()
    {
        return new Vector3(Random.Range(-8, 8), 0.55f, Random.Range(-8, 8));
    }




}