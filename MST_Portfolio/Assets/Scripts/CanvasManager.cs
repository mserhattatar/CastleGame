using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNumber;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private GameObject reloadButton;

    public void PlayButton()
    {
        StartPanelSetActive(false);
        ReloadButtonSetActive(true);
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
        ReloadButtonSetActive(false);
        StartPanelSetActive(true);
    }

    private void StartPanelSetActive(bool active)
    {
        startPanel.SetActive(active);
    }

    private void FailedPanelSetActive(bool active)
    {
        failedPanel.SetActive(active);
    }

    private void ReloadButtonSetActive(bool active)
    {
        reloadButton.SetActive(active);
    }
}