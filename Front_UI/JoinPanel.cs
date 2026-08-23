using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JoinPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject guildMakePanel;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Join;
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void OnClickMakeGuildButtonInGuildPanel()
    {
        guildMakePanel.SetActive(true);
    }
}
