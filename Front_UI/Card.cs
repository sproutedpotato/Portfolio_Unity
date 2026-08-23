using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardGrade { Three, Four, Five }
public enum CardType { Attack, Defense, Heal, Magic }

[System.Serializable]
public class Card
{
    public string cardName;
    public Sprite cardImage;
    public CardGrade cardGrade;
    public int weight;
    public Sprite cardIcon;
    public CardType cardType;
    public int typeLevel;
    public int breakthrough;

    public Card() { }
    public Card(Card card)
    {
        this.cardName = card.cardName;
        this.cardImage = card.cardImage;
        this.cardGrade = card.cardGrade;
        this.weight = card.weight;
        this.cardIcon = card.cardIcon;
        this.cardType = card.cardType;
        this.typeLevel = 0;
        this.breakthrough = 0;
    }

    public void Breakthrough()
    {
        this.breakthrough++;
    }

    public void TypeLevelUp()
    {
        this.typeLevel++;
    }
}
