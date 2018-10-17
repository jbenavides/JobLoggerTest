namespace JobLoggerAppTests
{
    using System.Collections.Generic;
    using JobLoggerApp.Helpers;
    using JobLoggerApp.Implementations;
    using JobLoggerApp.Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class DatabaseLoggerTests
    {
        private Mock<ILevelLogger> _levelLoggerMock;
        private Mock<IDatabaseLoggerHelper> _databaseLoggerHelperMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _levelLoggerMock = new Mock<ILevelLogger>();
            _databaseLoggerHelperMock = new Mock<IDatabaseLoggerHelper>();
        }

        [TestMethod]
        [ExpectedException(typeof(MessageTypeNotAllowedException), "Message type: Message is not allowed, please enable it in the settings file.")]
        public void LogMessage_NotAllowedMessageType_ThrowMessageTypeNotAllowedException()
        {
            // Arrange
            _levelLoggerMock.Setup(x => x.GetAllowedLevels()).Returns(new List<MessageType>());

            // Act & Assert
            var consoleLogger = new DatabaseLogger(_levelLoggerMock.Object, _databaseLoggerHelperMock.Object);
            consoleLogger.LogMessage(MessageType.Message, "message");
        }

        [TestMethod]
        public void LogMessage_MessageTypeMessageAllowed_MethodToSaveInDBCalled()
        {
            // Arrange
            _levelLoggerMock.Setup(x => x.GetAllowedLevels()).Returns(new List<MessageType>{MessageType.Message});

            // Act
            var consoleLogger = new DatabaseLogger(_levelLoggerMock.Object, _databaseLoggerHelperMock.Object);
            consoleLogger.LogMessage(MessageType.Message, "message");
            
            //Assert
            _databaseLoggerHelperMock.Verify(x=>x.SaveLog(MessageType.Message, "message"), Times.Exactly(1));
        }
    }
}
