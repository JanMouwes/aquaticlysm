using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Resources;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Event
{
    private String Title;
    private String Text;
    private buttonstyle _buttonstyle;
    private readonly ResourceManager _resourceManager;
    
    private static readonly Event instance = new Event();

    public static Event Instance
    {
        get
        {
            return instance;
        }
    }
    
    private Event()
    {
        this._resourceManager = ResourceManager.Instance;
    }

    public event Action<String, String, buttonstyle> EventCreated;

    public void CreateEvent(int id)
    {
        switch (id)
        {
            case 1:
                Title = "Hi";
                Text = "tadaa";
                _buttonstyle = buttonstyle.OK;
                break;
            case 2:
                Title = "Storm";
                Text = "There has been a storm you lost some resources.\n\nWood -5";
                _resourceManager.DecreaseResource("Wood", 5);
                _buttonstyle = buttonstyle.OK;
                break;
        }

        EventCreated?.Invoke(Title, Text, _buttonstyle);
    }
}

public enum buttonstyle
{
    AgreeDisagree,
    OK,
} 
