using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterIcon : MonoBehaviour
{
    [SerializeField] private CharacterStorage characterStorage;
    [SerializeField] private CardInfo cardInfo;
    [SerializeField] private GameObject iconPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private GameObject characterPanel;

    private Dictionary<string, int> charDic;
    private Dictionary<string, Card> charInfo;

    private void OnEnable()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        charDic = new Dictionary<string, int>();
        charDic = characterStorage.charDic;
        charInfo = new Dictionary<string, Card>();
        charInfo = cardInfo.cardDict;

        foreach (var (name, count) in charDic)
        {
           if(count > 0)
            {
                Card card = charInfo[name];
                GameObject icon = Instantiate(iconPrefab, gridParent);

                IconUI iconUI = icon.GetComponent<IconUI>();
                if (iconUI.panelType.Equals("Char"))
                {
                    iconUI.SetupChar(card.cardIcon, card.cardName, count, characterPanel);
                }
                else
                {
                    iconUI.SetupGrow(card.cardIcon, card.cardName, count, characterPanel);
                }
            }
        }
    }
}
