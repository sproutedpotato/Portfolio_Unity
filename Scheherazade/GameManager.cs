using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int day;
    private int time; // 0 : 낮, 1 : 밤
    public bool isHaveKey { get; set; }
    public bool canMove { get; set; }
    // <0 : 효과 없음>, <1 : 공격력 0.5>, <2 : 체력 1>, <3 : 공격력 1>, <4 : 체력 2>, <5 : 공격력 1.5>, <6 : 체력 3>
    public int itemNum { get; set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        day = 0;
        time = 0;
        itemNum = -1;
        isHaveKey = true;
        canMove = true;
    }

    private void AddTime()
    {
        if (time >= 1)
        {
            time = 0;
        }
        else
        {
            time++;
        }
    }
    public void SkipToNextDay()
    {
        if(time == 1)
        {
            day++;
            itemNum = -1;
            AddTime();
        }
        else
        {
            AddTime();
        }
        Debug.Log("Time is " + time + ", Day is " + day);
    }

    public int ReturnTimeOrDay(string s)
    {
        if (s.Equals("Time"))
        {
            return time;
        }
        else
        {
            return day;
        }
    }
}
