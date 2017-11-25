using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Management {

    public sealed class CommandArgument {

        public readonly string argKey;
        public readonly bool isRequired;

        public string ArgumentString => isRequired ? $" {argKey}" : $"[{argKey}]";

        public CommandArgument (string key, bool optional = false) {
            argKey = key;
            isRequired = !optional;
        }

        public bool Validate (string[] argStack, int matchIndex) {
            return true;
        }

    }

    public static class CommandArgExtensions {

        public static string AllToString (this CommandArgument[] argumentArray) {
            var s = "";
            foreach (var arg in argumentArray)
                s += arg.ArgumentString;
            return s;
        }
    }

}