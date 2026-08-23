using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoutPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Sprite[] scoutImages;
    [SerializeField] private Image scoutImage;
    [SerializeField] private GameObject x1Panel;
    [SerializeField] private GameObject x10Panel;
    [SerializeField] private GameObject x1CheckPanel;
    [SerializeField] private GameObject x10CheckPanel;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private GameObject prohibitInteractImage;
    [SerializeField] private PlayerInfo playerInfo;
    private bool isOpen => panel.activeSelf;
    public int scoutIndex { get; private set; }
    public PanelType type { get; private set; }

    void OnEnable()
    {
        scoutIndex = 0;
        scoutImage.sprite = scoutImages[scoutIndex];
    }

    void Start()
    {
        type = PanelType.Scout;
        scoutIndex = 0;
        scoutImage.sprite = scoutImages[scoutIndex];
    }
    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void PressLeftArrow()
    {
        scoutIndex--;
        if(scoutIndex < 0)
        {
            scoutIndex = scoutImages.Length - 1;
        }

        scoutImage.sprite = scoutImages[scoutIndex];
    }

    public void PressRightArrow()
    {
        scoutIndex++;
        if (scoutIndex > scoutImages.Length - 1)
        {
            scoutIndex = 0;
        }

        scoutImage.sprite = scoutImages[scoutIndex];
    }

    public void OnClickX1Button()
    {
        prohibitInteractImage.SetActive(true);
        x1CheckPanel.SetActive(true);
    }

    public void OnClickX10Button()
    {
        prohibitInteractImage.SetActive(true);
        x10CheckPanel.SetActive(true);
    }

    public void OnClickDoX1ScoutButton()
    {
        if (playerInfo.ScoutOneTime())
        {
            x1Panel.SetActive(true);
            prohibitInteractImage.SetActive(false);
        }
        else
        {
            failedPanel.SetActive(true);
        }
        x1CheckPanel.SetActive(false);
    }

    public void OnClickDoX10ScoutButton()
    {
        if (playerInfo.ScoutTenTime())
        {
            x10Panel.SetActive(true);
            prohibitInteractImage.SetActive(false);
        }
        else
        {
            failedPanel.SetActive(true);
        }
        x10CheckPanel.SetActive(false);
    }
}