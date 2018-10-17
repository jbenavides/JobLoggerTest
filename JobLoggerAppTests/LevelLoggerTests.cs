using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobLoggerApp.Helpers;
using JobLoggerApp.Implementations;
using JobLoggerApp.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace JobLoggerAppTests
{
    [TestClass]
    public class LevelLoggerTests
    {
        private Mock<ILevelLoggerSettings> _levelLoggerSettingsMock;

        [TestInitialize]
        public void TestInitialize()
        {
            _levelLoggerSettingsMock = new Mock<ILevelLoggerSettings>();
        }

        [TestMethod]
        [ExpectedException(typeof(MessageTypeNullException),"Must enable at least one message type (Message, Warning, Error).")]
        public void GetAllowedLevels_EmptyMessageTypesAllowed_ReturnNullException()
        {
            // Arrange
            _levelLoggerSettingsMock.Setup(x => x.IsErrorTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsMessageTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsWarningTypeAllowed).Returns(false);

            // Act & Assert
            var levelLogger = new LevelLogger(_levelLoggerSettingsMock.Object);
            levelLogger.GetAllowedLevels();
        }

        [TestMethod]
        public void GetAllowedLevels_MessageTypeAllowed_ReturnListWithOneElement()
        {
            // Arrange
            _levelLoggerSettingsMock.Setup(x => x.IsErrorTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsMessageTypeAllowed).Returns(true);
            _levelLoggerSettingsMock.Setup(x => x.IsWarningTypeAllowed).Returns(false);

            // Act
            var levelLogger = new LevelLogger(_levelLoggerSettingsMock.Object);
            var result = levelLogger.GetAllowedLevels();

            // Assert
            var expectedItems = 1;
            Assert.AreEqual(expectedItems, result.Count());
        }

        [TestMethod]
        public void GetAllowedLevels_WarningTypeAllowed_ReturnListWithOneElement()
        {
            // Arrange
            _levelLoggerSettingsMock.Setup(x => x.IsErrorTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsMessageTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsWarningTypeAllowed).Returns(true);

            // Act
            var levelLogger = new LevelLogger(_levelLoggerSettingsMock.Object);
            var result = levelLogger.GetAllowedLevels();

            // Assert
            var expectedItems = 1;
            Assert.AreEqual(expectedItems, result.Count());
        }

        [TestMethod]
        public void GetAllowedLevels_ErrorTypeAllowed_ReturnListWithOneElement()
        {
            // Arrange
            _levelLoggerSettingsMock.Setup(x => x.IsErrorTypeAllowed).Returns(true);
            _levelLoggerSettingsMock.Setup(x => x.IsMessageTypeAllowed).Returns(false);
            _levelLoggerSettingsMock.Setup(x => x.IsWarningTypeAllowed).Returns(false);

            // Act
            var levelLogger = new LevelLogger(_levelLoggerSettingsMock.Object);
            var result = levelLogger.GetAllowedLevels();

            // Assert
            var expectedItems = 1;
            Assert.AreEqual(expectedItems, result.Count());
        }

        [TestMethod]
        public void GetAllowedLevels_AllTypesAllowed_ReturnListWithThreeElement()
        {
            // Arrange
            _levelLoggerSettingsMock.Setup(x => x.IsErrorTypeAllowed).Returns(true);
            _levelLoggerSettingsMock.Setup(x => x.IsMessageTypeAllowed).Returns(true);
            _levelLoggerSettingsMock.Setup(x => x.IsWarningTypeAllowed).Returns(true);

            // Act
            var levelLogger = new LevelLogger(_levelLoggerSettingsMock.Object);
            var result = levelLogger.GetAllowedLevels();

            // Assert
            var expectedItems = 3;
            Assert.AreEqual(expectedItems, result.Count());
        }
    }
}
