using System;

namespace Events
{
    public class EventManager
    {
        private static readonly EventManager instance = new EventManager();

        public static EventManager Instance
        {
            get { return instance; }
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

            // Update every subscriber that an event has been made.
            EventCreated.Invoke(storage.GetEvent(id));
        }
    }
}
