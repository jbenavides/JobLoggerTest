namespace JobLoggerApp.Demo
{
    public interface ICommand
    {
        string Description { get; }
        void Execute();
    }
}
