﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Resources;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class EventManager
{
    private static readonly EventManager instance = new EventManager();

    public static EventManager Instance
    {
        get { return instance; }
    }


    /// <summary>
    /// event for when a event is created
    /// </summary>
    public event Action<Event> EventCreated;

    /// <summary>
    /// creates an event for the events
    /// </summary>
    /// <param name="id"></param>
    public void CreateEvent(int id)
    {
        EventStorage storage = new EventStorage();

        // Update every subscriber that an event has been made.
        EventCreated.Invoke(storage.GetEvent(id));
    }
}