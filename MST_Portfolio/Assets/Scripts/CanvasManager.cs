using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private ComponentContainer myComponent;
    private GameManager _gameManager;

    [SerializeField] private TextMeshProUGUI _levelNumber;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject reloadButton;
    [SerializeField] private Image enemyPowerBar;
    [SerializeField] private Image playerPowerBar;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.ReloadLevelHandler += ReloadCanvasManager;
    }

    private void Start()
    {
        _gameManager = myComponent.GetComponent("GameManager") as GameManager;
    }

    public void SetPowerBar(float enemyP, float playerP)
    {
        enemyPowerBar.fillAmount = enemyP;
        playerPowerBar.fillAmount = playerP;
    }

    #region Buttons Function

    public void PlayButton()
    {
        StartPanelSetActive(false);
        ReloadButtonSetActive(true);
        GameManager.StartGameHandler(true);
    }

    public void NextLevelButton()
    {
        _gameManager.NextLevel();
        WinPanelSetActive(false);
        StartPanelSetActive(true);
    }

    public void TryAgainButton()
    {
        _gameManager.ReloadLevel();
        FailedPanelSetActive(false);
        StartPanelSetActive(true);
    }

    public void SettingsButton()
    {
    }

    public void ReloadButton()
    {
        GameManager.StartGameHandler(false);
        _gameManager.ReloadLevel();
        ReloadButtonSetActive(false);
        StartPanelSetActive(true);
    }

    #endregion

    #region Panels, Buttons SetActive

    private void StartPanelSetActive(bool active)
    {
        startPanel.SetActive(active);
    }

    public void FailedPanelSetActive(bool active)
    {
        if (active)
        {
            GameManager.StartGameHandler(false);
            ReloadButtonSetActive(false);
        }
        failedPanel.SetActive(active);
    }

    public void WinPanelSetActive(bool active)
    {
        if (active)
        {
            GameManager.StartGameHandler(false);
            ReloadButtonSetActive(false);
        }
        winPanel.SetActive(active);
    }

    private void ReloadButtonSetActive(bool active)
    {
        reloadButton.SetActive(active);
    }

    #endregion

    private void ReloadCanvasManager(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _levelNumber.text = "Level " + levelNumber;
        SetPowerBar(0, 0);
    }
}