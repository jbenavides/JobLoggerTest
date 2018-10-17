﻿namespace JobLoggerApp.Demo
{
    using System;
    using Helpers;
    using Implementations;

    public interface IDatabaseLoggerCommand : ICommand {}

    public class DatabaseLoggerCommand : IDatabaseLoggerCommand
    {
        private readonly IDatabaseLogger _databaseLogger;

        public DatabaseLoggerCommand(IDatabaseLogger databaseLogger)
        {
            _databaseLogger = databaseLogger;
        }

        public string Description => "Log message in Database.";
        public void Execute()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string messageTypeString;
            var messageType = -1;

            var messageTypesAllowed = Helper.GetMessageTypesEnumValues();

            do
            {
                Console.WriteLine($"Insert Message Level. ({MessageType.Message} = {(int)MessageType.Message}, {MessageType.Warning} = {(int)MessageType.Warning}, {MessageType.Error} = {(int)MessageType.Error})");
                messageTypeString = Console.ReadLine();

            } while (!int.TryParse(messageTypeString, out messageType) || !messageTypesAllowed.Contains(messageType));

            string message;

            do
            {
                Console.WriteLine("Insert message.");
                message = Console.ReadLine();

            } while (string.IsNullOrEmpty(message));

            Console.WriteLine("");

            try
            {
                _databaseLogger.LogMessage((MessageType)messageType, message);
            }
            catch (MessageTypeNotAllowedException messageTypeNotAllowed)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(messageTypeNotAllowed);
            }
            catch (MessageTypeNullException messageTypeNullException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(messageTypeNullException);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
            }
        }
    }
}