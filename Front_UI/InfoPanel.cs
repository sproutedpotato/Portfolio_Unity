using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private TextMeshProUGUI myName;
    [SerializeField] private TextMeshProUGUI myGuild;
    [SerializeField] private TextMeshProUGUI myTitle;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    private void OnEnable()
    {
        myName.text = playerInfo.playerName;
        if(playerInfo.guild == null)
        {
            myGuild.text = "";
        }
        else
        {
            myGuild.text = playerInfo.guild.guildName;
        }
        myTitle.text = "";
    }

    void Start()
    {
        type = PanelType.Info;
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