using Events;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    private List<string> _events;
    private float timeToSalvage;

    private void Start()
    {
        _events = new List<string>();
        timeToSalvage = 15f;
    }

    private void Update()
    {
        if (!_events.Contains("hungerStrike") && ResourceManager.Instance.GetResourceAmount("food") < 10f)
        {
            HungerStrikeEvent();
        }

        if (!_events.Contains("gameOver") && ResourceManager.Instance.GetResourceAmount("food") <= 0f)
        {
            NotificationSystem.Instance.ShowNotification("AboutToStarve", 20);

            timeToSalvage -= Time.deltaTime;

            if (timeToSalvage <= 0f && ResourceManager.Instance.GetResourceAmount("food") <= 0f)
                GameOver();
        }

    }

    private void HungerStrikeEvent()
    { 
        EventManager.Instance.CreateEvent(3);
        _events.Add("hungerStrike");
    }

    private void GameOver()
    {
        EventManager.Instance.CreateEvent(4);
        _events.Add("gameOver");
        timeToSalvage = 15f;
    }
}
