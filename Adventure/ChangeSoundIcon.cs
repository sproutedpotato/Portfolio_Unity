using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundIcon : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider, sfxSlider;
    [SerializeField] private Button soundButton;
    [SerializeField] private Sprite onSprite, offSprite;

    private void OnEnable()
    {
        bool isMuted = (bgmSlider.value <= 0.0001f && sfxSlider.value <= 0.0001f);
        soundButton.image.sprite = isMuted ? offSprite : onSprite;

        bgmSlider.onValueChanged.AddListener(delegate { ChangeImage(); });
        sfxSlider.onValueChanged.AddListener(delegate { ChangeImage(); });
    }

    private void OnDisable()
    {
        bgmSlider.onValueChanged.RemoveListener(delegate { ChangeImage(); });
        sfxSlider.onValueChanged.RemoveListener(delegate { ChangeImage(); });
    }

    private void ChangeImage()
    {
        if (bgmSlider.value <= 0.0001f && sfxSlider.value <= 0.0001f)
        {
            soundButton.image.sprite = offSprite;
        }
        else
        {
            soundButton.image.sprite = onSprite;
        }
    }
}
