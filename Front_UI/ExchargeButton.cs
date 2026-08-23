using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExchargeButton : MonoBehaviour
{
    [SerializeField] private GameObject checkPanel;
    [SerializeField] private GameObject prohibitInteractiveImage;
    [SerializeField] private int index;

    public void OnClickExchargeButton()
    {
        if(checkPanel != null)
        {
            checkPanel.SetActive(true);
            checkPanel.GetComponent<CheckPanelButton>().GetIndex(index);
        }
        if (prohibitInteractiveImage != null)
        {
            prohibitInteractiveImage.SetActive(true);
        }
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
