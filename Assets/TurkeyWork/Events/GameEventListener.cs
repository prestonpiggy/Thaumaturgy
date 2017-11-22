using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TurkeyWork.Events {

    public class GameEventListener : MonoBehaviour, IEqualityComparer<GameEventListener> {

        [SerializeField] GameEvent gameEvent;
        [SerializeField] UnityEvent onEvent;

        int id;

        public void OnEventRaised () {
            onEvent.Invoke ();
        }

        void Awake () {
            id = GetInstanceID ();
        }

        private void OnEnable () {
            gameEvent.AddListener (this);
        }

        private void OnDisable () {
            gameEvent.RemoveListener (this);
        }

        public bool Equals (GameEventListener x, GameEventListener y) {
            return x.id == y.id;
        }

        public int GetHashCode (GameEventListener obj) {
            return obj.id;
        }
    }

}