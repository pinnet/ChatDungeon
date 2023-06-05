using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatusLogger 
{
    public event System.EventHandler OnLogUpdated;

    private List<string> log;

    public List<string> GetLog() { 
    
        return log;
    }
    public StatusLogger()
    {
        log = new List<string>();
    }
    public void Add(string status)
    {
        string timeStamp = DateTime.Now.ToString("hh:mm:ss");
        log.Add("[" + timeStamp + "]: " + status);
        OnLogUpdated?.Invoke(this,new EventArgs());
    }

}