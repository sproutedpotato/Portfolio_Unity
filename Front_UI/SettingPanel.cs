using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Toggle normalToggle;
    [SerializeField] private Image normalBackground;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Setting;
    }

    public void Open()
    {
        panel.SetActive(true);
        StartCoroutine(SelectToggleNextFrame());
    }

    public void Close()
    {
        normalToggle.isOn = true;
        panel.SetActive(false);
    }

    IEnumerator SelectToggleNextFrame()
    {
        yield return null;
        normalToggle.isOn = true;
        normalBackground.color = new Color32(0x46, 0x3D, 0x3D, 0xFF);
    }
}