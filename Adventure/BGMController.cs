using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.VFX;

public class BGMController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    private Slider BGMSlider;

    private void Start()
    {
        float saveValue = PlayerPrefs.GetFloat("BGM_Volume", 0.5f);

        ApplyVolumeToMixer(saveValue);
    }
    private void ApplyVolumeToMixer(float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20;

        audioMixer.SetFloat("BGM", volume);
    }

    private void SetLevel()
    {
        if (BGMSlider == null) return;

        ApplyVolumeToMixer(BGMSlider.value);
        GameManager.Instance.SaveSoundData("BGM_Volume", BGMSlider.value);
    }

    public float ReturnSliderValue()
    {
        return BGMSlider.value;
    }

    public void Initslider(Slider slider)
    {
        this.BGMSlider = slider;

        BGMSlider.onValueChanged.RemoveAllListeners();

        float saveValue = PlayerPrefs.GetFloat("BGM_Volume", 0.5f);

        BGMSlider.onValueChanged.AddListener(delegate { SetLevel(); });
        BGMSlider.value = saveValue;
    }
}
