using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonBehavior : MonoBehaviour
{
    [SerializeField] private ManagePanel managePanel;
    [SerializeField] private BagPanel bagPanel;
    [SerializeField] private GrowPanel growPanel;
    [SerializeField] private ScoutPanel scoutPanel;
    [SerializeField] private GuildPanel guildPanel;
    [SerializeField] private JoinPanel joinPanel;
    [SerializeField] private ShopPanel shopPanel;
    [SerializeField] private SettingPanel settingPanel;
    [SerializeField] private MailPanel mailPanel;
    [SerializeField] private FriendPanel friendPanel;
    [SerializeField] private MissionPanel missionPanel;
    [SerializeField] private InfoPanel infoPanel;
    [SerializeField] private CoinPanel coinPanel;
    [SerializeField] private DiamondPanel diamondPanel;
    [SerializeField] private PlayerInfo playerInfo;

    public void OnClickManageButton()
    {
        StartCoroutine(ManagePanelRoutine(managePanel));
    }
    public void OnClickBagButton()
    {
        bagPanel.Open();
    }
    public void OnClickGrowButton()
    {
        StartCoroutine(ManagePanelRoutine(growPanel));
    }
    public void OnClickScoutButton()
    {
        scoutPanel.Open();
    }
    public void OnClickGuildButton()
    {
        if (playerInfo.IsHaveGuild())
        {
            guildPanel.Open();
        }
        else
        {
            joinPanel.Open();
        }
    }
    public void OnClickShopButton()
    {
        shopPanel.Open();
    }
    public void OnClickSettingButton()
    {
        settingPanel.Open();
    }
    public void OnClickMailButton()
    {
        mailPanel.Open();
    }
    public void OnClickMissionButton()
    {
        missionPanel.Open();
    }
    public void OnClickFriendButton()
    {
        friendPanel.Open();
    }
    public void OnClickInfoButton()
    {
        infoPanel.Open();
    }
    public void OnClickCoinButton()
    {
        coinPanel.Open();
    }
    public void OnClickDiamondButton()
    {
        diamondPanel.Open();
    }

    private IEnumerator ManagePanelRoutine(IPanel panel)
    {
        yield return null;

        panel.Open();
    }
}
