using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //オプション画面のパネル
    public GameObject optionPanel;

    //フルスクリーン設定用のパネル
    public Toggle fullscreenToggle;

    private bool isInitializing = false;
    void Start()
    {
        //ゲーム開始時はオプション画面を非表示にする
        optionPanel.SetActive(false);

        //保存されているフルスクリーン設定を取得
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 0) == 1;

        ApplyFullscreen(isFullscreen);

        //トグルが設定されている場合は状態を同期
        if (fullscreenToggle != null)
        {
            isInitializing = true;
            fullscreenToggle.isOn = isFullscreen;
            isInitializing = false;
        }

        Debug.Log("Loaded Fullscreen = " + isFullscreen);
    }
    public void SetFullscreen(bool unused)
    {
        bool isOn = fullscreenToggle.isOn;

        Debug.Log("Fullscreen = " + isOn);

        ApplyFullscreen(isOn);

        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ApplyFullscreen(bool isOn)
    {
        if (isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }
    }

    public void OpenOption()
    {
        //オプション画面を表示
        optionPanel.SetActive(true);
        //ゲームを一時停止
        Time.timeScale = 0f;
    }

    public void CloseOption()
    {
        //オプション画面を非表示
        optionPanel.SetActive(false);
        //ゲームを再開
        Time.timeScale = 1f;
    }


}
