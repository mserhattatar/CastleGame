using UnityEngine;

public class PlayerController : JoystickManager
{
    private ComponentContainer myComponent;

    private CastlesManager _castlesManager;
    private SpawnManager _spawnManager;

    private Animator _playerAnimator;
    [SerializeField] private GameObject magnetPowerObj;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    private bool _isGameStarted;

    public int playerBag;


    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.StartGameHandler += GameStarted;
        GameManager.ReloadLevelHandler += ReloadPlayerController;
    }

    private void Start()
    {
        _castlesManager = myComponent.GetComponent("CastlesManager") as CastlesManager;
        _spawnManager = myComponent.GetComponent("SpawnManager") as SpawnManager;
        _playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_isGameStarted) return;

        transform.Translate(Vector3.forward * Time.deltaTime * speed * JoystickVertical);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * JoystickHorizontal);

        PlayerMovementAnimation(System.Math.Abs(JoystickVertical) + System.Math.Abs(JoystickHorizontal));
    }

    private void PlayerMovementAnimation(float animationSpeed)
    {
        _playerAnimator.SetFloat("Speed_f", animationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MagnetPowerIcon"))
        {
            other.gameObject.SetActive(false);
            _spawnManager.SetMagnetPowerIcon();
            magnetPowerObj.GetComponent<MagnetPowerScript>().SetMagnetPowerTransform(transform);
        }

        if (other.CompareTag("PowerIcon"))
        {
            other.GetComponent<PowerIconScript>().SetVisibility(false);
            playerBag++;
        }

        if (playerBag > 0 && other.CompareTag("PlayerCastle"))
        {
            _castlesManager.AddPower(true, playerBag);
            playerBag = 0;
        }
    }

    private void GameStarted(bool isStarted)
    {
        _isGameStarted = isStarted;
    }

    private void ReloadPlayerController(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        playerBag = 0;
        _isGameStarted = false;
        transform.position = new Vector3(0, 0.039f, 0);
    }
}