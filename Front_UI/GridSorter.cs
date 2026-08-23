using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSorter : MonoBehaviour
{
    private List<IconUI> iconUIs = new List<IconUI>();
    [SerializeField] private RectTransform layoutRoot;
    private void OnEnable()
    {
        StartCoroutine(DelayedInit());
    }

    private void CacheIcons()
    {
        iconUIs.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            IconUI icon = transform.GetChild(i).GetComponent<IconUI>();
            if (icon != null)
            {
                iconUIs.Add(icon);
            }
        }
    }

    private void SortIconsByName()
    {
        iconUIs.Sort((a, b) => string.Compare(a.characterName, b.characterName));

        for (int i = 0; i < iconUIs.Count; i++)
        {
            iconUIs[i].transform.SetSiblingIndex(i);
        }

        if (layoutRoot != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(layoutRoot);
        }
    }

    private IEnumerator DelayedInit()
    {
        CacheIcons();
        SortIconsByName();
        yield return null;
    }
}
