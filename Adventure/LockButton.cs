using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    private int stage;
    [SerializeField] private Button button;
    [SerializeField] private int myNum;
    void Awake()
    {
        stage = PlayerPrefs.GetInt("Stage", 1);
        if(myNum > stage)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
