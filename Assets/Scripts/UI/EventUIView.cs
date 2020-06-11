using System;
using Events;
using UnityEngine;
using UnityEngine.UI;
using Event = Events.Event;

public class EventUIView : MonoBehaviour
{
    private Event _event;
    public GameObject OneOptionUI;
    public GameObject TwoOptionsUI;
    private GameObject UI;
    
    /// <summary>
    /// Builds the Ui for the event Based on the information given with the Event that is given.
    /// </summary>
    /// <param name="input">Information For the Event.</param>
    public void BuildEventUiView(Event input)
    {
        // save input
        _event = input;
        switch (_event.ButtonStyle)
        {
            // Check the button style of the event.
            case ButtonStyle.TwoOptions:
                // Create the TwoOptionsUI and link the buttons to the actions.
                UI = Instantiate(TwoOptionsUI, TwoOptionsUI.transform.position, TwoOptionsUI.transform.rotation);
                UI.transform.Find("YesButton").GetComponent<Button>().onClick.AddListener(GreenPressed);
                UI.transform.Find("YesButton").transform.Find("YesButtonText").GetComponent<Text>().text = _event.ButtonText[0];
                UI.transform.Find("NoButton").GetComponent<Button>().onClick.AddListener(RedPressed);
                UI.transform.Find("NoButton").transform.Find("NoButtonText").GetComponent<Text>().text = _event.ButtonText[1];
                break;
            case ButtonStyle.OneOption:
                // Create the OneOptionsUI and link the button to the action.
                UI = Instantiate(OneOptionUI, OneOptionUI.transform.position, OneOptionUI.transform.rotation);
                UI.transform.Find("OkayButton").GetComponent<Button>().onClick.AddListener(GreenPressed);
                UI.transform.Find("OkayButton").transform.Find("ButtonText").GetComponent<Text>().text = _event.ButtonText[0];
                break;
            default:
                throw new Exception("Unknown UI created");
        }

        // Set create ui to be the child of this gameobject.
        UI.transform.SetParent(transform, false);
        // Change the title.
        UI.transform.Find("EventContainer/EventTitle").GetComponent<Text>().text = _event.Title;
        // Change the body text.
        UI.transform.Find("EventContainer/Content/EventText").GetComponent<Text>().text = _event.Text;
        // Change the state to an event state.
        GlobalStateMachine.instance.ChangeState(new EventState());
        // Animate.
        iTween.ScaleFrom(UI, iTween.Hash("scale", new Vector3(0, 0, 0), "ignoretimescale", true, "time", 0.5f));
        GameObject.Find("HelpButton").GetComponent<Button>().enabled = false; ;
    }

    /// <summary>
    /// When Event is dismissed animation is played and stated is changed to the play state.
    /// </summary>
    private void DismissEventUi()
    {
        // Animate
        iTween.ScaleTo(UI,
                       iTween.Hash("scale", new Vector3(0, 0, 0), "ignoretimescale", true, "time", 0.5f, "oncomplete",
                                   "DestroyUI", "oncompletetarget", gameObject));
        // Change state to play state
        GlobalStateMachine.instance.ChangeState(new PlayState());
        GameObject.Find("HelpButton").GetComponent<Button>().enabled = true;
    }


    /// <summary>
    /// Destroys the UI object,
    /// Gets called when ITween animation is completed.
    /// </summary>
    private void DestroyUI()
    {
        Destroy(UI);
    }


    /// <summary>
    /// Runs when the green button on the UI is pressed.
    /// </summary>
    private void GreenPressed()
    {
        DismissEventUi();
        if (_event.Actions.Count != 0)
            _event.Actions[0]();
    }

    /// <summary>
    /// Runs when the red button on the UI is pressed.
    /// </summary>
    private void RedPressed()
    {
        DismissEventUi();
        if (_event.Actions.Count > 1)
            _event.Actions[1]();
    }
}
