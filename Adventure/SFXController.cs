using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private Slider SFXSlider;
    void Start()
    {
        float saveValue = PlayerPrefs.GetFloat("SFX_Volume", 0.5f);

        ApplyVolumeToMixer(saveValue);
    }

    private void ApplyVolumeToMixer(float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;

        audioMixer.SetFloat("SFX", volume);
    }

    private void SetValue()
    {
        ApplyVolumeToMixer(SFXSlider.value);
        GameManager.Instance.SaveSoundData("SFX_Volume", SFXSlider.value);
    }

    public float ReturnSliderValue()
    {
        return SFXSlider.value;
    }

    public void Initslider(Slider slider)
    {
        this.SFXSlider = slider;

        SFXSlider.onValueChanged.RemoveAllListeners();

        float saveValue = PlayerPrefs.GetFloat("SFX_Volume", 0.5f);

        SFXSlider.onValueChanged.AddListener(delegate { SetValue(); });
        SFXSlider.value = saveValue;
    }
}
