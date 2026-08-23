using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartX10Gacha : MonoBehaviour
{
    [SerializeField] private RandomSelect randomSelect;
    private List<Card> cards;

    private void OnEnable()
    {
        StartCoroutine(DelayedInit());
    }

    public List<Card> ReturnCardList()
    {
        return this.cards;
    }

    private IEnumerator DelayedInit()
    {
        yield return null;

        randomSelect.ResultSelect10Time();
        cards = randomSelect.ReturnResult();
        Debug.Log("Selected. Count is " + cards.Count);
    }
}
