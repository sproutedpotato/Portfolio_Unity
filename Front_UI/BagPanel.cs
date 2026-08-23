using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject panel;
    private bool isOpen => panel.activeSelf;
    public PanelType type { get; private set; }

    void Start()
    {
        type = PanelType.Bag;
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
