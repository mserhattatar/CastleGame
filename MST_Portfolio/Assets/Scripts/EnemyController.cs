using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private SpawnManager _spawnManager;
    private Animator _enemyAnimator;
    private NavMeshAgent _navMeshAgent;

    private GameObject _activeGemTarget;
    private Vector3 _targetPos;
    [SerializeField] private Vector3 enemyCastlePos;

    public int enemyBag;

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _enemyAnimator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void LateUpdate()
    {
        EnemyMovementAnimation();
        if (enemyBag > 5 && _targetPos != enemyCastlePos)
        {
            SetEnemyDestination(enemyCastlePos);
        }
        else if (enemyBag > 5)
            return;
       
        if (_activeGemTarget == null || !_activeGemTarget.activeInHierarchy || enemyBag == 0)
        {
            _activeGemTarget = _spawnManager.GetActivePowerIcon();
            //eğer hala null ise kalene git. demekki sahnede hiç obje kalmamış
            _targetPos = _activeGemTarget == null ? enemyCastlePos : _activeGemTarget.transform.position;
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
        Debug.Log(_navMeshAgent.velocity.magnitude);
        float speed = System.Math.Abs(_navMeshAgent.velocity.magnitude);
        _enemyAnimator.SetFloat("Speed_f", speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerIcon"))
        {
            other.gameObject.SetActive(false);
            enemyBag++;
        }

        if (enemyBag > 0 && other.CompareTag("EnemyCastle"))
        {
            other.gameObject.GetComponent<CastleScript>().SentToCastle(enemyBag);
            enemyBag = 0;
        }
    }
}