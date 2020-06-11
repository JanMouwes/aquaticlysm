using Events;
using Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to hold and check for when story-related events need to be showed.
/// </summary>
public class StoryEvents : MonoBehaviour
{
    // List to hold all invoked events to prevent looping.
    private List<string> _events;
    private float timeToSalvage;

    public bool eventOn;

    private void Start()
    {
        _events = new List<string>();
        timeToSalvage = 15f;
        eventOn = false;
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

        eventOn = false;

    }

    /// <summary>
    /// Creates the welcome event at the beginning of the game.
    /// </summary>
    private void BeginEvent()
    {
        EventManager.Instance.CreateEvent(0);
        _events.Add("begin");
    }

    /// <summary>
    /// Show a warning event, when the village's fod supply gets low for the first time.
    /// </summary>
    private void HungerStrikeEvent()
    {
        EventManager.Instance.CreateEvent(3);
        _events.Add("hungerStrike");
    }

    /// <summary>
    /// Shows Game Over -event, when food supply has been low for too long time.
    /// </summary>
    private void GameOverStarved()
    {
        Debug.Log("Here.");
        EventManager.Instance.CreateEvent(4);
        _events.Add("gameOver");
        timeToSalvage = 15f;
    }

    /// <summary>
    /// Shows the help event, when the HelpButton is clicked.
    /// </summary>
    public void ShowHelpEvent()
    {
        EventManager.Instance.CreateEvent(6);
    }


    /// <summary>
    /// Resets resources to initial starting-values and loads the starting menu.
    /// </summary>
    public void ReloadGame()
    {
        GameObject.Find("ResourceSystem").GetComponent<ResourceSystem>().ReloadResourceSystem();
        SceneManager.LoadScene("StartGame");
    }
}
