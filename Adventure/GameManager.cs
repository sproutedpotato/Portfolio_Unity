using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int stage;
    private int count;
    private GameObject currentGameObject;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int selected = PlayerPrefs.GetInt("SelectedStage", 1);

        if (scene.name == "GameScene")
        {
            LoadStage(selected);
        }
    }

    public void SaveSoundData(string data, float volume)
    {
        PlayerPrefs.SetFloat(data, volume);

        PlayerPrefs.Save();
    }

    public void LoadStage(int stage)
    {
        if(currentGameObject != null)
        {
            Destroy(currentGameObject);
        }
        
        string path = "Stages/Stage" + stage;

        GameObject stagePrefab = Resources.Load<GameObject>(path);

        currentGameObject = Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);
    }

    public void GetCount(int num)
    {
        count = num;
    }

    public int ReturnCount()
    {
        return count;
    }
}
