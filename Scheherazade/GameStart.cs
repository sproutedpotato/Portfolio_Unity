using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip btnSound;
    private SceneController sceneController;

    private void Start()
    {
        sceneController = GameObject.Find("GameManager").GetComponent<SceneController>();
    }

    public void LoadScene()
    {
        audioSource.PlayOneShot(btnSound);
        sceneController.ChangeScene("StoryScene");
    }
}
