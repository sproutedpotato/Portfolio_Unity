using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class X1Card : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private RandomSelect randomSelect;
    [SerializeField] private Image mainImage;
    [SerializeField] private Button button;
    [SerializeField] private ScoutResultDisableManager scoutManager;
    [SerializeField] private CharacterStorage characterStorage;
    private List<Card> cards;

    private void OnEnable()
    {
        cards = new List<Card>();
        randomSelect.ResultSelect();
        mainImage.sprite = null;
        button.interactable = true;
    }

    void Update()
    {
        if (randomSelect.press == 1 && gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            resultPanel.SetActive(false);
        }
    }

    public void OnClickCardButton()
    {
        cards = randomSelect.ReturnResult();
        mainImage.sprite = cards[0].cardImage;
        characterStorage.IncreaseCharStorageNum(cards[0].cardName);
        button.interactable = false;
    }

    private void OnDisable()
    {
        if(scoutManager != null)
        {
            scoutManager.Close();
        }
    }
}
