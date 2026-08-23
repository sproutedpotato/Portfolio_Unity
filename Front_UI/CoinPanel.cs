using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerInfo playerInfo;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Coin;
        text.text = playerInfo.coin.ToString();
    }

    void OnEnable()
    {
        playerInfo.exchargeCoin += ChangeCoinText;
    }

    private void ChangeCoinText()
    {
        text.text = playerInfo.coin.ToString();
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}