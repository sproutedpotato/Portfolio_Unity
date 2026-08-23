using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MimicHP : MonoBehaviour
{
    [SerializeField]
    private Slider hpBar;

    public Mimic enemy;
    void Start()
    {
        enemy = GetComponent<Mimic>();
        hpBar.minValue = 0;
        hpBar.maxValue = enemy.maxHp;
        hpBar.value = enemy.currentHp;
    }
    public void SetHP(float currentHP, float maxHP)
    {
        hpBar.value = currentHP / maxHP;
    }
    void Update()
    {
        hpBar.value = enemy.currentHp;
    }
}
