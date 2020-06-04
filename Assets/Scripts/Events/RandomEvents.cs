using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RandomEvents : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.Instance.CreateEvent(0);
        StartCoroutine(EventTimer());
    }

    private IEnumerator EventTimer()
    {
        //Wait for between 60 and 300 seconds
        yield return new WaitForSeconds(Random.Range(60,300));
        // 50%
        if (Random.value < 0.5f)
        {
            EventManager.Instance.CreateEvent(1);
        }
        // 50%
        else
        {
            EventManager.Instance.CreateEvent(2);
        }
        ResetState();
    }

    private void ResetState()
    {
        StartCoroutine(EventTimer());
    }
}
