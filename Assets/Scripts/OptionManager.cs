using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public GameObject optionPanel;
    public Toggle fullscreenToggle;
    void Start()
    {
        optionPanel.SetActive(false);
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = isFullscreen;
        if (fullscreenToggle != null)
        {
            Screen.fullScreen = isFullscreen;
        }
    }

    public void SetFullscreen(bool isOn)
    {
        Screen.fullScreen = isOn;

        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    public void OpenOption()
    {
        optionPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseOption()
    {
        optionPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
