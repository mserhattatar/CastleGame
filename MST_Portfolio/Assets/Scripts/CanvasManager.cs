using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CanvasManager : MonoBehaviour
{
    private ComponentContainer myComponent;

    [SerializeField] private TextMeshProUGUI _levelNumber;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private GameObject reloadButton;

    public void Initialize(ComponentContainer componentContainer)
    {
        myComponent = componentContainer;
        GameManager.ReloadLevelHandler += ReloadCanvasManager;
    }

    private void ReloadCanvasManager(int levelNumber, int powerIconAmount, int magnetPowerIconAmount)
    {
        _levelNumber.text = "Level " + levelNumber;
    }

    public void PlayButton()
    {
        StartPanelSetActive(false);
        ReloadButtonSetActive(true);
        GameManager.StartGameHandler(true);
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