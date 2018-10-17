using System;
using System.Linq;
using JobLoggerApp.Demo;
using JobLoggerApp.DI;
using JobLoggerApp.Interfaces;
using Microsoft.Practices.Unity;

namespace JobLoggerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var unityContainer = UnityConfig.GetConfiguredContainer();

            var levelLogger = unityContainer.Resolve<ILevelLogger>();
            var levelsAllowedString = string.Join(",", levelLogger.GetAllowedLevels());

            var commands = new ICommand[]
            {
                unityContainer.Resolve<IConsoleLoggerCommand>(),
                unityContainer.Resolve<IFileLoggerCommand>(),
                unityContainer.Resolve<IDatabaseLoggerCommand>(),

                new ExitCommand()
            };

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Level Logger allowed are: {levelsAllowedString}. These can be on/off from config file.");
                Console.WriteLine("======================================================");
                Console.WriteLine("================== MENU ==============================");
                Console.WriteLine("======================================================");

                for (var i = 0; i < commands.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {commands[i].Description}");
                }

                var menuOption = string.Empty;
                var commandIndex = -1;

                do
                {
                    menuOption = Console.ReadLine();
                } while (!int.TryParse(menuOption, out commandIndex) || commandIndex > commands.Length);

                commands[commandIndex - 1].Execute();

                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
