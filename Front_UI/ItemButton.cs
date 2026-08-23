using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private UIDB db;
    [SerializeField] private int price;
    [SerializeField] private string money;
    [SerializeField] private GameObject prohibitInteractImage;
    [SerializeField] private GameObject checkPanel;
    [SerializeField] private GameObject failedPanel;
    private Items purchaseItem;

    public void OnClickPurchaseItemButton()
    {
        if((money.Equals("Coin") && playerInfo.coin < price) || (money.Equals("Diamond") && playerInfo.diamond < price))
        {
            prohibitInteractImage.SetActive(true);
            failedPanel.SetActive(true);
        }
        else
        {
            prohibitInteractImage.SetActive(true);
            checkPanel.SetActive(true);
        }
    }

    private void PurchaseItem(string item)
    {
        if (System.Enum.TryParse(item, out Items purchaseItem))
        {
            if (purchaseItem.Equals(Items.Energy))
            {
                playerInfo.AddItemToBag(purchaseItem, 30);
                db.UpdateData("Items", "Amount", playerInfo.itemDic[purchaseItem], "Name", item);
            }
            else
            {
                playerInfo.AddItemToBag(purchaseItem, 1);
                db.UpdateData("Items", "Amount", playerInfo.itemDic[purchaseItem], "Name", item);
            }
        }
        else
        {
            Debug.Log("Wrong Value...");
        }
    }

    public void OnClickItemBuyYesButton(string item)
    {
        if (money.Equals("Coin"))
        {
            playerInfo.BuyItem("Coin", price);
            PurchaseItem(item);
            prohibitInteractImage.SetActive(false);
            checkPanel.SetActive(false);
        }
        else if (money.Equals("Diamond"))
        {
            playerInfo.BuyItem("Diamond", price);
            PurchaseItem(item);
            prohibitInteractImage.SetActive(false);
            checkPanel.SetActive(false);
        }
    }
}
