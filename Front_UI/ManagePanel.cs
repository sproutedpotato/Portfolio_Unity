using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Manage;
    }

    public void Open()
    {
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
