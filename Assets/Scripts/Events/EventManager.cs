using System;
using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
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
        /// Event for when a event is created.
        /// </summary>
        public event Action<Event> EventCreated;

        /// <summary>
        /// Creates an event for the events.
        /// </summary>
        /// <param name="id">Event id.</param>
        public void CreateEvent(int id)
        {
            EventStorage storage = new EventStorage();
            Debug.Log(id);
            // Update every subscriber that an event has been made.
            EventCreated.Invoke(storage.GetEvent(id));
        }
    }
}
