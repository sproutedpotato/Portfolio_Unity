using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeName : MonoBehaviour, IPanel
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject nameError;
    [SerializeField] private Button checkButton;
    [SerializeField] private GameObject prohibitInteractiveImage;
    [SerializeField] private PlayerInfo playerInfo;

    private void Start()
    {
        inputField.characterLimit = 10;
        checkButton.interactable = false;
    }

    private void Update()
    {
        if(inputField.text != null)
        {

            checkButton.interactable = true;
        }
        else
        {
            checkButton.interactable = false;
        }
    }

    public void OnClickChangeNameButton()
    {
        if(inputField.text != null && inputField.text.Length <= 10 && !inputField.text.Contains(" ") && inputField.text.Length != 0)
        {
            string prevName = playerInfo.playerName;
            Guild guild = playerInfo.guild;
            bool isChecked = guild.checkDic[prevName];
            nameText.text = inputField.text;
            playerInfo.ChangeName(nameText.text);
            guild.guildMember.Remove(prevName);
            guild.guildMember.Add(nameText.text);
            guild.checkDic.Remove(prevName);
            guild.checkDic[nameText.text] = isChecked;
            if (guild.guildMaster.Equals(prevName))
            {
                guild.SetGuildMaster(nameText.text);
            }
            
            gameObject.SetActive(false);
            prohibitInteractiveImage.SetActive(false);
        }
        else
        {
            nameError.SetActive(true);
        }
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
