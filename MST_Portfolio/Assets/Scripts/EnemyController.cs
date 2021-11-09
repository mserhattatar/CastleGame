using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private ComponentContainer myComponent;
    private SpawnManager _spawnManager;
    private CastlesManager _castlesManager;

    private Animator _enemyAnimator;
    private NavMeshAgent _navMeshAgent;

    [SerializeField] private GameObject magnetPowerObj;
    [SerializeField] private Vector3 enemyCastlePos;
    private GameObject _activePowerIconTarget;
    private Vector3 _targetPos;
    private bool _isGameStarted;
    private int _enemyBagCount;
    private int _enemyBagMaxCount;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.StartGameHandler += GameStarted;
        GameManager.ReloadLevelHandler += ReloadEnemyController;
    }


    private void Start()
    {
        _castlesManager = myComponent.GetComponent("CastlesManager") as CastlesManager;
        _spawnManager = myComponent.GetComponent("SpawnManager") as SpawnManager;
        _enemyAnimator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        if (_isGameStarted)
        {
            if (_enemyBagCount > _enemyBagMaxCount && _targetPos != enemyCastlePos)
            {
                SetEnemyDestination(enemyCastlePos);
            }
            else if (_enemyBagCount > _enemyBagMaxCount)
                return;

            if (_activePowerIconTarget == null || !_activePowerIconTarget.activeInHierarchy || _enemyBagCount == 0)
            {
                _activePowerIconTarget = _spawnManager.GetActivePowerIcon();
                //eğer hala null ise kalene git. demekki sahnede hiç obje kalmamış
                _targetPos = _activePowerIconTarget == null
                    ? enemyCastlePos
                    : _activePowerIconTarget.transform.position;
                SetEnemyDestination(_targetPos);
            }
        }

        EnemyMovementAnimation();
    }

    private void SetEnemyDestination(Vector3 destPos)
    {
        _targetPos = destPos;
        _navMeshAgent.SetDestination(destPos);
    }

    private void EnemyMovementAnimation()
    {
        float speed = System.Math.Abs(_navMeshAgent.velocity.magnitude);
        _enemyAnimator.SetFloat("Speed_f", speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetPowerIcon"))
        {
            other.gameObject.SetActive(false);
            magnetPowerObj.GetComponent<MagnetPowerScript>().SetMagnetPowerTransform(transform);
        }

        if (other.CompareTag("PowerIcon"))
        {
            other.GetComponent<PowerIconScript>().SetVisibility(false);
            _enemyBagCount++;
        }

        if (_enemyBagCount > 0 && other.CompareTag("EnemyCastle"))
        {
            _castlesManager.AddPower(false, _enemyBagCount);
            _enemyBagCount = 0;
        }
    }

    private void GameStarted(bool isStarted)
    {
        _isGameStarted = isStarted;
        _navMeshAgent.isStopped = !isStarted;
    }


    private void ReloadEnemyController(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _isGameStarted = false;

        _targetPos = enemyCastlePos;
        transform.position = _targetPos;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        _activePowerIconTarget = null;

        _enemyBagCount = 0;

        if (levelNumber <= 5)
            _enemyBagMaxCount = 5;
        else
            _enemyBagMaxCount = levelNumber + 1;
    }
}