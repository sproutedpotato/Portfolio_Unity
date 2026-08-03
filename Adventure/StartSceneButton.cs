using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject backImage;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private BGMController bgmController;
    private SFXController sfxController;

    private void Start()
    {
        bgmController = GameObject.Find("BGM / Sound Controller").GetComponent<BGMController>();
        sfxController = GameObject.Find("BGM / Sound Controller").GetComponent<SFXController>();
    }

    public void OnClickGameStartButton()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnClickSettingButton()
    {
        backImage.SetActive(true);
        settingPanel.SetActive(true);
        bgmController.Initslider(bgmSlider);
        sfxController.Initslider(sfxSlider);
    }

    public void OnClickMenuExitButton()
    {
        backImage.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickLobbyExit()
    {
        SceneManager.LoadScene("StartScene");
    }
}
