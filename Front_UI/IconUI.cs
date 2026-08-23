using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconUI : MonoBehaviour
{
    [SerializeField] public string panelType;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI countText;
    private GameObject characterPanel;
    private GameObject growCharPanel;
    public string characterName { get; private set; }

    public void SetupChar(Sprite sprite, string name, int count, GameObject characterPanel)
    {
        iconImage.sprite = sprite;
        nameText.text = name;
        countText.text = $"+ {count}";
        characterName = name;
        this.characterPanel = characterPanel;
    }

    public void SetupGrow(Sprite sprite, string name, int count, GameObject growCharPanel)
    {
        iconImage.sprite = sprite;
        nameText.text = name;
        countText.text = $"+ {count}";
        characterName = name;
        this.growCharPanel = growCharPanel;
    }

    public void OnClickIconButton()
    {
        if (panelType.Equals("Char"))
        {
            characterPanel.SetActive(true);
            characterPanel.GetComponent<CharacterPanel>().Init(nameText.text);
        }
        else if (panelType.Equals("Grow"))
        {
            growCharPanel.SetActive(true);
            growCharPanel.GetComponent<GrowIcon>().Init(nameText.text);
        }
    }
}
