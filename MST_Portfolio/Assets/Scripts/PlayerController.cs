using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _playerAnimator;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    public int playerBag;

    private void Start()
    {
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
            other.gameObject.GetComponent<CastleScript>().SentToCastle(playerBag);
            playerBag = 0;
        }
    }
}