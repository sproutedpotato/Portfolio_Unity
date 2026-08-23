using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildMemberInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI memberName;
    [SerializeField] private Image checkImage;
    [SerializeField] private Sprite[] images;

    public void Init(string memberName, int num)
    {
        this.memberName.text = memberName;
        this.checkImage.sprite = images[num];
    }
}
