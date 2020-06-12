using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Events;
using UnityEngine;
using UnityEngine.Serialization;

public class RandomEvents : MonoBehaviour
{
    public AudioSource StormEffect;
    public AudioSource TradeEffect;

    [Tooltip("Min Value for time between events")]
    public int minTime = 600;
    [Tooltip("Max Value for time between events")]
    public int maxTime = 2400;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(EventTimer());
    }

    private IEnumerator EventTimer()
    {
        
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
            StormEffect.Play();
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
