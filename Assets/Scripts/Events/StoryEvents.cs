using Events;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!_events.Contains("begin"))
        {
            BeginEvent();
        }
        
        if (_events.Contains("begin") && !_events.Contains("hungerStrike") && ResourceManager.Instance.GetResourceAmount("food") < 20f)
        {
            HungerStrikeEvent();
        }

        if (_events.Contains("begin") && !_events.Contains("gameOver") && ResourceManager.Instance.GetResourceAmount("food") <= 0f)
        {
            NotificationSystem.Instance.ShowNotification("AboutToStarve", 20);

            timeToSalvage -= Time.deltaTime;
            if (timeToSalvage <= 0f)
            { 
                GameOverStarved();
            }
        }
        else
        {
            timeToSalvage = 15f;
        }

    }

    private void BeginEvent()
    {
        EventManager.Instance.CreateEvent(0);
        _events.Add("begin");
    }

    
    private void HungerStrikeEvent()
    {
        EventManager.Instance.CreateEvent(3);
        _events.Add("hungerStrike");
    }

    private void GameOverStarved()
    {
        Debug.Log("Here.");
        EventManager.Instance.CreateEvent(4);
        _events.Add("gameOver");
        timeToSalvage = 15f;
    }

    public void ReloadGame()
    {
        GameObject.Find("ResourceSystem").GetComponent<ResourceSystem>().ReloadResourceSystem();
        SceneManager.LoadScene("StartGame");
    }
}
