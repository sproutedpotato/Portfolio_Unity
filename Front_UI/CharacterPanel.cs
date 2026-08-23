using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject managePanel;
    [SerializeField] private GameObject growPanel;
    [SerializeField] private GameObject imagePanel;
    [SerializeField] private Image imagePanelImage;
    private Dictionary<string, Card> cardDict;
    private string cardName;
    private Card charCard;

    public void Init(string name)
    {
        cardDict = cardInfo.cardDict;
        cardName = name;
        charCard = cardDict[name];
        cardImage.sprite = charCard.cardImage;
        nameText.text = cardName;
    }

    public void OnClickGoToGrowButton()
    {
        managePanel.SetActive(false);
        growPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnClickImageButton()
    {
        if(imagePanel != null)
        {
            imagePanel.SetActive(true);
        }
        if(imagePanelImage != null)
        {
            imagePanelImage.sprite = cardImage.sprite;
        }
    }

    public void OnClickCharacterImagePanel()
    {
        if(imagePanel != null)
        {
            imagePanel.SetActive(false);
        }
    }
}
