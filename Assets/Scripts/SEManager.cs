using UnityEngine;
using UnityEngine.UI;

public class SEManager : MonoBehaviour
{
    public Slider seSlider;
    public static float seVolume = 0.5f;

    void Start()
    {
        seVolume = PlayerPrefs.GetFloat("SEVolume", 0.5f);

        if (seSlider != null)
        {
            seSlider.value = seVolume;
        }

    }

    public void SetSEVolume(float volume)
    {
        seVolume = volume;

        Debug.Log("SE Volume = " + seVolume);

        PlayerPrefs.SetFloat("SEVolume", volume);
        PlayerPrefs.Save();
    }
}
