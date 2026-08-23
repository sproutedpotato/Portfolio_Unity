using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutResultDisableManager : MonoBehaviour
{
    [SerializeField] private GameObject prohibitInteractImage;

    public void Open()
    {
        prohibitInteractImage.SetActive(true);
    }

    public void Close()
    {
        prohibitInteractImage.SetActive(false);
    }
}
