namespace JobLoggerAppTests
{
    using System.Collections.Generic;
    using System.IO;
    using JobLoggerApp.Helpers;
    using JobLoggerApp.Implementations;
    using JobLoggerApp.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class FileLoggerTests
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
            var consoleLogger = new FileLogger(_levelLoggerMock.Object);
            consoleLogger.LogMessage(MessageType.Message, "message");
        }

        [TestMethod]
        public void LogMessage_MessageTypeMessageAllowed_ResultNewFileWithMessage()
        {
            // Arrange
            _levelLoggerMock.Setup(x => x.GetAllowedLevels()).Returns(new List<MessageType>{MessageType.Message});
            var streamWriterMock = new Mock<StreamWriter>("output.txt");
            streamWriterMock.Object.Write("message");

            // Act
            var consoleLogger = new FileLogger(_levelLoggerMock.Object);
            consoleLogger.LogMessage(MessageType.Message, "message");
            
            // Assert
           streamWriterMock.Verify(a => a.Write("message"), Times.Exactly(1));
            
        }
    }
}
