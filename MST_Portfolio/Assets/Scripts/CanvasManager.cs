using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private ComponentContainer myComponent;

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

    private void ReloadCanvasManager(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _levelNumber.text = "Level " + levelNumber;
    }

    public void SetPowerBar(float enemyP, float playerP)
    {
        enemyPowerBar.fillAmount = enemyP;
        playerPowerBar.fillAmount = playerP;
    }

    public void PlayButton()
    {
        StartPanelSetActive(false);
        ReloadButtonSetActive(true);
        GameManager.StartGameHandler(true);
    }

    public void NextLevelButton()
    {
        StartPanelSetActive(true);
    }

    public void TryAgainButton()
    {
        StartPanelSetActive(true);
    }

    public void SettingsButton()
    {
    }

    public void ReloadButton()
    {
        GameManager.StartGameHandler(false);
        ReloadButtonSetActive(false);
        StartPanelSetActive(true);
    }

    private void StartPanelSetActive(bool active)
    {
        startPanel.SetActive(active);
    }

    private void FailedPanelSetActive(bool active)
    {
        GameManager.StartGameHandler(false);
        failedPanel.SetActive(active);
    }

    private void ReloadButtonSetActive(bool active)
    {
        GameManager.StartGameHandler(false);
        reloadButton.SetActive(active);
    }
}