using UnityEngine;

public class MainComponent : MonoBehaviour
{
    private ComponentContainer componentContainer;
    
    private CameraManager cameraManager;
    private CastlesManager castlesManager;
    private SpawnManager spawnManager;
    private EnemyController enemyController;
    private PlayerController playerController;

    private void Awake()
    {
        componentContainer = new ComponentContainer();
    }

    private void Start()
    {
        CreateCameraManager();
        CreateCastlesManager();
        CreateSpawnManager();
        CreateEnemyController();
        CreatePlayerController();

        InitializeComponents();
    }

    private void CreateCameraManager()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        componentContainer.AddComponent("CameraManager", cameraManager);
    }

    private void CreateCastlesManager()
    {
        castlesManager = FindObjectOfType<CastlesManager>();
        componentContainer.AddComponent("CastlesManager", castlesManager);
    }

    private void CreateSpawnManager()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        componentContainer.AddComponent("SpawnManager", spawnManager);
    }

    private void CreateEnemyController()
    {
        enemyController = FindObjectOfType<EnemyController>();
        componentContainer.AddComponent("EnemyController", enemyController);
    }

    private void CreatePlayerController()
    {
        playerController = FindObjectOfType<PlayerController>();
        componentContainer.AddComponent("PlayerController", playerController);
    }

    private void InitializeComponents()
    {
        cameraManager.Initialize(componentContainer);
        castlesManager.Initialize(componentContainer);
        spawnManager.Initialize(componentContainer);
        playerController.Initialize(componentContainer);
        enemyController.Initialize(componentContainer);
    }
}