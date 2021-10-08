using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public delegate void CineMachineDelegate();

    public static CineMachineDelegate CineMachineShakeDelegate;
    
    [SerializeField] private Transform fCamRotatePos;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera firstCamera;
    
    private CinemachineBasicMultiChannelPerlin _playerCmPerlin;

    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeIntensity;

    private float _shakeTimer;


    private void Awake()
    {
        CineMachineShakeDelegate += ShakeCamera;
        _shakeTimer = 0f;
    }
    private void Start()
    {
        _playerCmPerlin = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (firstCamera.gameObject.activeInHierarchy)
            FirstCameraRotation();
        if (_shakeTimer > 0)
        {
            _shakeTimer -= Time.deltaTime;
        }

        else if (_shakeTimer <= 0f)
        {
            _playerCmPerlin.m_AmplitudeGain = 0f;
        }
    }

    private void FirstCameraRotation()
    {
        var fCamPos = firstCamera.transform.position;
        var rPos = fCamRotatePos.position;

        fCamRotatePos.Rotate(Vector3.up, 42f * Time.deltaTime);
        firstCamera.transform.position = Vector3.Lerp(fCamPos, rPos, Time.deltaTime * 0.3f);

        if ((fCamPos - rPos).magnitude < 15f)
        {
            //Switching the camera 1 to 2 
            playerCamera.gameObject.SetActive(true);
            playerCamera.Priority = 2;
            firstCamera.gameObject.SetActive(false);
        }
    }


    private void ShakeCamera()
    {
        _playerCmPerlin.m_AmplitudeGain = shakeIntensity;
        _shakeTimer = shakeTime;
    }
}