using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class GrowButton : MonoBehaviour
{
    [SerializeField] private UIDB db;
    [SerializeField] private GrowIcon icon;
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private GameObject breakthroughCheckPanel;
    [SerializeField] private GameObject typeLevelCheckPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private GameObject prohibitedInteractImage;
    [SerializeField] private TextMeshProUGUI typeLevelNeedItem;
    [SerializeField] private TextMeshProUGUI breakthroughNeedItem;
    [SerializeField] private TextMeshProUGUI typeLevelNeedMoney;
    [SerializeField] private TextMeshProUGUI breakthroughNeedMoney;
    [SerializeField] private TextMeshProUGUI typeLevel;
    [SerializeField] private TextMeshProUGUI breakthroughLevel;
    private Dictionary<Items, int> myItemDict;
    private Dictionary<string, Card> cardDict;

    public void StartBreakthrough()
    {
        breakthroughCheckPanel.SetActive(true);
    }

    public void StartTypeLevelUp()
    {
        typeLevelCheckPanel.SetActive(true);
    }

    public void OnClickBreakthroughButton()
    {
        cardDict = cardInfo.cardDict;

        Card curCard = cardDict[icon.cardName];
        int myCoin = playerInfo.coin;

        if (myItemDict[Items.Breakthrough] > 0 && myCoin >= 2000 * (curCard.breakthrough + 1))
        {
            playerInfo.UseCoinToUpgrade(2000 * (curCard.breakthrough + 1));
            myItemDict[Items.Breakthrough] -= 1;
            db.UpdateData("Items", "Amount", playerInfo.itemDic[Items.Breakthrough], "Name", "Breakthrough");
            curCard.Breakthrough();
            db.UpdateData("Cards", "Breakthrough", curCard.breakthrough, "Name", "Breakthrough");
        }
        else
        {
            prohibitedInteractImage.SetActive(true);
            failedPanel.SetActive(true);
        }

        breakthroughNeedMoney.text = (2000 * (curCard.breakthrough + 1)).ToString();
        breakthroughNeedItem.text = "X " + myItemDict[Items.Breakthrough].ToString();
        breakthroughLevel.text = "+ " + cardDict[icon.cardName].breakthrough;
        breakthroughCheckPanel.SetActive(false);
    }

    public void OnClickLevelUpButton()
    {
        myItemDict = playerInfo.itemDic;
        cardDict = cardInfo.cardDict;
        Card curCard = cardDict[icon.cardName];

        CardType type = curCard.cardType;
        Items needs;
        if(type == CardType.Attack)
        {
            if(curCard.breakthrough < 1 || curCard.typeLevel <= 5)
            {
                needs = Items.Upgrade_Attack;
            }
            else
            {
                needs = Items.HighUpgrade_Attack;
            }
        }
        else if (type == CardType.Defense)
        {
            if (curCard.breakthrough < 1 || curCard.typeLevel <= 5)
            {
                needs = Items.Upgrade_Defense;
            }
            else
            {
                needs = Items.HighUpgrade_Defense;
            }
        }
        else if (type == CardType.Heal)
        {
            if (curCard.breakthrough < 1 || curCard.typeLevel <= 5)
            {
                needs = Items.Upgrade_Heal;
            }
            else
            {
                needs = Items.HighUpgrade_Heal;
            }
        }
        else
        {
            if (curCard.breakthrough < 1 || curCard.typeLevel <= 5)
            {
                needs = Items.Upgrade_Magic;
            }
            else
            {
                needs = Items.HighUpgrade_Magic;
            }
        }

        int myCoin = playerInfo.coin;
        int cardLevel = curCard.typeLevel;
        if (myItemDict[needs] > 0 && myCoin >= (cardLevel + 1) * 800)
        {
            playerInfo.UseCoinToUpgrade((cardLevel + 1) * 800);
            myItemDict[needs] -= 1;
            db.UpdateData("Items", "Amount", playerInfo.itemDic[needs], "Name", needs.ToString());
            curCard.TypeLevelUp();
            db.UpdateData("Cards", "Level", curCard.typeLevel, "Name", curCard.cardName);
        }
        else
        {
            prohibitedInteractImage.SetActive(true);
            failedPanel.SetActive(true);
        }

        typeLevelNeedMoney.text = (800 * (curCard.typeLevel + 1)).ToString();
        typeLevelNeedItem.text = "X " + myItemDict[needs].ToString();
        typeLevel.text = "+ " + cardDict[icon.cardName].typeLevel;
        typeLevelCheckPanel.SetActive(false);
    }
}
