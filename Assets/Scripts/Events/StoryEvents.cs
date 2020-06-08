using Events;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    private List<string> _events;

    private void Start()
    {
        _events = new List<string>();
    }

    private void Update()
    {
        if (!_events.Contains("hungerStrike") && ResourceManager.Instance.GetResourceAmount("food") < 10f)
        {
            HungerStrikeEvent();
        }
    }

    private void HungerStrikeEvent()
    { 
        EventManager.Instance.CreateEvent(3);
        _events.Add("hungerStrike");
    }
}
