using System;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        public EventStorage storage;
        private static EventManager _instance;

        public static EventManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }

       

        /// <summary>
        /// Creates an event for the events.
        /// </summary>
        /// <param name="id">Event id.</param>
        public void CreateEvent(int id)
        {
            // Update every subscriber that an event has been made.
            GameObject.FindObjectOfType<EventUIView>().BuildEventUiView(storage.GetEvent(id));
        }

        private void OnDestroy()
        {
            Destroy(_instance.gameObject);
        }
    }
}
