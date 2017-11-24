using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Management {

    public sealed class CommandRegistryDisplay : MonoBehaviour {

        private CommandInterpreter interpreter;
        public CommandBase[] Commands { get; private set; }

        private void OnEnable () {
            gameObject.hideFlags = HideFlags.DontSaveInBuild;
        }

        void Awake () {
            interpreter = GetComponent<CommandInterpreter> ();
            if (!interpreter) {
                Debug.LogError ("Command Registry Display needs to be attached to the same GameObject with CommandInterpreter!");
                return;
            }
        }

        private void Start () {
            GetCommands ();
            interpreter.OnCommandsUpated += OnCommandsUpdated;
        }

        private void OnCommandsUpdated () {
            GetCommands ();
        }

        private void GetCommands () {
            Commands = interpreter?.GetRegisteredCommands ();
        }
    }
}
