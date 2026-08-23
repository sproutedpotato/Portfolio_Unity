using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonWithoutIPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject prohibitInteractiveImage;

    public void OnClickBackButton()
    {
        if(prohibitInteractiveImage != null)
        {
            prohibitInteractiveImage.SetActive(false);
        }
        panel.SetActive(false);
    }
}
