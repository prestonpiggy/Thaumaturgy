namespace TurkeyWork.Management {

    public delegate string CommandEvent (string[] args);

    public class CommandBase {

        public readonly string Name;

        protected readonly CommandArgument[] args;
        protected readonly CommandEvent commandFunc;

        public CommandBase (string commandName, CommandEvent commandFunc, params CommandArgument[] commandArgs) {
            Name = commandName;
            args = commandArgs;
            this.commandFunc = commandFunc;
        }

        public string InvokeWithArgs (string[] args) {
            return commandFunc?.Invoke (args);
        }

        public override string ToString () {
            return $"{Name} {args.AllToString ()}";
        }

    }

}