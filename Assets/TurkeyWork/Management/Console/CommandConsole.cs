using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurkeyWork.Management {

    public sealed class CommandConsole : MonoBehaviour {

        // Editor Assignables
        [Header ("Console")]
        public GameObject console;
        public InputField inputField;
        public Text historyEntry;
        public Transform histroyContainer;

        // A keycode for opening the console panel.
        public KeyCode toggleKey = KeyCode.Backslash;

        private CommandInterpreter interpreter;
        private string command;
        private int selectionID = -1;

        public void ReadCommand (string command) {
            UpdateHistoryDisplay (command);
            if (interpreter.SendCommand (command) && interpreter.LastResult != null)
                UpdateHistoryDisplay ($" --> {interpreter.LastResult}");

            inputField.text = "";
            selectionID = interpreter.CommandHistoryLength - 1;
            SelectInputField ();
            Invoke ("SelectInputField", 0.01f);
        }

        public void LogToConsole (string message) {
            UpdateHistoryDisplay (message);
        }

        void UpdateHistoryDisplay (string command) {
            var historyObject = Instantiate (historyEntry, histroyContainer);
            historyObject.text = command;
            historyObject.gameObject.SetActive (true);
        }

        private void Awake () {
            interpreter = CommandInterpreter.Instance;
            interpreter.Console = this;
            inputField?.onEndEdit.AddListener (ReadCommand);
        }

        private void Update () {
            if (Input.GetKeyDown (toggleKey)) {
                console.SetActive (!console.activeSelf);
                SelectInputField ();
            } else if (Input.GetKeyDown (KeyCode.UpArrow)) {
                selectionID = selectionID > 0 ? --selectionID : 0;
                if (interpreter.TryGetHistoryCommand (selectionID, out command)) {
                    inputField.text = command;
                }
            } else if (Input.GetKeyDown (KeyCode.DownArrow)) {
                selectionID = selectionID < interpreter.CommandHistoryLength - 1 ? ++selectionID : interpreter.CommandHistoryLength - 1;
                if (interpreter.TryGetHistoryCommand (selectionID, out command))
                    inputField.text = command;
            }
        }

        private void SelectInputField () {
            if (console.activeSelf) {
                inputField.Select ();
                inputField.ActivateInputField ();
            } else
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject (null);
        }
    }

}
