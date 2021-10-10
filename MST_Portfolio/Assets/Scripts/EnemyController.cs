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

    public int enemyBag;

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
        EnemyMovementAnimation();
        if (!_isGameStarted) return;

        if (enemyBag > 5 && _targetPos != enemyCastlePos)
        {
            SetEnemyDestination(enemyCastlePos);
        }
        else if (enemyBag > 5)
            return;

        if (_activePowerIconTarget == null || !_activePowerIconTarget.activeInHierarchy || enemyBag == 0)
        {
            _activePowerIconTarget = _spawnManager.GetActivePowerIcon();
            //eğer hala null ise kalene git. demekki sahnede hiç obje kalmamış
            _targetPos = _activePowerIconTarget == null ? enemyCastlePos : _activePowerIconTarget.transform.position;
            SetEnemyDestination(_targetPos);
        }
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
            enemyBag++;
        }

        if (enemyBag > 0 && other.CompareTag("EnemyCastle"))
        {
            _castlesManager.AddPower(false, enemyBag);
            enemyBag = 0;
        }
    }

    private void GameStarted(bool isStarted)
    {
        _isGameStarted = isStarted;
    }


    private void ReloadEnemyController(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _isGameStarted = false;
        enemyBag = 0;
        _targetPos = enemyCastlePos;
        transform.position = _targetPos;
        _activePowerIconTarget = null;
    }
}