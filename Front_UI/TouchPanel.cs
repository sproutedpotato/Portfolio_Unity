using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TextMeshProUGUI panelText;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private CharacterStorage characterStorage;
    private Dictionary<string, int> myDict;
    private string charName;

    public void OnPointerDown(PointerEventData eventData)
    {
        myDict = characterStorage.charDic;
        charName = characterNameText.text;
        messagePanel.SetActive(true);
        panelText.text = "Current : " + myDict[charName];
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        messagePanel.SetActive(false);
    }
}
