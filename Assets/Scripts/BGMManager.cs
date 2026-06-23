using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    //BGM音量調節用のスライダー
    public Slider bgmSlider;

    //BGMを再生するAudiosource
    private AudioSource audioSource;

    void Start()
    {
        //Audiosourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        //保存されているBGM音量を読み込む
        float volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        //読み込んだ音量を適用
        audioSource.volume = volume;

        //スライダーの表示と音量を同期
        if (bgmSlider != null)
        {
            bgmSlider.value = volume;
        }
    }

    public void SetVolume(float volume)
    {
        //スライダーで変更された音量を適用
        audioSource.volume = volume;

        //音量設定を保存
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
}
