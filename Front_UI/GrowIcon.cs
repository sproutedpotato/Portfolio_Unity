using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GrowIcon : MonoBehaviour
{
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private CharacterStorage characterStorage;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image typeImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI charnumText;
    [SerializeField] private TextMeshProUGUI typeLevelText;
    [SerializeField] private TextMeshProUGUI breakthroughText;
    [SerializeField] private TextMeshProUGUI typeNum;
    [SerializeField] private TextMeshProUGUI breakthroughNum;
    [SerializeField] private TextMeshProUGUI typeLevelNeedMoney;
    [SerializeField] private TextMeshProUGUI breakthroughNeedMoney;
    [SerializeField] private Sprite[] typeSprites;
    private Dictionary<string, Card> cardDict;
    private Dictionary<string, int> myDict;
    private Dictionary<Items, int> myitemDict;
    public string cardName { get; private set; }
    private Card charCard;

    public void Init(string name)
    {
        cardDict = cardInfo.cardDict;
        myDict = characterStorage.charDic;
        myitemDict = playerInfo.itemDic;
        cardName = name;
        charCard = cardDict[name];
        cardImage.sprite = charCard.cardImage;
        nameText.text = cardName;
        if(charCard.cardType == CardType.Attack) //레벨따라 이미지 바뀌게 하기~
        {
            typeImage.sprite = typeSprites[0];
            if(charCard.typeLevel < 5)
            {
                typeNum.text = "X " + myitemDict[Items.Upgrade_Attack].ToString();
            }
            else if(charCard.typeLevel < 10)
            {
                typeNum.text = "X " + myitemDict[Items.HighUpgrade_Attack].ToString();
            }
        }
        else if(charCard.cardType == CardType.Defense)
        {
            typeImage.sprite = typeSprites[1];
            if (charCard.typeLevel < 5)
            {
                typeNum.text = "X " + myitemDict[Items.Upgrade_Defense].ToString();
            }
            else if (charCard.typeLevel < 10)
            {
                typeNum.text = "X " + myitemDict[Items.HighUpgrade_Defense].ToString();
            }
        }
        else if (charCard.cardType == CardType.Heal)
        {
            typeImage.sprite = typeSprites[2];
            if (charCard.typeLevel < 5)
            {
                typeNum.text = "X " + myitemDict[Items.Upgrade_Heal].ToString();
            }
            else if (charCard.typeLevel < 10)
            {
                typeNum.text = "X " + myitemDict[Items.HighUpgrade_Heal].ToString();
            }
        }
        else
        {
            typeImage.sprite = typeSprites[3];
            if (charCard.typeLevel < 5)
            {
                typeNum.text = "X " + myitemDict[Items.Upgrade_Magic].ToString();
            }
            else if (charCard.typeLevel < 10)
            {
                typeNum.text = "X " + myitemDict[Items.HighUpgrade_Magic].ToString();
            }
        }
        charnumText.text = "+ " + myDict[cardName].ToString();
        typeLevelText.text = "+ " + cardDict[cardName].typeLevel.ToString();
        breakthroughText.text = "+ " + cardDict[cardName].breakthrough.ToString();
        breakthroughNum.text = "X " + myitemDict[Items.Breakthrough].ToString();
        typeLevelNeedMoney.text = ((cardDict[cardName].typeLevel + 1) * 800).ToString();
        breakthroughNeedMoney.text = ((cardDict[cardName].breakthrough + 1) * 2000).ToString();
    }
}
