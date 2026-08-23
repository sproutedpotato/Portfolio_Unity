using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class X10Card : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private RandomSelect randomSelect;
    [SerializeField] private Image mainImage;
    [SerializeField] private Button button;
    [SerializeField] private int index;
    [SerializeField] private StartX10Gacha x10Gacha;
    [SerializeField] private ScoutResultDisableManager scoutManager;
    [SerializeField] private CharacterStorage characterStorage;
    private List<Card> cards;

    private void OnEnable()
    {
        cards = new List<Card>();
        mainImage.sprite = null;
        button.interactable = true;
    }

    void Update()
    {
        if (randomSelect.press == 10 && gameObject.activeSelf && Input.GetMouseButtonDown(0))
        {
            resultPanel.SetActive(false);
        }
    }

    public void OnClickCardButton()
    {
        cards = x10Gacha.ReturnCardList();
        mainImage.sprite = cards[index].cardImage;
        characterStorage.IncreaseCharStorageNum(cards[index].cardName);
        button.interactable = false;
    }

    private void OnDisable()
    {
        if (scoutManager != null)
        {
            scoutManager.Close();
        }
    }
}
