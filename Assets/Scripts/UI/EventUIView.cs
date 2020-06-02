using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUIView : MonoBehaviour
{
    public GameObject backgound;

    private String _eventText;
    private String _eventTitle;
    // Start is called before the first frame update
    void Start()
    {
        Event.Instance.EventCreated += UpdateEventUIView;
        _eventText = GameObject.Find("EventText").GetComponent<Text>().text;
        _eventTitle = GameObject.Find("EventTitle").GetComponent<Text>().text;
        backgound.SetActive(false);
    }

    void UpdateEventUIView(String Title, String text, buttonstyle buttonstyle)
    {
        _eventText = text;
        _eventTitle = Title;
        backgound.SetActive(true);
    }
}
