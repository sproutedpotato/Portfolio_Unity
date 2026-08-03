using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [SerializeField] private Transform stageRoot;
    [SerializeField] private GameObject[] stagePrefab;

    private GameObject currentStage;
    private int currentStageIndex = 0;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void LoadStage(int index)
    {
        if(currentStage != null)
        {
            Destroy(currentStage);
        }

        currentStageIndex = index;
        currentStage = Instantiate(stagePrefab[currentStageIndex], stageRoot);
    }

    public void LoadNextStage()
    {
        if(currentStageIndex + 1 >= stagePrefab.Length)
        {
            Debug.Log("Return");
            return;
        }
        LoadStage(currentStageIndex + 1);
    }

    public void RetryStage()
    {
        LoadStage(currentStageIndex);
    }
}
