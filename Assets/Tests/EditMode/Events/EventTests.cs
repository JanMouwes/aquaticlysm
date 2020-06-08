using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Event = Events.Event;

namespace Tests
{
    public class EventTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void whenCreatingEvent_ShouldReturnCreatedEvent()
        {
            //Arrange
            EventStorage storage = new EventStorage();
            Event test = new Event
            {
                Text = "test",
                Title = "test",
                Actions = new List<Action> {() => Debug.Log("test")},
                ButtonStyle = ButtonStyle.OneOption
            };
            //Act
            storage.CreateEvent("test", "test", ButtonStyle.OneOption, new List<string>(){"test"}, new List<Action> {() => Debug.Log("test")});
            //Assert
            Event temp = storage.GetEvent(3);
            Assert.AreEqual(test.Text, temp.Text);
            Assert.AreEqual(test.Title, temp.Title);
            Assert.AreEqual(test.ButtonStyle, temp.ButtonStyle);
        }
    }
}