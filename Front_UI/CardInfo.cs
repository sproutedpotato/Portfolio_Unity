using System.Collections.Generic;
using UnityEngine;

public class CardInfo : MonoBehaviour
{
    [SerializeField] public List<Card> cardList;
    public Dictionary<string, Card> cardDict { get; private set; }

    public void InitializeDictionary()
    {
        cardDict = new Dictionary<string, Card>();

        foreach (Card entry in cardList)
        {
            if (!cardDict.ContainsKey(entry.cardName))
            {
                cardDict.Add(entry.cardName, entry);
            }
        }
    }

    public Card GetCardByName(string name)
    {
        if (cardDict.TryGetValue(name, out var card))
            return card;
        return null;
    }

    public List<Card> ReturnCardList()
    {
        return cardList;
    }
}