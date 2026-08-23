using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemText : MonoBehaviour
{
    [SerializeField] private PlayerInfo playerInfo;
    [SerializeField] private TextMeshProUGUI[] texts;

    private Items[] item = { Items.Upgrade_Attack, Items.Upgrade_Defense, Items.Upgrade_Heal, Items.Upgrade_Magic,
                                Items.HighUpgrade_Attack, Items.HighUpgrade_Defense, Items.HighUpgrade_Heal, Items.HighUpgrade_Magic,
                                Items.Breakthrough, Items.GoldKey, Items.Energy, Items.Gift};

    private void OnEnable()
    {
        ChangeItemAmount();
    }

    public void ChangeItemAmount()
    {
        if (item.Length != texts.Length)
        {
            return;
        }
        
        for (int i = 0; i < item.Length; i++)
        {
            texts[i].text = playerInfo.itemDic[item[i]].ToString();
        }
    }
}
