using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class EventUIView : MonoBehaviour
{
    public GameObject Agree;
    public GameObject Ok;
    private GameObject UI; 
    private int _eventid;
    private Event _event;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.EventCreated += UpdateEventUIView;
    }

    void UpdateEventUIView(Event input)
    {
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
        GlobalStateMachine.instance.ChangeState(new EventState());
        UI.transform.SetParent(transform,false);
        GameObject.Find("EventTitle").GetComponent<Text>().text = input.Title;
        GameObject.Find("EventText").GetComponent<Text>().text = input.Text;
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
