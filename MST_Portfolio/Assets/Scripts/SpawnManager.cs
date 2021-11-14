using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

enum ObjectName
{
    BombIcon,
    PowerIcon,
    MagnetIcon
}

public class SpawnManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    private ObjectPool _powerIconsPool;
    private ObjectPool _bombPool;
    private ObjectPool _magnetIconsPool;
    private int _powerIconAmount;
    private int _magnetIconAmount;

    [SerializeField] private GameObject powerIconPrefab;
    [SerializeField] private GameObject bombPrefab;
    [FormerlySerializedAs("magnetPowerIconPrefab")] [SerializeField] private GameObject magnetIconPrefab;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.ReloadLevelHandler += ReloadSpawnManager;
    }

    public void GeneratePowerIcon(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            var pIcon = _powerIconsPool.GetPooledObject();
            if (pIcon != null)
            {
                pIcon.transform.position = RandomVector3Pos(ObjectName.PowerIcon);
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
                pIconScript.explodingPowerIconPos(RandomVector3Pos(ObjectName.PowerIcon));
            }
        }
    }

    public GameObject GetActivePowerIcon()
    {
        return _powerIconsPool.GetActiveObject();
    }

    public GameObject GetActiveMagnetIcon()
    {
        return _magnetIconsPool.GetActiveObject();
    }

    public void SetMagnetPowerIcon()
    {
        StartCoroutine(GenerateMagnetAndBombIcon(_magnetIconAmount, 20f));
    }

    private IEnumerator GenerateMagnetAndBombIcon(int amount, float waitForS)
    {
        yield return new WaitForSeconds(waitForS);
        for (var i = 0; i < amount; i++)
        {
            var mPIcon = _magnetIconsPool.GetPooledObject();
            var bomb = _bombPool.GetPooledObject();
            if (mPIcon != null)
            {
                mPIcon.transform.position = RandomVector3Pos(ObjectName.MagnetIcon);
                mPIcon.SetActive(true);
            }

            if (bomb != null)
            {
                bomb.transform.position = RandomVector3Pos(ObjectName.BombIcon);
                bomb.SetActive(true);
            }
        }
    }

    private void ReloadSpawnManager(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _powerIconAmount = powerIconAmount;
        _magnetIconAmount = magnetPowerIconAmount;

        if (_powerIconsPool == null)
        {
            _powerIconsPool = new ObjectPool(powerIconPrefab, _powerIconAmount);
            _magnetIconsPool = new ObjectPool(magnetIconPrefab, _magnetIconAmount);
            _bombPool = new ObjectPool(bombPrefab, _magnetIconAmount);
        }

        GeneratePowerIcon(_powerIconAmount);
        StartCoroutine(GenerateMagnetAndBombIcon(_magnetIconAmount, 5f));
    }

    private static Vector3 RandomVector3Pos(ObjectName objectName)
    {
        var y = objectName switch
        {
            ObjectName.PowerIcon => 0.5f,
            ObjectName.MagnetIcon => 0.33f,
            _ => 0.26f
        };

        return new Vector3(Random.Range(-8, 8), y, Random.Range(-8, 8));
    }
}

