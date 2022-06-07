using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour {

    public static PlayerStats Instance = null;

    private int money = Settings.StartMoney;
    private int waves = 0;

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There are 2 PlayerStats in the scene");
            return;
        }
        Instance = this;
    }

    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            UI.instance.UpdateMoneyText();
        }
    }
    public int Waves
    {
        get
        {
            return waves;
        }
        set
        {
            waves = value;
        }
    }

}
