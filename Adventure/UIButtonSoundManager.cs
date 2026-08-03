using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;

    private void Start()
    {
        RegisterButtons();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterButtons();
    }

    private void RegisterButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.RemoveListener(PlayClickSound);
            button.onClick.AddListener(PlayClickSound);
        }
    }

    private void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}