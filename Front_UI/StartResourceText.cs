using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartResourceText : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI diamondText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayResourceTextChange());
    }

    IEnumerator DelayResourceTextChange()
    {
        yield return null;
        coinText.text = playerInfo.coin.ToString();
        diamondText.text = playerInfo.diamond.ToString();
    }
}
