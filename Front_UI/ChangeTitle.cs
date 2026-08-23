using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTitle : MonoBehaviour, IPanel 
{
    private GameObject title;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnClickChangeTitleButton()
    {

    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
