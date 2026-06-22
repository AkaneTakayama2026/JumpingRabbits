using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //オプション画面のパネル
    public GameObject optionPanel;

    //フルスクリーン設定用のパネル
    public Toggle fullscreenToggle;
    void Start()
    {
        //ゲーム開始時はオプション画面を非表示にする
        optionPanel.SetActive(false);

        //保存されているフルスクリーン設定を取得
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        //フルスクリーン設定を適用
        Screen.fullScreen = isFullscreen;
        //トグルが設定されている場合は状態を同期
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = isFullscreen;
        }
    }

    public void SetFullscreen(bool isOn)
    {
        //フルスクリーン設定を変更
        Screen.fullScreen = isOn;

        //設定内容を保存
        PlayerPrefs.SetInt("Fullscreen", isOn ? 1 : 0);
        PlayerPrefs.Save();
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
