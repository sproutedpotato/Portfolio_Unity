using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private PlayerInfo playerInfo;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }


    void Start()
    {
        playerInfo.exchargeDiamond += ChangeDiamondText;
        type = PanelType.Diamond;
    }

    void OnEnable()
    {
        playerInfo.exchargeDiamond += ChangeDiamondText;
    }

    private void ChangeDiamondText()
    {
        text.text = playerInfo.diamond.ToString();
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