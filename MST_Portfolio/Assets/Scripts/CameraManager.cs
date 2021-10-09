using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private ComponentContainer myComponent;


    [SerializeField] private CinemachineVirtualCamera playerCamera;

    private CinemachineBasicMultiChannelPerlin _playerCmPerlin;

    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;

    private float _shakeTimer;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
    }

    private void Awake()
    {
        _shakeTimer = 0f;
    }

    private void Start()
    {
        _playerCmPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
        }

        else if (_shakeTimer <= 0f)
        {
            _playerCmPerlin.m_AmplitudeGain = 0f;
        }
    }


    private void ShakeCamera()
    {
        _playerCmPerlin.m_AmplitudeGain = shakeIntensity;
        _shakeTimer = shakeTime;
    }
}