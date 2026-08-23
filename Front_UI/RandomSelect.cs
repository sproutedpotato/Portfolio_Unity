using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> result = new List<Card>();
    public int total;
    public int press { get; private set; }

    private void OnEnable()
    {
        press = 0;
        total = 0;
        result.Clear();
        for (int i = 0; i < deck.Count; i++)
        {
            total += deck[i].weight;
        }
    }   

    public void ResultSelect()
    {
        result.Add(RandomCard());
    }

    public void ResultSelect10Time()
    {
        result.Clear();
        for(int i = 0; i < 10; i++)
        {
            result.Add(RandomCard());
            Debug.Log("Card is Added.");
        }
    }

    public List<Card> ReturnResult()
    {
        return result;
    }

    public Card RandomCard()
    {
        int weight = 0;
        int selectNum = 0;

        selectNum = Mathf.RoundToInt(total * Random.Range(0.0f, 1.0f));

        for(int i = 0; i < deck.Count; i++)
        {
            weight += deck[i].weight;
            if(selectNum <= weight)
            {
                Card temp = new Card(deck[i]);
                Debug.Log("Selected Card is : " + temp.cardName);
                return temp;
            }
        }

        return null;
    }

    public void AddPress()
    {
        press += 1;
    }
}
