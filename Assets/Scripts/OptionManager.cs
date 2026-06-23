using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //オプション画面のパネル
    public GameObject optionPanel;

    //フルスクリーン設定用のトグル
    public Toggle fullscreenToggle;

    private bool isInitializing = false;
    void Start()
    {
        //ゲーム開始時はオプション画面を非表示にする
        optionPanel.SetActive(false);

        //保存されているフルスクリーン設定を取得
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 0) == 1;

        ApplyFullscreen(isFullscreen);

        //トグルが設定されている場合は保存されていた設定と表示状態を同期
        if (fullscreenToggle != null)
        {
            isInitializing = true;
            fullscreenToggle.isOn = isFullscreen;
            isInitializing = false;
        }
        //デバッグ用に読み込んだ設定を確認
        Debug.Log("Loaded Fullscreen = " + isFullscreen);
    }
    public void SetFullscreen(bool unused)
    {
        //トグルの現在の状態を取得
        bool isOn = fullscreenToggle.isOn;

        //デバッグ用に現在の設定を確認
        Debug.Log("Fullscreen = " + isOn);

        //フルスクリーン設定を反映
        ApplyFullscreen(isOn);

        //設定を保存し、次回起動時にも反映されるようにする
        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    void ApplyFullscreen(bool isOn)
    {
        //フルスクリーンONの場合
        if (isOn)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        //フルスクリーンOFFの場合
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
        //ゲーム一時停止
        Time.timeScale = 0f;
    }

    public void CloseOption()
    {
        //オプション画面を非表示
        optionPanel.SetActive(false);
        //ゲーム再開
        Time.timeScale = 1f;
    }


}
