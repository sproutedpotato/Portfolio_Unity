using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStorage : MonoBehaviour
{
    public Dictionary<string, int> charDic { get; private set; }
    [SerializeField] private UIDB db;
    // Start is called before the first frame update
    void Start()
    {
        charDic = new Dictionary<string, int>()
        {
            { "Three", db.SelectData<int>("Cards", "Amount", "Name", "Three") },
            { "Four", db.SelectData<int>("Cards", "Amount", "Name", "Four") },
            { "Five", db.SelectData<int>("Cards", "Amount", "Name", "Five") },
        };
    }

    public void IncreaseCharStorageNum(string name)
    {
        charDic[name] += 1;
        db.UpdateData("Cards", "Amount", charDic[name], "Name", name);
        Debug.Log("Name is " + name + " and Count is " + charDic[name]);
    }
}
