using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;


    [SerializeField]private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSoundsVolume();
    }
    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master",DB(masterSlider));
    }

    public void SetMusicVolume()
    {
        audioMixer.SetFloat("Music", DB(musicSlider));
    }
    public void SetSoundsVolume()
    {

        audioMixer.SetFloat("Sounds", DB(soundSlider));
    }

    private float DB(Slider voluSliderme)
    {
        float volume = voluSliderme.value;
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        return dB;
    }
}
