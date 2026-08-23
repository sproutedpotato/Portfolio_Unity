using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class CheckPanelButton : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject checkPanel;
    [SerializeField] private GameObject exchargeCompletePanel;
    [SerializeField] private GameObject exchargeFailedPanel;
    [SerializeField] private GameObject prohibitInteractiveImage;
    [SerializeField] private TextMeshProUGUI[] texts;
    [SerializeField] private TextMeshProUGUI[] prices;
    [SerializeField] private PlayerInfo playerInfo;

    private int index;
    public void OnClickDiamondYesButton()
    {
        int num = int.Parse(texts[index].text.Split(" ")[0]);
        playerInfo.ExchargeDiamond(num);
        checkPanel.SetActive(false);
        exchargeCompletePanel.SetActive(true);
    }

    public void OnClickCoinYesButton()
    {
        int price = int.Parse(prices[index].text);
        checkPanel.SetActive(false);
        if (playerInfo.diamond < price)
        {
            exchargeFailedPanel.SetActive(true);
            return;
        }
        string input = texts[index].text.Split(" ")[0];
        if (input.Contains(","))
        {
            input = input.Replace(",", "");
        }

        int num = int.Parse(input);
        playerInfo.ExchargeCoin(num, price);
        exchargeCompletePanel.SetActive(true);
    }

    public void OnClickNoButton()
    {
        checkPanel.SetActive(false);
        prohibitInteractiveImage.SetActive(false);
    }

    public void OnClickExchargeCompleteButton()
    {
        exchargeCompletePanel.SetActive(false);
        prohibitInteractiveImage.SetActive(false);
    }

    public void GetIndex(int index)
    {
        this.index = index;
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
