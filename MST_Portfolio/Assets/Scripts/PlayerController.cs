using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private ComponentContainer MyComponent;

    private CastlesManager _castlesManager;

    private Animator _playerAnimator;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    public int playerBag;

    public void Initialize(ComponentContainer componentContainer)
    {
        MyComponent = componentContainer;
    }

    private void Start()
    {
        _castlesManager = MyComponent.GetComponent("CastlesManager") as CastlesManager;
        _playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        PlayerMovementAnimation(System.Math.Abs(forwardInput) + System.Math.Abs(horizontalInput));
    }

    private void PlayerMovementAnimation(float animationSpeed)
    {
        _playerAnimator.SetFloat("Speed_f", animationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerIcon"))
        {
            other.gameObject.SetActive(false);
            playerBag++;
        }

        if (playerBag > 0 && other.CompareTag("PlayerCastle"))
        {
            _castlesManager.AddPower(true, playerBag);
            playerBag = 0;
        }
    }
}