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
        if (!_events.Contains("hungerStrike") && ResourceManager.Instance.GetResourceAmount("food") < 20f)
        {
            HungerStrikeEvent();
        }

        //if (!_events.Contains("firstStorm") && _events.Contains("hungerStrike"))
        //{
        //    DelayStoryEvent();
        //    FirstStormEvent();
        //}

        //if (!_events.Contains("gameOverEveryoneDead") &&
        //        GameObject.FindGameObjectsWithTag("Character").Length == 0)
        //{ 
        //    GameOverEveryoneDead();
        //}

        if (!_events.Contains("gameOver") && ResourceManager.Instance.GetResourceAmount("food") <= 0f)
        {
            NotificationSystem.Instance.ShowNotification("AboutToStarve", 20);

            timeToSalvage -= Time.deltaTime;
            Debug.Log(timeToSalvage);
            if (timeToSalvage <= 0f)
            { 
                GameOverStarved();
                ReloadGame();
            }
        }
        else
        {
            timeToSalvage = 15f;
        }

    }

    private IEnumerable DelayStoryEvent()
    { 
        yield return new WaitForSeconds(30);
    }

    private void HungerStrikeEvent()
    {
        EventManager.Instance.CreateEvent(3);
        _events.Add("hungerStrike");
    }

    //private void FirstStormEvent()
    //{
    //    EventManager.Instance.CreateEvent(5);
    //    _events.Add("firstStorm");
    //}

    private void GameOverStarved()
    {
        Debug.Log("Here.");
        EventManager.Instance.CreateEvent(4);
        _events.Add("gameOver");
        timeToSalvage = 15f;
    }

    //private void GameOverEveryoneDead()
    //{
    //    EventManager.Instance.CreateEvent(6);
    //    _events.Add("gameOver");
    //    timeToSalvage = 15f;
    //    ReloadGame();
    //}

    public void ReloadGame()
    {
        GameObject.Find("ResourceSystem").GetComponent<ResourceSystem>().ReloadResourceSystem();
        SceneManager.LoadScene("StartGame");
    }
}
