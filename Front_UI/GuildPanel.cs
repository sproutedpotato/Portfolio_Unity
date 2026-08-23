using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject memberPanel;
    [SerializeField] private GameObject dailycheckPanel;
    [SerializeField] private GameObject prohibitInteractIamge;
    [SerializeField] private TextMeshProUGUI guildName;
    [SerializeField] private TextMeshProUGUI guildMaster;
    [SerializeField] private TextMeshProUGUI guildInfo;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Button dailycheckButton;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Guild;
    }

    public void Open()
    {
        Guild guild = playerInfo.guild;
        guildName.text = guild.guildName;
        guildMaster.text = guild.guildMaster;
        guildInfo.text = guild.guildInfo;
        if (guild.checkDic[playerInfo.playerName])
        {
            dailycheckButton.interactable = false;
        }
        panel.SetActive(true);
    }

    public void Close()
    {
        guildName.text = "";
        guildMaster.text = "";
        guildInfo.text = "";
        panel.SetActive(false);
    }

    public void OnClickMemberButton()
    {
        memberPanel.SetActive(true);
        prohibitInteractIamge.SetActive(true);
    }

    public void OnClickActiveExitPanel()
    {
        prohibitInteractIamge.SetActive(true);
        exitPanel.SetActive(true);
    }

    public void OnClickExitGuildButton()
    {
        playerInfo.ExitGuild();
        gameObject.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void OnClickDailyCheckButton()
    {
        playerInfo.guild.DailyCheck(playerInfo.playerName);
        prohibitInteractIamge.SetActive(true);
        dailycheckPanel.SetActive(true);
        Debug.Log("player " + playerInfo.guild.checkDic[playerInfo.playerName]);
        dailycheckButton.interactable = false;
    }
}