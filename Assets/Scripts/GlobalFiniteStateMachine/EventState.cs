using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventState : MonoBehaviour, IState
{
    private SelectionController _selectionContoller;
    public void Start()
    {
        _selectionContoller= FindObjectOfType<SelectionController>();
        _selectionContoller.enabled = false;
        Time.timeScale = 0f;
    }

    public void Execute()
    {
        
    }

    public void Stop()
    {
        Time.timeScale = 1f;
        _selectionContoller.enabled = true;
    }
}
