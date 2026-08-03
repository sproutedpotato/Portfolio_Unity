using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject[] stagePrefab;
    [SerializeField] private GameObject stopPanel;
    [SerializeField] private GameObject backImage;

    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite soundOnPressed;
    [SerializeField] private Sprite soundOffPressed;
    [SerializeField] private GameObject SoundMenu;

    [SerializeField] private Button soundButton;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private SFXController sfxController;
    private BGMController bgmController;
    private bool isMute = false;

    private void Start()
    {
        sfxController = GameObject.Find("BGM / Sound Controller").GetComponent<SFXController>();
        bgmController = GameObject.Find("BGM / Sound Controller").GetComponent<BGMController>();
    }

    public void OnClickMainButton()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClickNextStageButton()
    {
        int stage = PlayerPrefs.GetInt("SelectedStage", 1);
        Debug.Log("Prev stage is " + stage);
        if (stage >= 24)
        {
            SceneManager.LoadScene("LobbyScene");
            Time.timeScale = 1f;
            return;
        }

        PlayerPrefs.SetInt("SelectedStage" , stage + 1);
        stage = PlayerPrefs.GetInt("SelectedStage", 1);
        Debug.Log("Current stage is " + stage);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickRetryButton()
    {
        Time.timeScale = 1f;
        int currentPlaying = PlayerPrefs.GetInt("SelectedStage", 1);
        PlayerPrefs.SetInt("SelectedStage", currentPlaying);
        SceneManager.LoadScene("GameScene");
    }
    
    public void OnClickStopButton()
    {
        Time.timeScale = 0f;
        backImage.SetActive(true);
        stopPanel.SetActive(true);
    }

    public void OnClickReturnButton()
    {
        Time.timeScale = 1f;
        backImage.SetActive(false);
        SoundMenu.SetActive(false);
        stopPanel.SetActive(false);
    }

    public void OnClickSoundButton()
    {
        isMute = !isMute;

        if(isMute)
        {
            SoundMenu.SetActive(true);
            bgmController.Initslider(bgmSlider);
            sfxController.Initslider(sfxSlider);
            SetButtonAppearance(soundOffSprite, soundOffPressed);
        }
        else
        {
            SoundMenu.SetActive(false);
            SetButtonAppearance(soundOnSprite, soundOnPressed);
        }
    }

    public void SetButtonAppearance(Sprite normal, Sprite pressed)
    {
        soundButtonImage.sprite = normal;

        SpriteState state = soundButton.spriteState;

        if (bgmController.ReturnSliderValue() <= 0.0001f && sfxController.ReturnSliderValue() <= 0.0001f)
        {
            soundButtonImage.sprite = soundOffSprite;
            state.pressedSprite = soundOffPressed;
        }
        else
        {
            soundButtonImage.sprite = soundOnSprite;
            state.pressedSprite = soundOnPressed;
        }

        soundButton.spriteState = state;
    }

    public void OnClickStageButton(int stage)
    {
        PlayerPrefs.SetInt("SelectedStage", stage);
        SceneManager.LoadScene("GameScene");
    }
}
