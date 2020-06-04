using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class EventUIView : MonoBehaviour
{
    public GameObject Agree;
    public GameObject Ok;
    private GameObject UI; 
    private int _eventid;
    private Event _event;
    private bool firstrun = false;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.EventCreated += UpdateEventUIView;
    }

    void UpdateEventUIView(Event input)
    {
        Debug.Log("call");

        _event = input;
        if (input.Buttonstyle == buttonstyle.AgreeDisagree)
        {
            UI = Instantiate (Agree, Agree.transform.position, Agree.transform.rotation) as GameObject;
            UI.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(YesPressed);
            UI.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(NoPressed);
        }
        else
        {
            UI = Instantiate (Ok, Ok.transform.position, Ok.transform.rotation);
            UI.transform.Find("OkayButton").GetComponent<Button>().onClick.AddListener(OkayPressed);
        }
        UI.transform.SetParent(transform, false);
        UI.transform.Find("EventContainer/EventTitle").GetComponent<Text>().text = _event.Title;
        UI.transform.Find("EventContainer/Content/EventText").GetComponent<Text>().text = _event.Text;
        GlobalStateMachine.instance.ChangeState(new EventState());
        iTween.ScaleFrom(UI,iTween.Hash("scale", new Vector3(0,0,0), "ignoretimescale", true, "time", 0.5f));
    }

    void DismissEventUi()
    {
        iTween.ScaleTo(UI,iTween.Hash("scale", new Vector3(0,0,0), "ignoretimescale", true, "time", 0.5f, "oncomplete", "DestroyUI", "oncompletetarget", gameObject));
        GlobalStateMachine.instance.ChangeState(new PlayState());
    }

    void DestroyUI()
    {
        Destroy(UI);
    }

    void YesPressed()
    {
        DismissEventUi();
        _event.events[0]();
    }

    void NoPressed()
    {
        DismissEventUi();
        _event.events[1]();
    }

    void OkayPressed()
    {
        DismissEventUi();
        _event.events[0]();
    }
}
