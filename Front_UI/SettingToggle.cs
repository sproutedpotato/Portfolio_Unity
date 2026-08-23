using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingToggle : MonoBehaviour
{
    [SerializeField] private GameObject NormalPanel;
    [SerializeField] private GameObject ExtraPanel;
    [SerializeField] private TextMeshProUGUI normalText;
    [SerializeField] private TextMeshProUGUI extraText;
    [SerializeField] private Image normalBackground;
    [SerializeField] private Image extraBackground;

    public void OnClickNormalPanelToggle()
    {
        NormalPanel.SetActive(true);
        ExtraPanel.SetActive(false);

        normalText.color = Color.white;
        extraText.color = Color.black;

        normalBackground.color = new Color32(0x46, 0x3D, 0x3D, 0xFF);
        extraBackground.color = Color.white;
    }

    public void OnClickExtraPanelToggle()
    {
        NormalPanel.SetActive(false);
        ExtraPanel.SetActive(true);

        normalText.color = Color.black;
        extraText.color = Color.white;

        normalBackground.color = Color.white;
        extraBackground.color = new Color32(0x46, 0x3D, 0x3D, 0xFF);
    }

    private void OnDisable()
    {
        normalBackground.color = Color.white;
        extraBackground.color = Color.white;
        normalText.color = Color.white;
        extraText.color = Color.black;
        NormalPanel.SetActive(true);
        ExtraPanel.SetActive(false);
    }
}
