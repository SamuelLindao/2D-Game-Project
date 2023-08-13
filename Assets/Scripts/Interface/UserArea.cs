using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
public class UserArea : MonoBehaviour
{
    public static UserArea instance;
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI TimePlayed;
    public TextMeshProUGUI ObjectsDestroyed;
    public int value;
    [Header("If has not player info")]
    public DateTime StartTime;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PlayerInfo info = PlayerInfo.instance;
        if(info == null)
        {
            StartTime = DateTime.Now;
            PlayerName.text = "Start from the Menu :(";

        }
        else
        {
            StartTime = info.StartTime;
            if(info.name == "")
            {
                PlayerName.text = "No Name?? :(";
            }
            else
            {
                PlayerName.text = info.PlayerName;
            }
        }
    }

    private void Update()
    {
   
            TimePlayed.text = DateTime.Now.Subtract(StartTime).Hours.ToString("00") + ":";
            TimePlayed.text += DateTime.Now.Subtract(StartTime).Minutes.ToString("00") + ":";
            TimePlayed.text += DateTime.Now.Subtract(StartTime).Seconds.ToString("00");
      
    }

    public void NewValue()
    {
        ObjectsDestroyed.text = "OBJECTS : " + value.ToString();
        ObjectsDestroyed.transform.DOShakePosition(1f, 2.5f, 10, 90);
    }
}
