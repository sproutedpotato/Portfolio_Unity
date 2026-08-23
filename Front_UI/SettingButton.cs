using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private Image HDRButtonImage;
    [SerializeField] private TextMeshProUGUI HDRText;
    [SerializeField] private TextMeshProUGUI FPSText;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private GameObject languagePanel;
    [SerializeField] private GameObject couponPanel;
    [SerializeField] private GameObject prohibitInteractiveImage;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject couponUsePanel;
    [SerializeField] private GameObject couponFailedPanel;

    private bool isHDR, isFPS60;

    private void Start()
    {
        Application.targetFrameRate = 60;
        isFPS60 = true;
        if(targetCamera != null)
        {
            isHDR = targetCamera.allowHDR;
        }
        UpdateHDRUI();
    }

    public void OnClickHDRButton()
    {
        isHDR = !isHDR;
        targetCamera.allowHDR = isHDR;
        UpdateHDRUI();
        UpdateFPSUI();
    }

    public void OnClickFPSButton()
    {
        isFPS60 = !isFPS60;
        Application.targetFrameRate = 30;
        UpdateFPSUI();
    }

    public void OnClickTermsofServiceButton()
    {
        Application.OpenURL("https://github.com/sproutedpotato/Roll_A_Ball/blob/main/README.md");
    }

    public void OnClickStaffButton()
    {
        Application.OpenURL("https://github.com/sproutedpotato");
    }

    public void OnClickLanguageButton()
    {
        prohibitInteractiveImage.SetActive(true);
        languagePanel.SetActive(true);
    }

    public void OnClickCouponButton()
    {
        prohibitInteractiveImage.SetActive(true);
        couponPanel.SetActive(true);
    }

    public void OnClickAccountButton()
    {
        Debug.Log("Account");
    }

    public void OnClickCouponPanelYesButton()
    {
        string input = inputField.text.ToString();

        if (string.Equals(input, "TestCoupon"))
        {
            couponUsePanel.SetActive(true);
        }
        else
        {
            couponFailedPanel.SetActive(true);
        }
    }

    private void UpdateHDRUI()
    {
        if(targetCamera != null)
        {
            HDRText.text = isHDR ? "HDR ON" : "HDR OFF";
            HDRButtonImage.color = isHDR ? new Color32(0xFF, 0xFF, 0xFF, 0xFF) : new Color32(0x65, 0x65, 0x65, 0xFF);
        }
    }

    private void UpdateFPSUI()
    {
        FPSText.text = isFPS60 ? "FPS 60" : "FPS 30";
    }
}
