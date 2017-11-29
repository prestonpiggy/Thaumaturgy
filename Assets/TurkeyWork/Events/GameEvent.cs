using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Events {

    [CreateAssetMenu (menuName = "TurkeyWork/Game Event")]
    public class GameEvent : ScriptableObject {

        static Dictionary<string, GameEvent> gameEvents = new Dictionary<string, GameEvent> ();

        [ReadOnly, ShowInInspector]
        List<GameEventListener> listeners = new List<GameEventListener> ();

        [System.NonSerialized]
        public object EventData;

        public static void RaiseEvent (string eventName) {
            GameEvent gameEvent;
            if (gameEvents.TryGetValue (eventName, out gameEvent))
                gameEvent.Raise ();
            Debug.Log ($"Raised Event {gameEvent?.name}");
        }

        public static void RaiseEventWithData (string eventName, object eventData) {
            GameEvent gameEvent;
            if (gameEvents.TryGetValue (eventName, out gameEvent)) {
                gameEvent.RaiseWithData (eventData);
            }
            Debug.Log ($"Raised Event {gameEvent?.name}");
        }

        [Button]
        public void Raise () {
            for (int i = listeners.Count -1; i >= 0; i--)
                listeners[i].OnEventRaised ();
        }

        public void RaiseWithData (object eventData) {
            EventData = eventData;
            Raise ();
            eventData = null;
        }

        public void AddListener (GameEventListener listener) {
            listeners.Add (listener);
        }

        public void RemoveListener (GameEventListener listener) {
            listeners.Remove (listener);
        }

        private void OnEnable () {
            gameEvents.Add (this.name, this);
        }

        private void OnDisable () {
            gameEvents.Remove (this.name);
        }

    }

}