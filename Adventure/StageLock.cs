using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLock : MonoBehaviour
{
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        LockStage();
    }

    private void LockStage()
    {
        if (button.interactable)
        {
            gameObject.SetActive(false);
        }
    }
}
