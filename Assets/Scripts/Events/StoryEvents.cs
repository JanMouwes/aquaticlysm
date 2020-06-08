using Events;
using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvents : MonoBehaviour
{
    private bool alreadyInvoked;

    // Start is called before the first frame update
    void Start()
    {
        alreadyInvoked = false;
    }

    public void VillagersGetHungry()
    {
        if (!alreadyInvoked && ResourceManager.Instance.GetResourceAmount("food") < 10f)
        {
            new WaitForSeconds(20);
            EventManager.Instance.CreateEvent(3);
            alreadyInvoked = true;
        }
    }
}
