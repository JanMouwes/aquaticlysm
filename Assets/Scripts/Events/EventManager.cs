using System;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager _instance;
        private EventStorage storage;
        private EventUIView eventUi;

        public static EventManager Instance { get { return _instance; } }


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
            } else {
                _instance = this;
            }
            storage = new EventStorage();
            eventUi = FindObjectOfType<EventUIView>();
        }
        

        /// <summary>
        /// Creates an event for the events.
        /// </summary>
        /// <param name="id">Event id.</param>
        public void CreateEvent(int id)
        {
            // Update every subscriber that an event has been made.
            eventUi.BuildEventUiView(storage.GetEvent(id));
        }
    }
}
