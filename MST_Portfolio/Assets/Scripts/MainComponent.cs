using UnityEngine;

public class MainComponent : MonoBehaviour
{
    private ComponentContainer componentContainer;

    private GameManager gameManager;
    private CameraManager cameraManager;
    private CastlesManager castlesManager;
    private SpawnManager spawnManager;
    private EnemyController enemyController;
    private PlayerController playerController;
    private CanvasManager canvasManager;

    private void Awake()
    {
        componentContainer = new ComponentContainer();

        CreateGameManager();
        CreateCameraManager();
        CreateCastlesManager();
        CreateSpawnManager();
        CreateEnemyController();
        CreatePlayerController();
        CreateCanvasManager();

        InitializeComponents();
    }

    private void CreateGameManager()
    {
        gameManager = FindObjectOfType<GameManager>();
        componentContainer.AddComponent("GameManager", gameManager);
    }

    private void CreateCanvasManager()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        componentContainer.AddComponent("CanvasManager", canvasManager);
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
        gameManager.Initialize(componentContainer);
        cameraManager.Initialize(componentContainer);
        castlesManager.Initialize(componentContainer);
        spawnManager.Initialize(componentContainer);
        playerController.Initialize(componentContainer);
        enemyController.Initialize(componentContainer);
    }
}