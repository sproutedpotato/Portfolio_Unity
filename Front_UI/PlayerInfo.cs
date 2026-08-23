using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum Items
{
    Upgrade_Attack,
    Upgrade_Defense,
    Upgrade_Heal,
    Upgrade_Magic,
    HighUpgrade_Attack,
    HighUpgrade_Defense,
    HighUpgrade_Heal,
    HighUpgrade_Magic,
    Breakthrough,
    Energy,
    GoldKey,
    Gift
}
public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private UIDB db;
    public Action exchargeDiamond;
    public Action exchargeCoin;
    public int diamond { get; private set; }
    public int coin { get; private set; }
    public Guild guild { get; private set; }
    public string playerName { get; private set; }
    public Dictionary<Items, int> itemDic { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //get player name;
        playerName = "Tipop";
        guild = null;
        //guild = db.SelectData<Guild>("PlayerInfoDB", "Guild", "Name", playerName);
        diamond = db.SelectData<int>("PlayerInfoDB", "Diamond", "Name", playerName);
        coin = db.SelectData<int>("PlayerInfoDB", "Coin", "Name", playerName);
        itemDic = new Dictionary<Items, int>()
        { 
            { Items.Upgrade_Attack, db.SelectData<int>("Items", "Amount", "Name", "Upgrade_Attack") },
            { Items.Upgrade_Defense, db.SelectData<int>("Items", "Amount", "Name", "Upgrade_Defense") },
            { Items.Upgrade_Heal, db.SelectData<int>("Items", "Amount", "Name", "Upgrade_Heal") },
            { Items.Upgrade_Magic, db.SelectData < int >("Items", "Amount", "Name", "Upgrade_Magic") },
            { Items.HighUpgrade_Attack, db.SelectData<int>("Items", "Amount", "Name", "HighUpgrade_Attack") },
            { Items.HighUpgrade_Defense, db.SelectData < int >("Items", "Amount", "Name", "HighUpgrade_Defense") },
            { Items.HighUpgrade_Heal, db.SelectData<int>("Items", "Amount", "Name", "HighUpgrade_Heal") },
            { Items.HighUpgrade_Magic, db.SelectData < int >("Items", "Amount", "Name", "HighUpgrade_Magic") },
            { Items.Breakthrough, db.SelectData<int>("Items", "Amount", "Name", "Breakthrough") },
            { Items.Energy, db.SelectData<int>("Items", "Amount", "Name", "Energy") },
            { Items.GoldKey, db.SelectData < int >("Items", "Amount", "Name", "GoldKey") },
            { Items.Gift, db.SelectData<int>("Items", "Amount", "Name", "Gift") }
        };
        guild = null;
    }

    public void ChangePlayerName(string name)
    {
        playerName = name;
    }

    public void ExchargeDiamond(int amount)
    {
        diamond += amount;
        exchargeDiamond?.Invoke();
        db.UpdateData("PlayerInfoDB", "Diamond", diamond, "Name", playerName);
    }

    public void ExchargeCoin(int amount, int diamondAmount)
    {
        coin += amount;
        diamond -= diamondAmount;
        exchargeDiamond?.Invoke();
        exchargeCoin?.Invoke();
        db.UpdateData("PlayerInfoDB", "Diamond", diamond, "Name", playerName);
        db.UpdateData("PlayerInfoDB", "Coin", coin, "Name", playerName);
    }
    
    public bool ScoutOneTime()
    {
        if(diamond < 100)
        {
            return false;
        }
        else
        {
            diamond -= 100;
            exchargeDiamond?.Invoke();
            db.UpdateData("PlayerInfoDB", "Diamond", diamond, "Name", playerName);
            return true;
        }
    }

    public bool ScoutTenTime()
    {
        if (diamond < 900)
        {
            return false;
        }
        else
        {
            diamond -= 900;
            exchargeDiamond?.Invoke();
            db.UpdateData("PlayerInfoDB", "Diamond", diamond, "Name", playerName);
            return true;
        }
    }

    public void BuyItem(string type, int price)
    {
        if(type.Equals("Diamond"))
        {
            diamond -= price;
            exchargeDiamond?.Invoke();
            db.UpdateData("PlayerInfoDB", "Diamond", diamond, "Name", playerName);
        }
        else if(type.Equals("Coin"))
        {
            coin -= price;
            exchargeCoin?.Invoke();
            db.UpdateData("PlayerInfoDB", "Coin", coin, "Name", playerName);
        }
    }

    public void AddItemToBag(Items item, int amount)
    {
        if (itemDic.ContainsKey(item))
        {
            itemDic[item] += amount;
            Debug.Log("Item is " + item + ", Amount is " + itemDic[item]);
        }
        else
        {
            Debug.Log("Wrong Key...");
        }
    }

    public void UseCoinToUpgrade(int amount)
    {
        coin -= amount;
        exchargeCoin?.Invoke();
        db.UpdateData("PlayerInfoDB", "Coin", coin, "Name", playerName);
    }

    public bool IsHaveGuild()
    {
        if(guild == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SetGuild(Guild guild)
    {
        if (this.guild != null)
        {
            Debug.Log("Guild is not null");
            return;
        }
        this.guild = guild;
        db.UpdateData("PlayerInfoDB", "Guild", guild.guildName, "Name", playerName);
        Debug.Log("Guild is " + guild);
    }

    public void ExitGuild()
    {
        if(guild != null)
        {
            guild = null;
        }
    }

    public void ChangeName(string name)
    {
        db.UpdateData("PlayerInfoDB", "Name", name, "Name", playerName);
        this.playerName = name;
    }
}
