using System;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Events
{
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
        public void CreateEvent(string title, string text, ButtonStyle buttonStyle,List<string> buttontext, List<Action> actions)
        {
            Event createdEvent = new Event {Title = title, Text = text, ButtonStyle = buttonStyle, ButtonText = buttontext, Actions = actions};
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
                const string text = "The world is not as it was. Global warming ran its course and now the mainland is uninhabitable." +
                                    " Those who survived ﬂed to sea and band together to stand a chance in this new world. But the sea is" +
                                    " not your only enemy…\n\nIn this Real-Time Strategy-game you will play as a group of survivors and try to" +
                                    " make sure your members don’t die. Manage resources such as wood and food, lead expeditions, and build up your" +
                                    " ﬂoating settlement. Experience the story that unfolds through the choices you make";
                const ButtonStyle buttonStyle = ButtonStyle.OneOption;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string> {"Im ready!"};
                CreateEvent(title, text, buttonStyle,buttontext, actions);
            }
            // 1 : Overboard Event
            {
                const string title = "Overboard";
                const string text = "Your Villager is about to trow something in the water.\n " +
                                    "you can only save 1 item what will it be?";
                const ButtonStyle buttonStyle = ButtonStyle.TwoOptions;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string>();
                Action action = () => _resourceManager.DecreaseResource("wood", 5);
                buttontext.Add("-5 wood");
                actions.Add(action);
                action = () => _resourceManager.DecreaseResource("water", 5);
                buttontext.Add("-5 water");
                actions.Add(action);
                CreateEvent(title, text, buttonStyle, buttontext, actions);
            }
            // 2 : Storm Event 
            {
                const string title = "Storm";
                const string text = "The strom is coming. Prepare your settlement, what will you salvage?";
                const ButtonStyle buttonStyle = ButtonStyle.TwoOptions;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string>();
                Action action = () => _resourceManager.DecreaseResource("wood", 5);
                buttontext.Add("Retie the water pilars.");
                actions.Add(action);
                action = () => _resourceManager.DecreaseResource("water", 5);
                buttontext.Add("Cover the wood stock");
                actions.Add(action);
                CreateEvent(title, text, buttonStyle, buttontext, actions);
            }
            // 3 : Villagers get hungry
            {
                const string title = "Hunger strike";
                const string text = "Your villagers are hungry! You need to start gathering food so your villagers wont starve to death.\n\n" +
                                    "Click on the boat to select it. Then click on the fishing hook to start fishing.";
                const ButtonStyle buttonStyle = ButtonStyle.OneOption;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string> { "Ahoi!" };
                CreateEvent(title, text, buttonStyle, buttontext, actions);
            }
            // 4 : Village starved to death
            {
                const string title = "Game over";
                string text = "Your villagers starved to death.\n" +
                                    "You survived " + DateTime.DayCounter + " days.";
                const ButtonStyle buttonStyle = ButtonStyle.OneOption;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string>();
                Action action = () => SceneManager.LoadScene("LocalSea");
                buttontext.Add("Start over.");
                actions.Add(action);
                CreateEvent(title, text, buttonStyle, buttontext, actions);
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
        public List<string> ButtonText;
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
}