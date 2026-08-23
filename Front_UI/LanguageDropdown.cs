using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    public void OnDropdownChanged()
    {
        int index = dropdown.value;
        Debug.Log("선택된 인덱스: " + index);
        Debug.Log("선택된 옵션: " + dropdown.options[index].text);
    }
}
