using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuildMember : MonoBehaviour
{
    [SerializeField] private GameObject memberPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Guild guild;
    [SerializeField] private PlayerInfo playerInfo;

    private void OnEnable()
    {
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        guild = playerInfo.guild;
        Dictionary<string, bool> myGuildDict = guild.checkDic;
        foreach(var memberName in guild.guildMember)
        {
            var obj = Instantiate(memberPrefab, gridParent);
            int num = guild.checkDic[memberName] ? 1 : 0;
            obj.GetComponent<GuildMemberInfo>().Init(memberName, num);
        }
    }
}
