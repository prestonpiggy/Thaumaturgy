using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace TurkeyWork.Management {

    public sealed class CommandInterpreter : SingletonBehaviour<CommandInterpreter> {

        public event System.Action OnCommandsUpated;

        private Dictionary<string, CommandBase> _commandDict = new Dictionary<string, CommandBase> ();
        //private Queue<string> commandQueue = new Queue<string> ();
        [NonSerialized] public List<string> commandHistory;

        public CommandConsole Console;

        public int CommandHistoryLength => commandHistory.Count;

        public string LastResult { get; private set; }

        public CommandBase[] GetRegisteredCommands () {
            var arr = new CommandBase[_commandDict.Values.Count];
            _commandDict.Values.CopyTo (arr, 0);
            return arr;
        }

        public bool SendCommand (string commandString) {
            AddCommandToHistory (commandString);

            string[] argsStack;
            CommandBase command;
            if (TryGetCommand (commandString, out command, out argsStack)) {
                LastResult = command?.InvokeWithArgs (argsStack);
                return true;
            }
            return false;
        }

        public bool TryGetHistoryCommand (int index, out string command) {
            if (index < commandHistory.Count && index > 0) {
                command = commandHistory[index];
                return true;
            }
            command = "";
            return false;
        }

        private bool TryGetCommand (string commandString, out CommandBase command, out string[] argsStack) {
            Debug.Log ("Trying to find matching command..");
            argsStack = null;
            command = null;

            argsStack = commandString.Split (' ');
            if (_commandDict.TryGetValue (argsStack[0], out command)) {
                Debug.Log ("Matching command found!");
                return true;
            }
            Debug.Log ("Command not found!");
            return false;
        }

        public static void RegisterCommand (CommandBase command) {
            Instance._RegisterCommand (command);
        }
        private void _RegisterCommand (CommandBase command) {
            _commandDict.Add (command.Name, command);
            OnCommandsUpated?.Invoke ();
        }

        /// <summary>
        /// This will also reset the command history selection index!
        /// </summary>
        /// <param name="command"></param>
        private void AddCommandToHistory (string command) {
            commandHistory.Add (command);
        }

        private void AddDefaultCommands () {
            Debug.Log ("There are no default commands!");
        }

        private void Awake () {
            AddDefaultCommands ();
        }

    }
}