using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnSound;
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void OnClickQuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("Title");
    }
}
