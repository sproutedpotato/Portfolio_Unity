using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakePanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField guildName;
    [SerializeField] private TMP_InputField guildInfo;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Button makeButton;
    [SerializeField] private GameObject prohibitInteractImage;
    [SerializeField] private GameObject joinPanel;
    [SerializeField] private GameObject guildPanel;
    [SerializeField] private GameObject failPanel;

    private void Update()
    {
        if (guildName.text.Equals(""))
        {
            makeButton.interactable = false;
        }
        else
        {
            makeButton.interactable = true;
        }
    }

    public void OnClickMakeGuildInMakePanel()
    {
        if(playerInfo.diamond < 1000)
        {
            failPanel.SetActive(true);
            prohibitInteractImage.SetActive(true);
            return;
        }
        else
        {
            playerInfo.BuyItem("Diamond", 1000);
        }
        Guild guild = new Guild(guildName.text, guildInfo.text, playerInfo.playerName);
        playerInfo.SetGuild(guild);
        joinPanel.SetActive(false);
        guildPanel.GetComponent<IPanel>().Open();
        guildName.text = "";
        guildInfo.text = "";
        gameObject.SetActive(false);
    }
}
