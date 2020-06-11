using System;
using System.Collections.Generic;
using Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Events
{
    public class EventStorage
    {
        private readonly List<Event> _events = new List<Event>();

        public EventStorage()
        {
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
                List<string> buttontext = new List<string>();
                buttontext.Add("Im ready!");
                CreateEvent(title, text, buttonStyle,buttontext, actions);
            }
            // 1 : Overboard Event
            {
                const string title = "Overboard";
                const string text = "Your Villager is about to throw a tantrum and throwing something in the water.\n " +
                                    "What will you do?";
                const ButtonStyle buttonStyle = ButtonStyle.TwoOptions;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string>();
                Action action = () => ResourceManager.Instance.DecreaseResource("wood", 5);
                buttontext.Add("Save the fresh water.");
                actions.Add(action);
                action = () => ResourceManager.Instance.DecreaseResource("water", 5);
                buttontext.Add("Save the building wood.");
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
                Action action = () => ResourceManager.Instance.DecreaseResource("water", 10);//GameObject.Destroy(GameObject.FindGameObjectWithTag("Character"));
                buttontext.Add("Cover the wood stock.");
                actions.Add(action);
                action = () => ResourceManager.Instance.DecreaseResource("wood", 5);
                buttontext.Add("Take cover.");
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
                Action action = () => GameObject.FindObjectOfType<StoryEvents>().ReloadGame();
                actions.Add(action);
                List<string> buttontext = new List<string>();
                buttontext.Add("Start over.");
                CreateEvent(title, text, buttonStyle, buttontext, actions);
            }
            // 5 : Initial storm
            {
                const string title = "Storm";
                const string text = "The strom is coming. Prepare your settlement, your villagers can take cover or salvage your property!";
                const ButtonStyle buttonStyle = ButtonStyle.TwoOptions;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string>();
                Action action = () => ResourceManager.Instance.DecreaseResource("water", 10);// GameObject.Destroy(GameObject.FindGameObjectWithTag("Charater"));
                buttontext.Add("Cover the wood stock.");
                actions.Add(action);
                action = () => ResourceManager.Instance.DecreaseResource("wood", 20);
                buttontext.Add("Take cover.");
                actions.Add(action);
                CreateEvent(title, text, buttonStyle, buttontext, actions);
            }
            // 6 : Everyone dead
            {
                const string title = "Help";
                string text = "Move: WASD\n" +
                              "Rotate: QE\n" +
                              "Zoom: Scroll Up / Scroll Down\n\n" +
                              "Holding Shift down: \tDisable building snapping, Queue actions.\n\n" +
                              "Wood: Wood is used as building resource and can usually be found looting barrels and exploring with the boat.\n\n" +
                              "Metal: Metal is used to build buildings. Metal can be found in some of the barrels drifting in the sea and exploring.\n\n" +
                              "Energy: Energy is used generating clean water. Generating energy can be done by building solar panels, that generate a set amount of energy during the day." +
                                "Energy can be used as is or it can be stored by building batteries.\n\n" +
                              "Water: Water is used for farming and can be generated by building a desalinator. Cleaning water requires energy.\n\n" +
                              "Plastic: Plastic is used as a building material and can be found by looting barrels found drifting in the sea and exploring.\n\n" +
                              "Food: Food can be gathered by fishing with the boat, or by building a farm and farming with a character. In order to store food you need to build a storage. Farming costs clean water!\n\n" +
                              "House: wood - 25\n" +
                              "Solar Panel: metal - 35, plastic - 50, wood - 100\n" +
                              "Dock: wood - 10\n" +
                              "Farm: wood - 30, metal - 5\n" +
                              "Desalinator: wood - 200, metal - 70, plastic - 35\n" +
                              "Battery: wood - 50, metal - 30, plastic - 10\n";
                const ButtonStyle buttonStyle = ButtonStyle.OneOption;
                List<Action> actions = new List<Action>();
                List<string> buttontext = new List<string> { "Got it!" };
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