using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Events;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    public AudioSource StromEffect;
    public AudioSource TradeEffect;

    [Tooltip("Min Value for time between events")]
    public int minTime = 30;
    [Tooltip("Max Value for time between events")]
    public int maxTime = 300;
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.CreateEvent(0);
        StartCoroutine(EventTimer());
    }

    private IEnumerator EventTimer()
    {
        //Wait for between 60 and 300 seconds
        yield return new WaitForSeconds(Random.Range(minTime,maxTime));
        // 50%
        if (Random.value < 0.5f)
        {
            EventManager.Instance.CreateEvent(1);
        }
        // 50%
        else
        {
            EventManager.Instance.CreateEvent(2);
            StromEffect.Play();
        }
        ResetState();
    }

    private void ResetState()
    {
        StartCoroutine(EventTimer());
    }

    public void InvokeStoryEvent(int eventIndex)
    {
        EventManager.Instance.CreateEvent(eventIndex);
    }
}
