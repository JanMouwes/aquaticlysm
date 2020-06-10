using UnityEngine;

namespace Events
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        public EventStorage storage;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(this.gameObject); }
            else { Instance = this; }
        }

        /// <summary>
        /// Creates an event for the events.
        /// </summary>
        /// <param name="id">Event id.</param>
        public void CreateEvent(int id)
        {
            // Update every subscriber that an event has been made.
            FindObjectOfType<EventUIView>().BuildEventUiView(storage.GetEvent(id));
        }

        private void OnDestroy()
        {
            Destroy(Instance.gameObject);
        }
    }
}