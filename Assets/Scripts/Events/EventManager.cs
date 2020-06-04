using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Resources;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EventManager
{
    private string Title;
    private string Text;
    private buttonstyle _buttonstyle;
    private readonly ResourceManager _resourceManager;

    private static readonly EventManager instance = new EventManager();

    public static EventManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    private EventManager()
    {
        this._resourceManager = ResourceManager.Instance;
    }

    public event Action<Event> EventCreated;

    public void CreateEvent(int id)
    {
        Event createdEvent = new Event();
        Action action;
        createdEvent.events = new List<Action>();
        switch (id)
        {
            case 1:
                createdEvent.Title = "Your Fucked";
                createdEvent.Text = "Your Villager is about to trow something in the water.\n " +
                            "you can only save 1 item what will it be?\n\n" +
                            "Green = lose 5 wood\n" +
                            "Red = lose 5 water";
                createdEvent.Buttonstyle = buttonstyle.AgreeDisagree;
                action = () => _resourceManager.DecreaseResource("wood", 5);
                createdEvent.events.Add(action);
                action = () => _resourceManager.DecreaseResource("water", 5);
                createdEvent.events.Add(action);
                break;
            
            case 2:
                createdEvent.Title = "Storm";
                createdEvent.Text = "There has been a storm you lost some resources.\n\nWood -5";
                createdEvent.Buttonstyle = buttonstyle.OK;
                action = () => _resourceManager.DecreaseResource("wood", 5);
                createdEvent.events.Add(action);
                break;
        }
        EventCreated.Invoke(createdEvent);
    }
}

public struct Event
{
    public string Title;
    public string Text;
    public buttonstyle Buttonstyle;
    public List<Action> events;
}

public enum buttonstyle
{
    AgreeDisagree,
    OK,
} 
