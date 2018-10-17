namespace JobLoggerApp.DI
{
    using System;
    using Implementations;
    using Interfaces;
    using Demo;
    using Microsoft.Practices.Unity;

    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return Container.Value;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IConsoleLogger, ConsoleLogger>();
            container.RegisterType<IFileLogger, FileLogger>();
            container.RegisterType<IDatabaseLogger, DatabaseLogger>();
            container.RegisterType<ILevelLogger, LevelLogger>();
            container.RegisterType<ILevelLoggerSettings, LevelLoggerSettings>();


            container.RegisterType<IConsoleLoggerCommand, ConsoleLoggerCommand>();
            container.RegisterType<IFileLoggerCommand, FileLoggerCommand>();
            container.RegisterType<IDatabaseLoggerCommand, DatabaseLoggerCommand>();
        }
    }
}
