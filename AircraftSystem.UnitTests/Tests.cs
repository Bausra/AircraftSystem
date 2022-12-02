using AircraftSystem;
using AircraftSystem.Models;

namespace AircraftSystem.UnitTests
{
    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public void GetActionType_Input1to3_ResultActionTypeReportOrDatabase()
        {
            //Arrange
            AircraftSystem.Menu menu = new AircraftSystem.Menu();
            string expected = "Incorrect input!";
            //Act
            ActionType result = menu.GetActionType();
            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}