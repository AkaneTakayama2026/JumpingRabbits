using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    public Slider bgmSlider;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        audioSource.volume = volume;

        if (bgmSlider != null)
        {
            bgmSlider.value = volume;
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;

        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }
}
