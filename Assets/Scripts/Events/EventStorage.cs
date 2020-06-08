using System;
using System.Collections.Generic;
using Resources;

public class EventStorage
{
    private readonly List<Event> _events = new List<Event>();
    private readonly ResourceManager _resourceManager;

    public EventStorage()
    {
        _resourceManager = ResourceManager.Instance;
        CreateAllEvents();
    }

    public Event GetEvent(int id)
    {
        return _events[id];
    }

    /// <summary>
    /// Create an Event.
    /// </summary>
    /// <param name="title">Event title.</param>
    /// <param name="text">Event text.</param>
    /// <param name="buttonStyle">Button style.</param>
    /// <param name="actions">The actions that are invoked.</param>
    public void CreateEvent(string title, string text, ButtonStyle buttonStyle, List<Action> actions)
    {
        Event createdEvent = new Event {Title = title, Text = text, ButtonStyle = buttonStyle, Actions = actions};
        _events.Add(createdEvent);
    }

    /// <summary>
    /// Creates all the predefined Events.
    /// </summary>
    private void CreateAllEvents()
    {
        // 0 : Welcome Event
        {
            const string title = "Welcome";
            const string text = "Ask Meri";
            const ButtonStyle buttonStyle = ButtonStyle.OneOption;
            List<Action> actions = new List<Action>();
            CreateEvent(title, text, buttonStyle, actions);
        }
        // 1 : Your Fucked event
        {
            const string title = "Your Fucked";
            const string text = "Your Villager is about to trow something in the water.\n " +
                                "you can only save 1 item what will it be?\n\n"             +
                                "Green = lose 5 wood and lose 5 water\n"                    +
                                "Red = lose 5 water";
            const ButtonStyle buttonStyle = ButtonStyle.TwoOptions;
            List<Action> actions = new List<Action>();
            Action action = () => _resourceManager.DecreaseResource("wood", 5);
            action += () => _resourceManager.DecreaseResource("water", 5);
            actions.Add(action);
            action = () => _resourceManager.DecreaseResource("water", 5);
            actions.Add(action);
            CreateEvent(title, text, buttonStyle, actions);
        }
        // 2 : Storm Event 
        {
            const string title = "Storm";
            const string text = "There has been a storm you lost some resources.\n\nWood -5";
            const ButtonStyle buttonStyle = ButtonStyle.OneOption;
            List<Action> actions = new List<Action>();
            Action action = () => _resourceManager.DecreaseResource("wood", 5);
            actions.Add(action);
            CreateEvent(title, text, buttonStyle, actions);
        }
    }
}

/// <summary>
/// The Struct For the Event.
/// Holds the information for the event.
/// </summary>
public struct Event
{
    public string Title;
    public string Text;
    public ButtonStyle ButtonStyle;
    public List<Action> Actions;
}

/// <summary>
/// Buttonstyle for the event.
/// </summary>
public enum ButtonStyle
{
    TwoOptions,
    OneOption
}
