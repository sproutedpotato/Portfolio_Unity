using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guild
{
    public string guildName { get; private set; }
    public string guildInfo { get; private set; }
    public string guildID { get; private set; }
    public string guildMaster { get; private set; }
    public List<string> guildMember { get; private set; } = new List<string>();
    public Dictionary<string, bool> checkDic { get; private set; } = new Dictionary<string, bool>(); 

    public Guild() { }

    public Guild(string guildName, string guildInfo, string guildMaster)
    {
        this.guildName = guildName;
        this.guildInfo = guildInfo;
        this.guildMaster = guildMaster;

        guildMember.Add(guildMaster);
        checkDic[guildMaster] = false;
    }

    public void JoinGuild(string memberName, PlayerInfo player)
    {
        if (player.guild != null && player.guild != this)
        {
            Debug.Log($"플레이어 {memberName}는 이미 다른 길드({player.guild.guildName})에 가입되어 있습니다.");
            return;
        }

        if (guildMember.Count >= 30)
        {
            Debug.Log("길드 인원이 가득 찼습니다.");
            return;
        }

        if (guildMember.Contains(memberName))
        {
            Debug.Log("이미 이 길드에 가입된 멤버입니다.");
            return;
        }

        player.SetGuild(this);
        guildMember.Add(memberName);
        checkDic[memberName] = false;
        Debug.Log($"{memberName}가 {guildName} 길드에 가입했습니다.");
    }

    public void DailyCheck(string username)
    {
        checkDic[username] = true;
    }

    public void SetGuildMaster(string username)
    {
        guildMaster = username;
    }
}
