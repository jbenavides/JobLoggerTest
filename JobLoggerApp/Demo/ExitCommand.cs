namespace JobLoggerApp.Demo
{
    using System;

    public class ExitCommand : ICommand
    {
        public string Description => "Exit";

        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
