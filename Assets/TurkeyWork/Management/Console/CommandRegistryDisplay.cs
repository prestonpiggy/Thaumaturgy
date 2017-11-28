using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Management {

    [RequireComponent (typeof (CommandInterpreter))]
    public sealed class CommandRegistryDisplay : MonoBehaviour {

        private CommandInterpreter interpreter;
        public CommandBase[] Commands { get; private set; }

        private void OnEnable () {
            gameObject.hideFlags = HideFlags.DontSaveInBuild;
        }

        void Awake () {
            interpreter = GetComponent<CommandInterpreter> ();
        }

        private void Start () {
            GetCommands ();
            interpreter.OnCommandsUpated += GetCommands;
        }

        private void GetCommands () {
            Commands = interpreter.GetRegisteredCommands ();
        }
    }
}
