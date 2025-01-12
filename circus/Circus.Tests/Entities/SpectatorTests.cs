using Circus.App.Entities;

namespace Circus.Tests.Entities
{
    [TestClass]
    public class SpectatorTests
    {
        [TestMethod]
        public void When_Applause_WithTrickAndAnimalName_ThenDisplayReturnString()
        {
            // Arrange
            Spectator spectator = new Spectator();
            Trick trick = new Trick("Walk on hand");
            string animalName = "Snoopy";

            // Act
            string applauseResult = spectator.Applause(trick, animalName);

            // Assert
            Assert.AreEqual($"spectateur applaudit pendant le tour d'acrobatie '{trick.Name}' du {animalName}", applauseResult);
        }
    }
}
