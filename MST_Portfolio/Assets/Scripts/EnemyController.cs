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
    private GameObject _activeTargetIcon;
    private Vector3 _targetPos;
    private bool _isGameStarted;
    private int _enemyBagCount;
    private int _enemyBagMaxCount;
    private static readonly int SpeedF = Animator.StringToHash("Speed_f");

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

    [System.Obsolete]
    private void LateUpdate()
    {
        EnemyMovementAnimation();

        if (_isGameStarted)
        {
            if (_enemyBagCount > _enemyBagMaxCount)
            {
                if (_targetPos == enemyCastlePos) return;
                SetEnemyDestination(enemyCastlePos);
            }

            else if (_activeTargetIcon == null || !_activeTargetIcon.activeInHierarchy ||
                     (_enemyBagCount == 0 && _targetPos == enemyCastlePos))
            {
                SetTargetIconPos();
            }
        }
    }

    [System.Obsolete]
    private void SetTargetIconPos()
    {
        Random.seed = System.DateTime.Now.Millisecond;
        var isTrue = Random.Range(0, 2);

        if (isTrue == 1)
        {
            var activeMagnet = _spawnManager.GetActiveMagnetIcon();
            if (activeMagnet != null)
            {
                _activeTargetIcon = activeMagnet;
                SetEnemyDestination(activeMagnet.transform.position);
                return;
            }
        }

        _activeTargetIcon = _spawnManager.GetActivePowerIcon();

        if (_activeTargetIcon != null)
            _targetPos = _activeTargetIcon.transform.position;
        else
            _targetPos = enemyCastlePos;

        SetEnemyDestination(_targetPos);
    }

    private void SetEnemyDestination(Vector3 destPos)
    {
        _targetPos = destPos;
        _navMeshAgent.SetDestination(destPos);
    }

    private void EnemyMovementAnimation()
    {
        float speed = System.Math.Abs(_navMeshAgent.velocity.magnitude);
        _enemyAnimator.SetFloat(SpeedF, speed);
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
        _activeTargetIcon = null;

        _enemyBagCount = 0;
        _enemyBagMaxCount = levelNumber + 5;
    }
}