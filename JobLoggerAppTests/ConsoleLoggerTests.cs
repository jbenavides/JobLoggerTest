namespace JobLoggerAppTests
{
    using System;
    using System.Collections.Generic;
    using JobLoggerApp.Helpers;
    using JobLoggerApp.Implementations;
    using JobLoggerApp.Interfaces;
    using Helper;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;


    [TestClass]
    public class ConsoleLoggerTests
    {
        private Mock<ILevelLogger> _levelLoggerMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _levelLoggerMock = new Mock<ILevelLogger>();
        }

        [TestMethod]
        [ExpectedException(typeof(MessageTypeNotAllowedException), "Message type: Message is not allowed, please enable it in the settings file.")]
        public void LogMessage_NotAllowedMessageType_ThrowMessageTypeNotAllowedException()
        {
            // Arrange
            _levelLoggerMock.Setup(x => x.GetAllowedLevels()).Returns(new List<MessageType>());

            // Act & Assert
            var consoleLogger = new ConsoleLogger(_levelLoggerMock.Object);
            consoleLogger.LogMessage(MessageType.Message, "message");
        }

        [TestMethod]
        public void LogMessage_MessageTypeWarningAllowed_MethodExecutedWithouthErros()
        {
            // Arrange
            _levelLoggerMock.Setup(x => x.GetAllowedLevels()).Returns(new List<MessageType>{MessageType.Warning});
            var expectedOutput = $"Date: {DateTime.Now:dd/MM/yyy hh:mm}, MessageType: {MessageType.Warning}, Message: message{Environment.NewLine}";

            // Act
            var consoleLogger = new ConsoleLogger(_levelLoggerMock.Object);
            using (var consoleOutput = new ConsoleOutput())
            {
                consoleLogger.LogMessage(MessageType.Warning, "message");

                // Assert
                Assert.AreEqual(expectedOutput, consoleOutput.GetOuput());
            }
        }
    }
}
