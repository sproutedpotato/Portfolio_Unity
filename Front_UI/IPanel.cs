using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType
{
    Manage,
    Bag,
    Grow,
    Scout,
    Guild,
    Join,
    Shop,
    Friend,
    Mission,
    Mail,
    Info,
    Setting,
    Coin,
    Diamond,
    Error
}
public interface IPanel
{
    void Open();
    void Close();
}
