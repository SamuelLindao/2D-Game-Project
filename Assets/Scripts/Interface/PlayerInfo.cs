using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;
    public string PlayerName;
    public DateTime StartTime;

    private void Start()
    {
        StartTime = DateTime.Now;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;        DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
