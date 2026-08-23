using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject prohibitInteractiveImage;
    [SerializeField] private GameObject changeNamePanel;
    [SerializeField] private GameObject changeTitlePanel;

    public void OnClickChangeNameButton()
    {
        prohibitInteractiveImage.SetActive(true);
        changeNamePanel.SetActive(true);
    }

    public void OnClickChangeTitleButton()
    {
        prohibitInteractiveImage.SetActive(true);
        changeTitlePanel.SetActive(true);
    }
}
