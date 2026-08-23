using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Toggle dailyToggle;
    [SerializeField] private Image dailyBackground;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Mission;
    }

    public void Open()
    {
        panel.SetActive(true);
        StartCoroutine(SelectToggleNextFrame());
    }

    public void Close()
    {
        dailyToggle.isOn = true;
        panel.SetActive(false);
    }

    IEnumerator SelectToggleNextFrame()
    {
        yield return null;
        dailyToggle.isOn = true;
        dailyBackground.color = new Color32(0x46, 0x3D, 0x3D, 0xFF);
    }
}